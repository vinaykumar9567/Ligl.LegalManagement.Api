using Ligl.LegalManagement.Model.Command;
using Ligl.LegalManagement.Model.Common;
using Ligl.LegalManagement.Model.Query;
using Ligl.LegalManagement.Repository.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Ligl.LegalManagement.Model.Query.Constants;
using Ligl.LegalManagement.Model.Query.CustomModels;
using StakeHolderEntity = Ligl.LegalManagement.Model.Command.StakeHolderEntity;
using StakeHolder = Ligl.LegalManagement.Repository.Domain.StakeHolder;
using Microsoft.Data.SqlClient;
using Ligl.Core.Sdk.Shared.Business.Region.Cache.Interface;


namespace Ligl.LegalManagement.Business.Command
{

    /// <summary>
    /// Class for UpdateStakeHolderDetailQueryHandler
    /// </summary>
    /// <seealso cref="UpdateStakeHolderDetailQuery" />
    public class UpdateStakeHolderDetailQueryHandler(IRegionUnitOfWork regionUnitOfWork, ILookUpBusiness lookUpBusiness, ILogger<UpdateCaseLHEscalationDetailQueryHandler> logger
       ) : IRequestHandler<UpdateStakeHolderDetailQuery, Unit>
    {
        private const string ClassName = nameof(UpdateCaseLHEscalationDetailQueryHandler);

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="AccessViolationException">Invalid user exception</exception>
        public async Task<Unit> Handle(UpdateStakeHolderDetailQuery request, CancellationToken cancellationToken)
        {
            const string methodName = $"{ClassName} - {nameof(Handle)}";
            try
            {

                logger.LogInformation(message: "Started execution of {methodName}", methodName);
                if (request.CaseStakeHolderModel?.StakeHolderModel?.ID == null || request.CaseStakeHolderModel?.StakeHolderModel?.ID == Guid.Empty)
                    logger.LogError("Error Processing {methodName}, {ErrorType.Error} ,{ClassName} ", methodName, ErrorType.Error, ClassName);

                bool isMailExists = (await regionUnitOfWork.stakeHolderEntity.GetAsync()).Any(x => x.EmailAddress == request.CaseStakeHolderModel.StakeHolderModel.EmailAddress && x.UUID != request.CaseStakeHolderModel.StakeHolderModel.ID && x.IsDeleted == false);
                if (isMailExists)
                {
                    throw new CustomError(CaseErrorCodes.Emailalreadyexists,
                    string.Format(BaseErrorProvider.GetErrorString<CaseErrorCodes>(CaseErrorCodes.Emailalreadyexists)),
                        $"{ClassName} - {nameof(GetStakeHolder)}");
                }
                StakeHolderModel stakeHolderModel = null;
                  var dbStakeHolderModel = GetStakeHolder(request.CaseStakeHolderModel?.StakeHolderModel?.ID);

                StakeHolderMapper.MapStakeHolderEntity(dbStakeHolderModel.Result, request.CaseStakeHolderModel.StakeHolderModel);
                //   return SaveStakeHolder(dbStakeHolderModel).ID;


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
                var dbStakeHolderEntity = (await regionUnitOfWork.stakeHolderEntity.GetAsync()).FirstOrDefault(
                    stakeholder => stakeholder.UUID == stakeHolderUniqueID && !stakeholder.IsDeleted==false);

                if (dbStakeHolderEntity == null)
                    throw new CustomError(CaseErrorCodes.StakeholderNotFound,
                        BaseErrorProvider.GetErrorString<CaseErrorCodes>(CaseErrorCodes.StakeholderNotFound),
                        $"{ClassName} - {nameof(GetStakeHolder)}");

                dbStakeHolderEntity.UUID = dbStakeHolderEntity.UUID;
                 var stakeHolderModel = GenericMapper.Map< StakeHolder, StakeHolderModel >(dbStakeHolderEntity);

              
                //var stakeHolderModels = StakeHolderMapper.AutoMapStakeHolderEntity(dbStakeHolderEntity);
                return stakeHolderModel;
            }
            catch (Exception ex)
            {
                logger.LogError("Error Processing GetStakeHolder {ex} {ErrorType.Error} {ClassName}", ex, ErrorType.Error, ClassName);
                throw;
            }

        }


        public async Task<StakeHolderModel> SaveStakeHolder(StakeHolderModel stakeHolderModel)
        {
            try
            {
            
                StakeHolderModel stakeHolderModels =new StakeHolderModel();
                StakeHolderEntity stakeHolder = StakeHolderSave(stakeHolderModel).Result;

         var GetStakeHolders= GetStakeHolder(stakeHolder?.ID);
                return await GetStakeHolders;

            }
            catch (Exception ex)
            {
                logger.LogError("Error Processing GetStakeHolder {ex} {ErrorType.Error} {ClassName}", ex, ErrorType.Error, ClassName);
                throw;
            }

        }
        public  async Task<StakeHolderEntity> StakeHolderSave(StakeHolderModel stakeHolderModel)
        {
            try
            {
                Guid? Active = lookUpBusiness.GetDomainValueById(id: Status.Active.id).Uuid;
                Guid searchUuid = stakeHolderModel.StatusUniqueID;
                stakeHolderModel.DepartmentID = (await regionUnitOfWork.DepartmentRepository.GetAsync()).FirstOrDefault(dept => dept.Uuid == stakeHolderModel.DepartmentUniqueID)?.DepartmentId;
                //stakeHolderModel.Status = (await regionUnitOfWork.LookupRepository.GetAsync()).FirstOrDefault(x => x.Uuid == (stakeHolderModel.StatusUniqueID).;
                stakeHolderModel.CategoryID = (await regionUnitOfWork.LookupRepository.GetAsync()).FirstOrDefault(x => x.Uuid == stakeHolderModel.CategoryUniqueID)?.LookupId;
                
                StakeHolderEntity stakeHolderEntity = null;
                var stakeholders = new StakeHolder
                {
                    FirstName=stakeHolderModel.FirstName!,
                    LastName=stakeHolderModel.LastName!, 
                    FullName=stakeHolderModel.FullName!,
                    EmailAddress=stakeHolderModel.EmailAddress!,
                    DepartmentID=stakeHolderModel.DepartmentID,
                    Status = stakeHolderModel.Status,   
                    StatusChangeReason=stakeHolderModel.StatusChangeReason,
                    CategoryID=stakeHolderModel.CategoryID,
                    CreatedBy=stakeHolderModel.CreatedBy,
                    CreatedOn=stakeHolderModel.CreatedOn,
                    ModifiedOn=stakeHolderModel.ModifiedOn,
                    ModifiedBy=stakeHolderModel.ModifiedBy,
                };

                await regionUnitOfWork.stakeHolderEntity.UpdateAsync(stakeholders);
                regionUnitOfWork.SaveAsync();

                 var stakeHolder = StakeHolderMapper.AutoMapStakeHolderEntities(stakeHolderModel, stakeHolderEntity);
                return  stakeHolder;


            }
            catch (SqlException ex)
            {
                logger.LogError("Error Processing StakeHolderSave {ex} {ErrorType.Error} {ClassName}", ex, ErrorType.Error, ClassName);
               
                if (ex.Message.StartsWith(SQLErrorCodes.NotFound.ToString()))
                {
                    throw new CustomError(CaseErrorCodes.StakeholderNotFound,
                        BaseErrorProvider.GetErrorString<CaseErrorCodes>(CaseErrorCodes.StakeholderNotFound),
                        $"{ClassName} - {nameof(SaveStakeHolder)}-{ex.StackTrace} - {ex.Message}");
                }
                throw new CustomError(BaseErrorCodes.UnknownSqlException,
                    BaseErrorProvider.GetErrorString<BaseErrorCodes>(BaseErrorCodes.UnknownSqlException),
                    $"{ClassName} - {nameof(StakeHolderSave)}-{ex.StackTrace} - {ex.Message}");
            }
            catch (Exception ex)
            {
                logger.LogInformation( "Error Processing StakeHolderSave {ex} {ErrorType.Error} {ClassName}", ex, ErrorType.Error, ClassName);
                throw;
            }
        }




    }
}
