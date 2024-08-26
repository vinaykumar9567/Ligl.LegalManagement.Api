using Ligl.Core.Sdk.Shared.Repository.Region.Domain;
using Ligl.LegalManagement.Model.Query;
using Ligl.LegalManagement.Model.Query.Constants;
using Ligl.LegalManagement.Model.Query.CustomModels;
using Ligl.LegalManagement.Repository.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Reflection;



namespace Ligl.LegalManagement.Business.Command
{
    /// <summary>
    /// Class for SaveCaseStakeHoldersDetailQueryHandler
    /// </summary>
    /// <seealso cref="SaveCaseStakeHoldersDetailQuery" />
    public class SaveCaseStakeHoldersDetailQueryHandler(IRegionUnitOfWork regionUnitOfWork, ILogger<SaveCaseStakeHoldersDetailQueryHandler> logger) : IRequestHandler<SaveCaseStakeHoldersDetailQuery, List<EntityLHNResponse>>
    {
        private const string ClassName = nameof(SaveCaseStakeHoldersDetailQueryHandler);

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="AccessViolationException">Invalid user exception</exception>
        public async Task<List<EntityLHNResponse>> Handle(SaveCaseStakeHoldersDetailQuery request, CancellationToken cancellationToken)
        {
            const string methodName = $"{ClassName} - {nameof(Handle)}";
            try
            {
                

               logger.LogInformation(message: "Started execution of {methodName}", methodName);
                var caseID = GetCase(request.CaseID);                           
                var EmployeeMaster = await regionUnitOfWork.EmployeeMasterRepository.GetAsync();
                request.caseStakeHolderEmailTemplate.CaseStakeHolderModels.ForEach(
                         casestakeholder =>
                         {
                             casestakeholder.CaseID = 1;
                             casestakeholder.StakeHolderID =
                                 GetStakeHolder(casestakeholder.StakeHolderModel?.ID)
                                    // .StakeHolderID;
                                    .Id;
                         });
                //request.caseStakeHolderEmailTemplate.CaseStakeHolderModels = SaveCaseStakeHolders(request.caseStakeHolderEmailTemplate.CaseStakeHolderModels);
                return (List<EntityLHNResponse>)EmployeeMaster;
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

        public async Task<Case?> GetCase (Guid? caseUniqueID)
        {
            await regionUnitOfWork.CaseRepository.GetAsync();
            var caseDetails =(await regionUnitOfWork.CaseRepository.GetAsync()).FirstOrDefault(caseinfo => caseinfo.Uuid == caseUniqueID && !caseinfo.IsDeleted);

            if (caseDetails == null)
                logger.LogError("case details not found");

            return caseDetails;
        }

    

        public async Task<StakeHolderModel> GetStakeHolder(Guid? stakeHolderUniqueID)
        {

            try
            {
                if (stakeHolderUniqueID == null || stakeHolderUniqueID == Guid.Empty)
                    throw new CustomError(CaseErrorCodes.IdNotFound,
                        string.Format(
                            BaseErrorProvider.GetErrorString<CaseErrorCodes>(CaseErrorCodes.IdNotFound),
                            "StakeHolderId"),
                        $"{ClassName} - {nameof(GetStakeHolder)}");
                var dbStakeHolderEntity = (await regionUnitOfWork.stakeHolderEntity.GetAsync()).FirstOrDefault(stakeholder => stakeholder.UUID == stakeHolderUniqueID && !stakeholder.IsDeleted.Value);
                if (dbStakeHolderEntity == null)
                {
                    throw new CustomError(CaseErrorCodes.StakeholderNotFound,
                        BaseErrorProvider.GetErrorString<CaseErrorCodes>(CaseErrorCodes.StakeholderNotFound),
                        $"{ClassName} - {nameof(GetStakeHolder)}");
                 
                }

                dbStakeHolderEntity.UUID = dbStakeHolderEntity.UUID;
                StakeHolderModel stakeHolderModel = null!;
                stakeHolderModel.UUID = dbStakeHolderEntity.UUID;
                stakeHolderModel.FullName = dbStakeHolderEntity.FullName;
                stakeHolderModel.CategoryID = dbStakeHolderEntity.CategoryID;
                return StakeHolderMapper.Mapper(stakeHolderModel, stakeHolderModel);

            }
            catch (Exception ex)
            {
                logger.LogError("Error Processing GetStakeHolder {ex.Message}", ex.Message);
            }
            finally
            {
                logger.LogInformation("process completed");
            }
            return null;
       
        }

        private async Task<List<CaseStakeHolderModel>> SaveCaseStakeHolders(List<CaseStakeHolderModel> caseStakeHolderModels)
        {
            const string methodName = $"{ClassName} - {nameof(Handle)}";

            try
            {
                foreach (var caseStakeHolderModel in caseStakeHolderModels)
                {
                    if (caseStakeHolderModel.ID != null && caseStakeHolderModel.ID != Guid.Empty)
                        continue;
               
                    var caseStakeHolder = StakeHolderMapper.MapCaseStakeHolderEntity(caseStakeHolderModel, caseStakeHolderModel.CaseID);
                    var casestake = new  Ligl.LegalManagement.Repository.Domain.CaseStakeHolder
                    {
                     CaseId = caseStakeHolderModel.CaseID,
                    };


                    await regionUnitOfWork.caseStakeHolderEntity.CreateAsync(casestake);
                }
               

                return caseStakeHolderModels;
            }
            catch (Exception e)
            {
                logger.LogError("Error in {Name} - {Message} /n {Trace}",
                  methodName, e.Message, e.StackTrace);
                throw;
            }
        }



    }
}
