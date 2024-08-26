using Ligl.Core.Sdk.Common.Helper;
using Ligl.Core.Sdk.Shared.Business;
using Ligl.Core.Sdk.Shared.Business.Region.Cache.Interface;
using Ligl.Core.Sdk.Shared.Repository.Region.Domain;
using Ligl.LegalManagement.Business.Query;
using Ligl.LegalManagement.Model.Command;
using Ligl.LegalManagement.Model.Common;
using Ligl.LegalManagement.Model.Query;
using Ligl.LegalManagement.Model.Query.Constants;
using Ligl.LegalManagement.Model.Query.CustomModels;
using Ligl.LegalManagement.Repository.Domain;
using Ligl.LegalManagement.Repository.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EntityState = Ligl.LegalManagement.Model.Query.CustomModels.EntityState;
using EntityType = Ligl.LegalManagement.Model.Common.EntityType;
using StatusType = Ligl.LegalManagement.Model.Common.StatusType;

namespace Ligl.LegalManagement.Business.Command
{

    /// <summary>
    /// Class for LegalHoldCreateCommandHandler
    /// </summary>
    /// <seealso cref="UpdateCaseLHEscalationDetailQuery" />
    public class LegalHoldCreateCommandHandler(IRegionUnitOfWork regionUnitOfWork, ILookUpBusiness lookUpBusiness, IUserContextBusiness userContextBusiness, IEntityBusiness entityBusiness, ISender sender, ILogger<UpdateCaseLHEscalationDetailQueryHandler> logger
      ) : IRequestHandler<LegalHoldCreateCommand, Unit>
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
        public async Task<Unit> Handle(LegalHoldCreateCommand request, CancellationToken cancellationToken)
        {
            const string methodName = $"{ClassName} - {nameof(Handle)}";
            try
            {
                var user = userContextBusiness.GetContext;
                logger.LogInformation(message: "Started execution of {methodName}", methodName);
                var caseLegalHoldLDR = new DateRange();
                var caseLegalHoldKW = new KeyWord();
                var legalHold = GetCaseLegalHoldInfo(request.caseLegalHoldModel);
                if (legalHold.Result.CaseID == 0)
                {
                    logger.LogError( "Error Processing {ErrorType.Error} {ClassName}",  ErrorType.Error,ClassName);
                    throw new CustomError(AuthorizationErrorCodes.EntityNotAuthorized,
                        BaseErrorProvider.GetErrorString<AuthorizationErrorCodes>(AuthorizationErrorCodes.EntityNotAuthorized),
                        $"{ClassName} - {nameof(methodName)}");
                }
                legalHold.Result.UUID = Guid.NewGuid();
             
                legalHold.Result.Status = lookUpBusiness.GetDomainValueById(id: StatusType.Active.id).LookupId;
                legalHold.Result.IsDeleted = false;
                legalHold.Result.CreatedOn = DateTime.Now;
                legalHold.Result.CreatedBy = user.userIdString;
                if (request.caseLegalHoldModel.StartDate == null && request.caseLegalHoldModel.EndDate == null)
                {
                    legalHold.Result.DateRangeID = defaultDrKwID.DateRangeID;
                    legalHold.Result.NumberOfDays = request.caseLegalHoldModel.NumberOfDays;
                }
                else
                {
                    if (!request.caseLegalHoldModel.DateRangesInRange.Value)
                    {
                        var ldrStartDate = request.caseLegalHoldModel?.StartDate;
                        var ldrEndDate = request.caseLegalHoldModel?.EndDate;
                        var caseDateRanges = (await regionUnitOfWork.CaseDateRangeEntityRepository.GetAsync()) .Where(x => x.CaseID == legalHold.Result.CaseID);
                        foreach (var cdr in caseDateRanges)
                        {
                            var caseDateRange = (await regionUnitOfWork.DateRangeRepository.GetAsync()).FirstOrDefault(x => x.DateRangeId == cdr.DateRangeID);
                            if ((ldrStartDate <= caseDateRange?.StartDate && ldrEndDate >= caseDateRange?.EndDate)
                                && (ldrStartDate <= caseDateRange?.EndDate && ldrEndDate >= caseDateRange?.EndDate))
                            {
                                continue;
                            }
                            else
                            {
                                return Unit.Value;
                            }

                        }
                    }
                }
                caseLegalHoldLDR = SaveCaseLegalHoldDR(request.caseLegalHoldModel!);
                legalHold.Result.DateRangeID = caseLegalHoldLDR.DateRangeId;
                legalHold.Result.NumberOfDays = request.caseLegalHoldModel!.NumberOfDays;


                if (request.caseLegalHoldModel.KeyWords == null)
                {
                    legalHold.Result.KeyWordID = (long)defaultDrKwID.KeyWordID; 
                }
                else
                {
                    caseLegalHoldKW = SaveCaseLegalHoldKW(request.caseLegalHoldModel);
                    legalHold.Result.KeyWordID = caseLegalHoldKW.KeyWordsId;
                }
               SaveCaseLegalHoldHistory(legalHold.Result);
                var result=(await regionUnitOfWork.MatterNotificationTemplateRepository.GetAsync()).First(x => x.UUID == request.caseLegalHoldModel.LHNCustodianTemplateUniqueID);
           
                if (result != null && result.EntityID != null && result.EntityTypeID != null)
                {
                   // var caseLhEntityTypeId = EntitiesBusinessLogic.GetEntities().First(entity => entity.UUID == EntityType.CaseLegalHold.GetEnumValue()).ID;
                    var notificationTemplateData = (await regionUnitOfWork.MatterNotificationTemplateRepository.GetAsync()).FirstOrDefault(x => x.NotificationTemplateID == legalHold.Result.LHNCustodianTemplateID);
                    result.EntityTypeID = 1;
                    result.EntityID = legalHold.Result.CaseLegalHoldID;
                    //dbContext.SaveChanges();
                }
                if (request.caseLegalHoldModel?.LHNStakeHolderTemplateUniqueID != Guid.Empty && request.caseLegalHoldModel?.LHNStakeHolderTemplateUniqueID != null)
                {
                    var value = (await regionUnitOfWork.MatterNotificationTemplateRepository.GetAsync()).First(x => x.UUID == request.caseLegalHoldModel.LHNStakeHolderTemplateUniqueID);
                    if (value != null && value.EntityID != null && value.EntityTypeID != null)
                    {
                       // var caseLhEntityTypeId = EntitiesBusinessLogic.GetEntities().First(entity => entity.UUID == EntityType.CaseLegalHold.GetEnumValue()).ID;
                        var notificationTemplateData = (await regionUnitOfWork.MatterNotificationTemplateRepository.GetAsync()).FirstOrDefault(x => x.NotificationTemplateID == legalHold.Result.LHNStakeHolderTemplateID);
                        value.EntityTypeID = 1;
                        value.EntityID = legalHold.Result.CaseLegalHoldID;
                        //dbContext.SaveChanges();
                    }
                }

                var reminderandescalationtemplateID = ReminderandEscalationTemplate.ReminderandEscalationTemplateID;
               // Guid guid = new Guid(reminderandescalationtemplateID);

                var defaultConfig = (await regionUnitOfWork.ReminderAndEscalationRepository.GetAsync()).FirstOrDefault(x => x.UUID == reminderandescalationtemplateID);

                var reminderEscalationConfig = new EscalationReminderConfig
                {
                    ReminderConfig = request.caseLegalHoldModel.EscalationAndReminderConfigDetails.EscalationReminderConfig.ReminderConfig,
                    EscalationConfig = request.caseLegalHoldModel.EscalationAndReminderConfigDetails.EscalationReminderConfig.EscalationConfig
                };
                var remAndEscTemplateID = SaveEscalationAndReminderConfig(reminderEscalationConfig, legalHold.Result.CaseLegalHoldID);

               SaveCustInEntityLHN(request.caseLegalHoldModel, legalHold.Result.CaseLegalHoldID);
                SaveDocument(request.caseLegalHoldModel, legalHold.Result.CaseLegalHoldID);

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


        public async Task< CaseLegalHold > GetCaseLegalHoldInfo(CaseLegalHoldModel caseLegalHoldModel)
        {
            const string methodName = nameof(GetCaseLegalHoldInfo);
            try
            {
           
                    var lhnCostodianTemplate = (await regionUnitOfWork.MatterNotificationTemplateRepository.GetAsync()).FirstOrDefault(x => x.UUID == caseLegalHoldModel.LHNCustodianTemplateUniqueID);

                    var caseLegalHolds =(await regionUnitOfWork.CaseLegalHoldDetailRepository.GetAsync()).Where(x => x.UUID == caseLegalHoldModel.UUID).FirstOrDefault();
                    var result = new CaseLegalHold
                    {

                        LegalHoldName = caseLegalHoldModel.LegalHoldName,
                        CaseID =(await regionUnitOfWork.CaseRepository.GetAsync()).FirstOrDefault(x => x.Uuid == caseLegalHoldModel.CaseUniqueID)?.CaseId ?? default,

                        LHNCustodianTemplateID = caseLegalHoldModel?.LHNCustodianTemplateUniqueID != null ? lhnCostodianTemplate != null ? lhnCostodianTemplate.NotificationTemplateID : default : default,

                        LHNStakeHolderTemplateID = caseLegalHoldModel?.LHNStakeHolderTemplateUniqueID != null ? (await regionUnitOfWork.MatterNotificationTemplateRepository.GetAsync()).FirstOrDefault(x => x.UUID ==
                              caseLegalHoldModel.LHNStakeHolderTemplateUniqueID)?.NotificationTemplateID : null,

                        CustodianQuestionnaireTemplateID = caseLegalHoldModel?.CustodianQuestionnaireTemplateUniqueID != null ? (await regionUnitOfWork.QuestionnaireTemplateEntityRespository.GetAsync()).FirstOrDefault(x => x.UUID ==
                              caseLegalHoldModel.CustodianQuestionnaireTemplateUniqueID)?.QuestionnaireTemplateID : null,
                        StakeHolderQuestionnaireTemplateID = caseLegalHoldModel?.StakeHolderQuestionnaireTemplateUniqueID != null ? (await regionUnitOfWork.QuestionnaireTemplateEntityRespository.GetAsync()).FirstOrDefault(x => x.UUID ==
                                caseLegalHoldModel.StakeHolderQuestionnaireTemplateUniqueID)?.QuestionnaireTemplateID : null,

                        //ModifiedOn = DateTime.Now,
                        //ModifiedBy = UserContextInfo<BaseEntity>.UserContext.EmployeeID.ToString(),
                        DateRangeID = caseLegalHolds?.DateRangeID,
                        NumberOfDays = caseLegalHoldModel?.NumberOfDays,
                        KeyWordID = caseLegalHolds?.KeyWordID,
                    };
                    return result;
               
            }
            catch (SqlException ex)
            {
                logger.LogError("Error in {Name} - {Message} /n {Trace}",
             methodName, ex.Message, ex.StackTrace); throw new CustomError(BaseErrorCodes.UnknownSqlException,
                  BaseErrorProvider.GetErrorString<BaseErrorCodes>(BaseErrorCodes.UnknownSqlException),
                  $"{ClassName} - {methodName}-{ex.StackTrace} - {ex.Message}");
            }
            catch (Exception ex)
            {
                logger.LogError("Error in {Name} - {Message} /n {Trace}",methodName, ex.Message, ex.StackTrace);
                throw;
            }
        }


        public DateRange SaveCaseLegalHoldDR(CaseLegalHoldModel caseLegalHoldModel)
        {
            const string methodName = nameof(SaveCaseLegalHoldDR);
                           var   user = userContextBusiness.GetContext;
            try
                {
                    var caseLegalHoldRD = new DateRange();
                    caseLegalHoldRD.Uuid = Guid.NewGuid();
                    caseLegalHoldRD.StartDate = caseLegalHoldModel.StartDate;
                    caseLegalHoldRD.EndDate = caseLegalHoldModel.EndDate;
                    caseLegalHoldRD.CreatedOn = DateTime.UtcNow;
                    caseLegalHoldRD.CreatedBy = caseLegalHoldRD.ModifiedBy = user.userIdString;
                    caseLegalHoldRD.ModifiedOn = DateTime.UtcNow;
        
                caseLegalHoldRD.Status = lookUpBusiness.GetDomainValueById(id: StatusType.Active.id).LookupId;
                caseLegalHoldRD.IsDeleted = false;

                regionUnitOfWork.DateRangeRepository.Create(caseLegalHoldRD);
                regionUnitOfWork.Save();       
                return caseLegalHoldRD;
                
               }
                catch (Exception ex)
                {
                logger.LogError("Error in {Name} - {Message} /n {Trace}",methodName, ex.Message, ex.StackTrace);
                throw;
                }
                finally
                {
                logger.LogError( "Completed Processing {methodName}  {ErrorType.Error} {ClassName}", methodName, ErrorType.Information, ClassName);
                }
            
        }


        public KeyWord SaveCaseLegalHoldKW(CaseLegalHoldModel caseLegalHoldModel)
        {
            const string methodName = nameof(SaveCaseLegalHoldDR);
            var user = userContextBusiness.GetContext;
            try
            {
                var caseLegalHoldKW = new KeyWord();
                caseLegalHoldKW.Uuid = Guid.NewGuid();
                caseLegalHoldKW.KeyWords = caseLegalHoldModel.KeyWords;
                caseLegalHoldKW.CreatedOn = DateTime.UtcNow;
                caseLegalHoldKW.CreatedBy = caseLegalHoldKW.ModifiedBy = user.userIdString;
                caseLegalHoldKW.ModifiedOn = DateTime.UtcNow;
                caseLegalHoldKW.Status = lookUpBusiness.GetDomainValueById(id: StatusType.Active.id).LookupId;
                caseLegalHoldKW.IsDeleted = false;
                regionUnitOfWork.KeyWordRepository.Create(caseLegalHoldKW);
                regionUnitOfWork.Save();



                return caseLegalHoldKW;
            }
            catch (Exception ex)
            {
                logger.LogError("Error in {Name} - {Message} /n {Trace}", methodName, ex.Message, ex.StackTrace);
                throw ;
            }
            finally
            {
                logger.LogError("Completed Processing {methodName} {ErrorType.Information} {ClassName} ", methodName ,ErrorType.Information, ClassName);
            }

        }


        public  CaseLegalHoldHistory SaveCaseLegalHoldHistory(CaseLegalHold caseLegalHold)
        {
            const string methodName = nameof(SaveCaseLegalHoldHistory);
            try
            {

                var result = new CaseLegalHoldHistory
                {
                    CaseLegalHoldID = caseLegalHold.CaseLegalHoldID,
                    LegalHoldName = caseLegalHold.LegalHoldName!,
                    CaseID = caseLegalHold.CaseID,
                    LHNCustodianTemplateID = caseLegalHold.LHNCustodianTemplateID,
                    CustodianQuestionnaireTemplateID = caseLegalHold.CustodianQuestionnaireTemplateID,
                    LHNStakeHolderTemplateID = caseLegalHold.LHNStakeHolderTemplateID,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = caseLegalHold.CreatedBy,
                    ModifiedOn = DateTime.UtcNow,
                    ModifiedBy = "vinay",
                    Status = caseLegalHold.Status,
                    IsDeleted = caseLegalHold.IsDeleted,
                    StakeHolderQuestionnaireTemplateID = caseLegalHold.StakeHolderQuestionnaireTemplateID,
                    KeywordsID = caseLegalHold.KeyWordID,
                    DateRangeID = caseLegalHold.DateRangeID,
                    NumberOfDays = caseLegalHold.NumberOfDays
                };                
                regionUnitOfWork.CaseLegalHoldHistoryRepository.Create(result);
                regionUnitOfWork.Save();

                return result;
            }
            catch (DbUpdateException ex)
            {
                logger.LogError("Error Processing {methodName} {ErrorType.Error} {ClassName} ", ex, ErrorType.Error, ClassName);
               
                throw new CustomError(BaseErrorCodes.UnknownSqlException,
                  BaseErrorProvider.GetErrorString<BaseErrorCodes>(BaseErrorCodes.UnknownSqlException),
                  $"{ClassName} - {methodName}-{ex.StackTrace} - {ex.Message}");
            }
            catch (Exception ex)
            {
                logger.LogError("Error in {Name} - {Message} /n {Trace}",
                methodName, ex.Message, ex.StackTrace);
                throw;
            }
        }


        public Guid SaveEscalationAndReminderConfig(EscalationReminderConfig escalationReminderConfig, int? caseLegalHoldID)
        {
            const string methodName = nameof(SaveEscalationAndReminderConfig);
            try
            {
   
                var user = userContextBusiness.GetContext;
                var ReminderAndEscalation = new ReminderAndEscalation                            
                {

                    ReminderConfig = SerializationHelper.XMLSerialize<ReminderConfig>(escalationReminderConfig.ReminderConfig),
                    EscalationConfig = SerializationHelper.XMLSerialize<EscalationConfig>(escalationReminderConfig.EscalationConfig),
                    UUID = Guid.NewGuid(),
                    Status = lookUpBusiness.GetDomainValueById(id: StatusType.Active.id).LookupId,
                    IsDeleted = false,
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow,                 
                    CreatedBy =user.userIdString,
                    ModifiedBy = user.userIdString,
                    CaseLegalHoldID = caseLegalHoldID
                };
                    regionUnitOfWork.ReminderAndEscalationRepository.Create(ReminderAndEscalation);
                    regionUnitOfWork.Save();
                    return ReminderAndEscalation.UUID;
              
            }
            catch (Exception ex)
            {
                logger.LogError("Error Processing {methodName} {ErrorType.Error} {ClassName} ", ex, ErrorType.Error, ClassName);

                throw new CustomError(BaseErrorCodes.UnknownSqlException,
                  BaseErrorProvider.GetErrorString<BaseErrorCodes>(BaseErrorCodes.UnknownSqlException),
                  $"{ClassName} - {methodName}-{ex.StackTrace} - {ex.Message}");
            }
            finally
            {
                logger.LogInformation( "Completed Processing {methodName} {ErrorType.Information} {ClassName}", methodName, ErrorType.Information, ClassName);
            }
        }




        /// <summary>
        /// To Save CaseLegalHodl Document
        /// </summary>
        /// <param name="caseLegalHoldModel"></param>
        /// <param name="caseLegalHoldId"></param>
        /// <param name="dbContext"></param>
        /// <param name="isUpdateCaseLh"></param>
        private   void SaveDocument(CaseLegalHoldModel caseLegalHoldModel,
            int caseLegalHoldId,bool isUpdateCaseLh = false)
        {
            const string methodName = nameof(SaveDocument);
            try
            {
                isUpdateCaseLh = false;
                if (!(caseLegalHoldModel.Documents != null && caseLegalHoldModel.Documents.Count > 0))
                    return;
                foreach (var documentModel in caseLegalHoldModel.Documents)
                {
                    //documentModel = caseLegalHoldModel.Documents.First();
                 //   var caseLhEntityTypeId = EntitiesBusinessLogic.GetEntities()?.FirstOrDefault(entity => entity.UUID == EntityType.CaseLegalHold.GetEnumValue()).ID;
                    DocumentStreamEntity documentStreamEntity = null!;
                    if (documentModel == null)
                        return;
                    //if (documentModel.IsLHNNoticeTemplate)
                    //{
                    //    documentStreamEntity = isUpdateCaseLh ?
                    //       (await regionUnitOfWork.DocumentStreamRepository.GetAsync()).FirstOrDefault(x => x.Uuid == documentModel.ID
                    //       || (x.EntityId == caseLegalHoldId && x.EntityTypeId == 1) && !x.IsDeleted && x.FilePath == caseLegalHoldModel.CaseUniqueID.ToString())
                    //       : null;
                    //}
                    //else
                    //{
                    //    documentStreamEntity = isUpdateCaseLh ?
                    //       (await regionUnitOfWork.DocumentStreamRepository.GetAsync()).FirstOrDefault(x => x.Uuid == documentModel.ID
                    //       )
                    //       : null;
                    //}


                    var entityState = documentStreamEntity == null ? EntityState.Added : EntityState.Modified;

                    var documentStream = DocumentStreamMapper.DocumentEntityMapper(documentModel, documentStreamEntity ??
                        new DocumentStreamEntity(), caseLegalHoldModel.CaseUniqueID, caseLegalHoldId, (int)1,
                        entityState == Model.Query.CustomModels.EntityState.Added, documentModel.FilePath);

                    //dbContext.DocumentStreamEntities.Add(documentStream);
                    //dbContext.Entry(documentStream).State = entityState;
                    //dbContext.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                logger.LogError("Error Processing {methodName} {ErrorType.Error} {ClassName} ", ex, ErrorType.Error, ClassName);

                throw new CustomError(BaseErrorCodes.UnknownSqlException,
                  BaseErrorProvider.GetErrorString<BaseErrorCodes>(BaseErrorCodes.UnknownSqlException),
                  $"{ClassName} - {methodName}-{ex.StackTrace} - {ex.Message}");
            }
            catch (Exception e)
            {
                logger.LogError("Error in {Name} - {Message} /n {Trace}",
       methodName, e.Message, e.StackTrace);
                throw;
            }
        }


        /// <summary>
        /// To store custodians in entity legal hold notice.
        /// </summary>
        /// <param name="caseLegalHoldModel"></param>
        /// <param name="caseLegalHoldId"></param>
        /// <param name="dbContext"></param>
        public void SaveCustInEntityLHN(CaseLegalHoldModel caseLegalHoldModel,int caseLegalHoldId)
        {
            const string methodName = nameof(SaveCustInEntityLHN);
            try
            {
                logger.LogInformation("Started Processing {methodName} {ErrorType.Information} {ClassName}", methodName, ErrorType.Information, ClassName);
            
               
                var user = userContextBusiness.GetContext;
                var caseCustodiansModel = new CaseCustodianViewModel
                {
                    CaseID = caseLegalHoldModel.CaseUniqueID,
           
                };

                var request = new CaseCustodianDetailQuery(caseCustodiansModel.CaseCustodianUniqueID, caseCustodiansModel.CaseCustodianUniqueID);
                var response =  sender.Send(request);               
                SaveCaseCustInEntityLhn(response.Result.ToList(), caseLegalHoldId);
            }
            catch (Exception ex)
            {
                logger.LogError("Error in {Name} - {Message} /n {Trace}",
              methodName, ex.Message, ex.StackTrace);
                throw ;
            }
            finally
            {
                logger.LogInformation( "Completed Processing {methodName} {ErrorType.Information} {ClassName}", methodName, ErrorType.Information, ClassName);
            }
        }






        /// <summary>
        /// Method to save custodian with not initiated status in entity lehal hold notice table
        /// </summary>
        /// <param name="caseCustodianList"></param>
        /// <param name="dbContext"></param>
        /// <param name="caseLegalHoldId"></param>
        public  void SaveCaseCustInEntityLhn(List<CaseCustodianViewModel> caseCustodianList,  int caseLegalHoldId)
        {
            const string methodName = nameof(SaveCaseCustInEntityLhn);
            try
            {
                var user = userContextBusiness.GetContext;
                logger.LogInformation("Started Processing {methodName} {ErrorType.Information} {ClassName}", methodName, ErrorType.Information, ClassName );
                int caseCustEntityType = entityBusiness.GetDomainValueById(id: EntityType.CaseCustodian.id).EntityId;
                if (caseCustEntityType == null)
                    {

                    logger.LogInformation("Error Processing Get EntityTypeID {methodName} {ErrorType.Error} {ClassName}", methodName, ErrorType.Information, ClassName);
                    throw new CustomError(EntityNotificationErrorCodes.InValidEntityType, BaseErrorProvider.GetErrorString<EntityNotificationErrorCodes>(EntityNotificationErrorCodes.InValidEntityType), $"{ClassName} - {nameof(methodName)}");
                    }
                var notInitiatedStatusID = lookUpBusiness.GetDomainValueById(id: DPNStatus.NotInitiated.id).LookupId;
                               
                    if (caseCustodianList.Count > 0)
                    {
                        foreach (var cust in caseCustodianList)
                        {
                            var isRecordExists = (regionUnitOfWork.EntityLegalHoldNoticeRepository.GetAsync()).Result.Any(x => x.EntityTypeID == caseCustEntityType && x.EntityID == cust.CaseCustodianID && x.CaseLegalHoldID == caseLegalHoldId && x.IsDeleted == false);
                            if (!isRecordExists)
                            {
                            var entityLhnModel = EntityLegalHoldNoticeMapper.EntityLegalHoldNoticeFieldMapper(cust.CaseCustodianID, caseCustEntityType, caseLegalHoldId, notInitiatedStatusID);

                            entityLhnModel.CreatedBy = entityLhnModel.ModifiedBy = user.userIdString;

                            entityLhnModel.Status = lookUpBusiness.GetDomainValueById(id: StatusType.Active.id).LookupId;
                            regionUnitOfWork.EntityLegalHoldNoticeRepository.Create(entityLhnModel);
                            regionUnitOfWork.Save();
                            var entityLhnHistoryModel = GenericMapper.Map<EntityLegalHoldNotice, EntityLegalHoldNoticeHistory>(entityLhnModel);
                            entityLhnHistoryModel.EntityLegalHoldNoticeID =regionUnitOfWork.EntityLegalHoldNoticeRepository.GetAsync().Result.FirstOrDefault(x => x.UUID == entityLhnModel.UUID).EntityLegalHoldNoticeID;
                            regionUnitOfWork.EntityLegalHoldNoticeHistory.Create(entityLhnHistoryModel);
                            regionUnitOfWork.Save();
                        }

                        }
                    }
               

            }

            catch (Exception ex)
            {
                logger.LogError("Error in {Name} - {Message} /n {Trace}",
               methodName, ex.Message, ex.StackTrace);
                throw;
            }
            finally
            {
                logger.LogInformation("Error Processing {methodName} {ErrorType.Performance} {ClassName} ", methodName, ErrorType.Performance, ClassName);
              
            }

        }

    }
}
