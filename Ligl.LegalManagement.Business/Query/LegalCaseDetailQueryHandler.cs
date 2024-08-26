using Ligl.Core.Sdk.Common.Helper;
using Ligl.Core.Sdk.Shared.Business.Region.Cache.Interface;
using Ligl.LegalManagement.Model.Common;
using Ligl.LegalManagement.Model.Query;
using Ligl.LegalManagement.Model.Query.CustomModels;
using Ligl.LegalManagement.Repository.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Xml;
using EntityTypes = Ligl.LegalManagement.Model.Query.Constants.EntityTypes;



namespace Ligl.LegalManagement.Business.Query
{
    /// <summary>
    /// Class for LegalCaseDetailQueryByIdHandler
    /// </summary>
    /// <seealso cref="LegalCaseDetailQueryById" />
    public class LegalCaseDetailQueryHandler(IRegionUnitOfWork regionUnitOfWork, ILookUpBusiness lookUpBusiness, ILogger<LegalCaseDetailQueryHandler> logger) : IRequestHandler<LegalDetailQuery, List<CaseLegalHoldModel>>
    {
        private const string ClassName = nameof(LegalCaseDetailQueryHandler);

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="AccessViolationException">Invalid user exception</exception>
        public async Task <List<CaseLegalHoldModel>> Handle(LegalDetailQuery request, CancellationToken cancellationToken)
        {
            const string methodName = $"{ClassName} - {nameof(Handle)}";
            try
            {
                logger.LogInformation(message: "Started execution of {methodName}", methodName);
                var notInitiatedStatus = lookUpBusiness.GetDomainValueById(id: DPNStatus.NotInitiated.id).LookupId ;
                var result = (from caseLegalHold in await regionUnitOfWork.CaseLegalHoldDetailRepository.GetAsync()
                              join entityLHN in await regionUnitOfWork.EntityLegalHoldNoticeRepository.GetAsync()
                              on caseLegalHold.CaseLegalHoldID equals entityLHN.CaseLegalHoldID into entityLHNResult
                              from entityLHN in entityLHNResult.DefaultIfEmpty()
                              where caseLegalHold.IsDeleted == false
                              group entityLHN by caseLegalHold.CaseLegalHoldID into grouped
                              select new { CaseLegalHoldID = grouped.Key, Count = grouped.Count(t => t.LHNStatusID != notInitiatedStatus) });

                var caseLegalHoldResult = (from caseLegalHold in await regionUnitOfWork.CaseLegalHoldDetailRepository.GetAsync()
                                           join entityLHNResult in result on
                                           caseLegalHold.CaseLegalHoldID equals entityLHNResult.CaseLegalHoldID
                                           join dbcase in await regionUnitOfWork.CaseRepository.GetAsync()
                                           on caseLegalHold.CaseID equals dbcase.CaseId 
                                           join custodianQuestTemplate in await regionUnitOfWork.QuestionnaireTemplateEntityRespository.GetAsync()
                                            on new
                                            {
                                                QuestionnaireTemplateID = caseLegalHold.CustodianQuestionnaireTemplateID.Value,
                                                IsDeleted = false
                                            }
                                            equals new
                                            {
                                                custodianQuestTemplate.QuestionnaireTemplateID,
                                                custodianQuestTemplate.IsDeleted
                                            }
                                            into custodianQuestresult
                                           from custodianQuestTem in custodianQuestresult.DefaultIfEmpty()
                                           join stakeHolderQuestTemplate in await regionUnitOfWork.QuestionnaireTemplateEntityRespository.GetAsync()
                                            on new
                                            {
                                                QuestionnaireTemplateID =caseLegalHold.StakeHolderQuestionnaireTemplateID.Value,
                                                IsDeleted = false
                                            }
                                            equals new
                                            {
                                                stakeHolderQuestTemplate.QuestionnaireTemplateID ,
                                                 stakeHolderQuestTemplate.IsDeleted 
                                            }
                                            into stakeHolderQuestresult
                                           from stakeHolderQuestTem in stakeHolderQuestresult.DefaultIfEmpty()
                                           join notificationTemplate in await regionUnitOfWork.MatterNotificationTemplateRepository.GetAsync()
                                            on caseLegalHold.LHNCustodianTemplateID equals notificationTemplate.NotificationTemplateID
                                           join stakeholderTemplate in await regionUnitOfWork.MatterNotificationTemplateRepository.GetAsync()
                                           on new
                                           {
                                               LHNStakeHolderTemplateID =(int)caseLegalHold.LHNStakeHolderTemplateID ,
                                               IsDeleted = false
                                           } equals new
                                           {
                                               LHNStakeHolderTemplateID = stakeholderTemplate.NotificationTemplateID ,
                                               IsDeleted = stakeholderTemplate.IsDeleted ?? false
                                           }
                                           into stakeHolderTempResult
                                           from stakeHolderTemp in stakeHolderTempResult.DefaultIfEmpty()

                                           join entityApproval in await regionUnitOfWork.EntityApprovalRepository.GetAsync()
                                           on new
                                           {
                                               EntityID = caseLegalHold.CaseLegalHoldID ,
                                               IsDeleted = false,
                                               EntityTypeID = (int)EntityTypes.CaseLegalHold
                                           }
                                            equals new
                                            {
                                                EntityID = entityApproval.EntityID ,
                                                IsDeleted = entityApproval.IsDeleted,
                                                EntityTypeID = entityApproval.EntityTypeID
                                            }
                                            into approvalTempResult
                                           from approvalTemp in approvalTempResult.DefaultIfEmpty()
                                        
                                           join userLogin in await regionUnitOfWork.UserLoginRepository.GetAsync()
                                            on new
                                            {
                                                ApprovalUserID =approvalTemp.ApprovalUserID.Value,
                                                IsDeleted = false
                                            }
                                            equals new
                                            {
                                                ApprovalUserID = userLogin.UserLoginId ,
                                                IsDeleted =userLogin.IsDeleted
                                            }
                                            into userTempResult
                                           from userTemp in userTempResult.DefaultIfEmpty()

                                           join ua in await regionUnitOfWork.UserLoginRepository.GetAsync()
                                            on new
                                            {
                                                CreatedBy = approvalTemp.ApprovalUserID.ToString() ?? string.Empty,
                                                IsDeleted = (bool?)false
                                            }
                                           equals new
                                           {
                                               CreatedBy = ua.Uuid.ToString() ?? string.Empty,
                                               IsDeleted =(bool?) ua.IsDeleted
                                           }
                                           into userLoginTempResult
                                           from userLoginTemp in userLoginTempResult.DefaultIfEmpty()

                                           join lookups in await regionUnitOfWork.LookupEntityRepository.GetAsync()
                                           on new
                                           {
                                               LookupID = approvalTemp.ApprovalStatusID ,
                                               IsDeleted = false

                                           } equals new
                                           {
                                               LookupID = (int)lookups.LookupEntityId,
                                               IsDeleted =false
                                           }
                                           into lookupTempResult
                                           from lookupTemp in lookupTempResult.DefaultIfEmpty()

                                           join remEsctempalte in await regionUnitOfWork.ReminderAndEscalationRepository.GetAsync()
                                            on caseLegalHold.CaseLegalHoldID equals remEsctempalte.CaseLegalHoldID into remEscresult
                                           from remEscresults in remEscresult.DefaultIfEmpty()

                                           join caseLDR in await regionUnitOfWork.DateRangeRepository.GetAsync()
                                           on new
                                           {
                                               DateRangeID = (long)caseLegalHold.DateRangeID,
                                               IsDeleted =(bool?) false
                                           } equals new
                                           {
                                               DateRangeID = caseLDR.DateRangeId,
                                               IsDeleted = (bool?)caseLDR.IsDeleted
                                           }
                                           join caseLKW in await regionUnitOfWork.KeyWordRepository.GetAsync()
                                           on new
                                           {
                                               KeyWordID = (long)caseLegalHold.KeyWordID,
                                               IsDeleted = (bool?)false
                                           } equals new
                                           {
                                               KeyWordID = caseLKW.KeyWordsId,
                                              
                                             IsDeleted = (bool?)caseLKW.IsDeleted
                                           }

                                           where notificationTemplate.IsDeleted == false && caseLegalHold.IsDeleted == false
                                           && dbcase.IsDeleted == false && remEscresults.IsDeleted == false
                                             && caseLegalHold.CaseID== 4589
                                           select new CaseLegalHoldModel
                                           {
                                               LegalHoldName = caseLegalHold.LegalHoldName,
                                               UUID = caseLegalHold.UUID,
                                               CaseLegalHoldID = caseLegalHold.CaseLegalHoldID,
                                               CaseUniqueID = dbcase.Uuid,
                                               LHNCustodianTemplateUniqueID = notificationTemplate.UUID,
                                               LHNStakeHolderTemplateUniqueID = stakeHolderTemp != null ? stakeHolderTemp.UUID : Guid.Empty,
                                              RemAndEscTemplateUniqueID = remEscresults.UUID,
                                               CustodianQuestionnaireTemplateUniqueID = custodianQuestTem != null ? custodianQuestTem.UUID : Guid.Empty,
                                               CustodianTemplateName = notificationTemplate.Name,
                                               StakeHolderTemplateName = stakeHolderTemp != null ? stakeHolderTemp.Name : null,
                                               CustodianQuestionnaireTemplateName = custodianQuestTem != null ? custodianQuestTem.QuestionnaireTemplateName : null,
                                               StakeHolderQuestionnaireTemplateUniqueID = stakeHolderQuestTem != null ? stakeHolderQuestTem.UUID : Guid.Empty,
                                               StakeHolderQuestionnaireTemplateName = stakeHolderQuestTem != null ? stakeHolderQuestTem.QuestionnaireTemplateName : null,
                                               IsConfigEditable = entityLHNResult.Count == 0,
                                               CreatedOn = DateTime.Today,
                                               StartDate = caseLDR.StartDate,
                                               EndDate = caseLDR.EndDate,
                                               KeyWords = caseLKW.KeyWords,
                                               NumberOfDays = caseLegalHold.NumberOfDays,
                                               EscalationAndReminderConfigDetails = new EscalationReminderConfigModelDetails
                                               {
                                                    UUID = remEscresults.UUID,
                                                   ReminderConfig = remEscresults.ReminderConfig,
                                                   EscalationConfig = remEscresults.EscalationConfig,
                                                   EscalationReminderConfig = new EscalationReminderConfig
                                                   {
                                                       EscalationConfig = new EscalationConfig(),
                                                       ReminderConfig = new ReminderConfig()
                                                   }
                                               },
                                               EntityApprovalDetails = new EntityApprovalModel
                                               {
                                                   EntityApprovalID = approvalTemp != null ? approvalTemp.EntityApprovalID : default(int),
                                                   UUID = approvalTemp != null ? approvalTemp.UUID : Guid.Empty,
                                                   EntityTypeID = approvalTemp != null ? approvalTemp.EntityTypeID : default(int),
                                                   EntityID = approvalTemp != null ? approvalTemp.EntityID : default(int),
                                                   Status = approvalTemp != null ? approvalTemp.Status : default(int),
                                                   ApprovalStatusID = approvalTemp != null ? approvalTemp.ApprovalStatusID : default(int),
                                                   ApprovalStatusUniqueID = lookupTemp != null ? (Guid)lookupTemp.RowId : Guid.Empty,
                                                   ApprovalUserUniqueID = userTemp != null ? userTemp.Uuid : default(Guid),
                                                   ApprovalUserID = userTemp != null ? userTemp.UserLoginId : default(int),
                                                   CreatedBy = userTemp != null ? userTemp.FullName : null,
                                                  // ApprovedOn = approvalTemp.ApprovedOn,
                                                   Comments = approvalTemp != null ? approvalTemp.Comments : null,
                                               },
                                               ApprovalStatusName = lookupTemp != null ? lookupTemp.Name : (lookupTemp.Name != null ? lookupTemp.Name : "Not Initiated"),
                                               NotificationUniqueID = approvalTemp != null ? approvalTemp.UUID : default(Guid),
                                           }).ToList();
              


                foreach (var caseLegalHoldDetails in caseLegalHoldResult)
                {
                    var documents = (await regionUnitOfWork.DocumentStreamRepository.GetAsync()).Where(dsEntity => dsEntity.IsDeleted == false && dsEntity.EntityId == caseLegalHoldDetails.CaseLegalHoldID
                    && dsEntity.EntityTypeId == (int)EntityTypes.CaseLegalHold).
                    Select(x => new DocumentStreamModel
                    {
                        ID = x.Uuid,
                        Name = x.Name,
                        Extension = x.Extension,
                        Comments = x.Comments,
                        FileSize =(decimal)x.FileSize,
                        FilePath = x.FilePath,
                        IsLHNNoticeTemplate = ((x.FilePath == caseLegalHoldDetails.CaseUniqueID.ToString()) ? true : false)
                    }).
                    ToList();
                    caseLegalHoldDetails.NoOfAttachments = documents.Count();
                    caseLegalHoldDetails.Documents = new List<DocumentStreamModel>();
                  
                    XmlDocument? xmlDocument = null;
                    caseLegalHoldDetails.EscalationAndReminderConfigDetails.EscalationReminderConfig.ReminderConfig = SerializationHelper.XmlToObject<ReminderConfig>(xmlDocument,caseLegalHoldDetails.EscalationAndReminderConfigDetails.ReminderConfig);
                    caseLegalHoldDetails.EscalationAndReminderConfigDetails.EscalationReminderConfig.EscalationConfig = SerializationHelper.XmlToObject<EscalationConfig>(xmlDocument,caseLegalHoldDetails.EscalationAndReminderConfigDetails.EscalationConfig);

                }


                return caseLegalHoldResult;



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
    }
}
