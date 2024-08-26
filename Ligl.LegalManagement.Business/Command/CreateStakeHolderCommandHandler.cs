using Ligl.Core.Sdk.Shared.Business;
using Ligl.Core.Sdk.Shared.Business.Region.Cache.Interface;
using Ligl.LegalManagement.Model.Common;
using Ligl.LegalManagement.Model.Query;
using Ligl.LegalManagement.Model.Query.Constants;
using Ligl.LegalManagement.Model.Query.CustomModels;
using Ligl.LegalManagement.Repository.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ligl.LegalManagement.Business.Command
{
    /// <summary>
    /// Class for CreateStakeHolderCommandHandler
    /// </summary>
    /// <seealso cref="CreateStakeHolderCommand" />
    public class CreateStakeHolderCommandHandler(IRegionUnitOfWork regionUnitOfWork, ILookUpBusiness lookUpBusiness, IUserContextBusiness userContextBusiness,
        ILogger<CreateStakeHolderCommandHandler> logger
     ) : IRequestHandler<CreateStakeHolderCommand, Unit>
    {
        private const string ClassName = nameof(CreateStakeHolderCommandHandler);

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="AccessViolationException">Invalid user exception</exception>
        public async Task<Unit> Handle(CreateStakeHolderCommand request, CancellationToken cancellationToken)
        {
            const string methodName = $"{ClassName} - {nameof(Handle)}";
            try
            {
               // bool defaultSettings = false;
             logger.LogInformation(message: "Started execution of {methodName}", methodName);
         
                //bool _isLegalFirm = GetIsLegalFirm();     function not implemented.
                bool _isLegalFirm = false;
                 bool isMailExists =(await regionUnitOfWork.stakeHolderEntity.GetAsync()).Any(x => x.EmailAddress == request.caseStakeHolderModel.StakeHolderModel.EmailAddress && x.IsDeleted == false);
                    if (isMailExists)
                    {
                        logger.LogError("Error in {methodName} - invalid email", methodName);
                        throw new AccessViolationException("Invalid stakeholder exception");
                    }
                     
                        var currentDateTime = DateTime.UtcNow;
                        var user = userContextBusiness.GetContext;
                        request.caseStakeHolderModel.StakeHolderModel.ModifiedOn = currentDateTime;
                        request.caseStakeHolderModel.StakeHolderModel.ModifiedBy = user.userIdString;
                        var department = await regionUnitOfWork.DepartmentRepository.GetByIdAsync(request.caseStakeHolderModel.StakeHolderModel.DepartmentUniqueID);
                        request.caseStakeHolderModel.StakeHolderModel.DepartmentID = department?.DepartmentId;
                        var lookup = await regionUnitOfWork.LookupRepository.GetByIdAsync(request.caseStakeHolderModel.StakeHolderModel.CategoryUniqueID);
                        request.caseStakeHolderModel.StakeHolderModel.CategoryID = lookup?.LookupId;
                        var lookupstatus = await regionUnitOfWork.LookupRepository.GetByIdAsync(request.caseStakeHolderModel.StakeHolderModel.StatusUniqueID);
                        request.caseStakeHolderModel.Status = lookup?.Status;
                request.caseStakeHolderModel.StakeHolderModel.UUID= Guid.NewGuid();
                var createstakeholder = new Repository.Domain.StakeHolder
                        {
                         
                            UUID = request.caseStakeHolderModel.StakeHolderModel.UUID,
                            FirstName = request.caseStakeHolderModel.StakeHolderModel.FirstName!,
                            MiddleName = request.caseStakeHolderModel.StakeHolderModel.MiddleName!,
                            LastName = request.caseStakeHolderModel.StakeHolderModel.LastName!,
                            EmailAddress = request.caseStakeHolderModel.StakeHolderModel.EmailAddress!,
                            DepartmentID = request.caseStakeHolderModel.StakeHolderModel.DepartmentID,
                            Status = request.caseStakeHolderModel.StakeHolderModel.Status,
                            FullName = request.caseStakeHolderModel.StakeHolderModel.FullName!,
                            StatusChangeReason = request.caseStakeHolderModel.StakeHolderModel.StatusChangeReason,
                            CategoryID = request.caseStakeHolderModel.StakeHolderModel.CategoryID,
                            IsDeleted = false,
                            CreatedBy="vinay",
                            CreatedOn=DateTime.UtcNow,
                            ModifiedBy="vinay",
                            ModifiedOn=DateTime.UtcNow
                        };

                        await regionUnitOfWork.stakeHolderEntity.CreateAsync(createstakeholder);
                        regionUnitOfWork.Save();

                if (!_isLegalFirm)
                {
                    request.caseStakeHolderModel.CaseID = (await regionUnitOfWork.CaseRepository.GetAsync()).FirstOrDefault(caseinfo => caseinfo.Uuid == request.caseStakeHolderModel.CaseUniqueID && !caseinfo.IsDeleted).CaseId;
                    if (request.caseStakeHolderModel?.CaseID == null)
                        throw new CustomError(CaseErrorCodes.CaseNotFound,
                            BaseErrorProvider.GetErrorString<CaseErrorCodes>(CaseErrorCodes.CaseNotFound),
                            $"{ClassName} - {nameof(Handle)}");

                    request.caseStakeHolderModel.StakeHolderID = createstakeholder.StakeHolderID;

                    _ = SaveCaseStakeHolders(
                          new List<CaseStakeHolderModel>
                          {
                                    request.caseStakeHolderModel
                          });


                   
                }
                return Unit.Value;
               
            }
            catch (Exception e)
            {
                logger.LogError("Error in {Name} - {Message} /n {Trace}",
                    methodName, e.Message, e.StackTrace);
                throw;
            }
            finally
            {
                logger.LogInformation(message: "Completed execution of {methodName}", methodName);
            }
        }



        /// <summary>
        /// To Save CaseStakeHolders
        /// </summary>
        /// <param name="matterManagementContext"></param>
        /// <param name="caseStakeHolderModels"></param>
        /// <returns></returns>
        private async Task< List<CaseStakeHolderModel>> SaveCaseStakeHolders(List<CaseStakeHolderModel> caseStakeHolderModels)
        {
            try
            {
                
                foreach (var caseStakeHolderModel in caseStakeHolderModels)
                {
                    if (caseStakeHolderModel.ID != null && caseStakeHolderModel.ID != Guid.Empty)
                        continue;
                    var user = userContextBusiness.GetContext;
                    var caseStakeHolder = StakeHolderMapper.MapCaseStakeHolderEntitymapper(caseStakeHolderModel, caseStakeHolderModel.CaseID);
                    caseStakeHolder.CreatedOn = caseStakeHolder.ModifiedOn = DateTime.UtcNow;
                    caseStakeHolder.ModifiedBy = caseStakeHolder.CreatedBy = user.userIdString;
                    caseStakeHolder.DpnStatusId= lookUpBusiness.GetDomainValueById(id: DPNStatus.NotInitiated.id).LookupId;
                    caseStakeHolder.Status = lookUpBusiness.GetDomainValueById(id: Status.Active.id).LookupId;
                     regionUnitOfWork.caseStakeHolderEntity.Create(caseStakeHolder);
                    regionUnitOfWork.Save();
                    regionUnitOfWork.CommitAsync();

                }
                return caseStakeHolderModels;
            }
            catch (Exception ex)
            {
                logger.LogError("Error  {ex} ", ex);
                throw;
            }
        }



    }
}
