using Ligl.LegalManagement.Model.Query.CustomModels;
using Ligl.LegalManagement.Model.Query;
using MediatR;
using Microsoft.Extensions.Logging;
using Ligl.LegalManagement.Repository.Interface;
using Microsoft.AspNetCore.Http;
using NotificationTemplateViewModel = Ligl.LegalManagement.Model.Query.CustomModels.NotificationTemplateViewModel;
using Ligl.LegalManagement.Model.Common;
using Ligl.LegalManagement.Model.Query.Constants;
using Microsoft.Data.SqlClient;
using Ligl.Core.Sdk.Shared.Business.Region.Cache.Interface;

namespace Ligl.LegalManagement.Business.Query
{
 
    /// <summary>
    /// Class for EmailTemplatesQueryDetailHandler
    /// </summary>
    /// <seealso cref="EmailTemplatesQueryDetails" />
    public class EmailTemplatesQueryDetailHandler(IRegionUnitOfWork regionUnitOfWork, ILookUpBusiness lookUpBusiness, ILogger<EmailTemplatesQueryDetailHandler> logger) : IRequestHandler<EmailTemplatesQueryDetails, IQueryable<NotificationTemplateViewModel>>
    {
        private const string ClassName = nameof(EmailTemplatesQueryDetailHandler);

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="AccessViolationException">Invalid user exception</exception>
        public async Task<IQueryable<NotificationTemplateViewModel>> Handle(EmailTemplatesQueryDetails request, CancellationToken cancellationToken)
        {
            var emailTemplateTypeUniqueID = NotificationTemplateTypes.Email;
            return   GetNotificationTemplates()?.Result.Where(notification =>
               notification.TemplateTypeUniqueID == emailTemplateTypeUniqueID);

        }

        /// <inheritdoc />
        /// <summary>
        /// To Get the List of Notification Templates 
        /// </summary>
        /// <returns></returns>
        public async Task<IQueryable<NotificationTemplateViewModel>> GetNotificationTemplates(bool isFromUserLogin = false)
        {
            const string methodName = nameof(GetNotificationTemplates);
            try
            {
                isFromUserLogin = false;
                // implementation pending
                // var languageId = AppConfigHelper.GetLanguageAppConfigLookUpId(isFromUserLogin);
                var languageId = 1;
                var defaultLanguageId = lookUpBusiness.GetDomainValueById(primaryId: LookUpEnums.DefaultLanguageSetting.primaryId).LookupId;


                var currentLanguageContent = from notification in await regionUnitOfWork.MatterNotificationTemplateRepository.GetAsync()
                                             join content in await regionUnitOfWork.notificationTemplateContentEntityRepository.GetAsync()
                                                 on new { notification.NotificationTemplateID, languageId, isDeleted = false } equals
                                                 new
                                                 {
                                                     content.NotificationTemplateID,
                                                     languageId = content.LanguageID,
                                                     isDeleted = content.IsDeleted
                                                 }
                                                 into tempContent
                                             from notificationContent in tempContent.DefaultIfEmpty()
                                             where notification.IsDeleted==false
                                             select new { notification, notificationContent };


                var defaultLanguageContent = (from notification in currentLanguageContent
                                              join content in await regionUnitOfWork.notificationTemplateContentEntityRepository.GetAsync()
                                                  on new
                                                  {
                                                      notification.notification.NotificationTemplateID,
                                                     defaultLanguageId,
                                                      isDeleted = false
                                                  } equals
                                                  new
                                                  {
                                                      content.NotificationTemplateID,
                                                      defaultLanguageId = content.LanguageID,
                                                      isDeleted = content.IsDeleted
                                                  }
                                                  into tempContent
                                              from notificationContent in tempContent.DefaultIfEmpty()
                                              where notification.notificationContent == null
                                              select new { notification.notification, notificationContent })
                                .Where(notification => notification.notificationContent != null);

                var notificationTemplateResult = from notificationTemplate in currentLanguageContent
                        .Where(x => x.notificationContent != null).Union(defaultLanguageContent)
                                                 join lookupCategory in await regionUnitOfWork.LookupRepository.GetAsync()
                                                     on
                                                     new
                                                     {
                                                         LookupID = notificationTemplate.notification.CategoryID,
                                                         IsDeleted = false
                                                     } equals new
                                                     {
                                                         LookupID = lookupCategory.LookupId,
                                                         lookupCategory.IsDeleted
                                                     }
                                                 join lookUpStatus in await regionUnitOfWork.LookupRepository.GetAsync()
                                                    on notificationTemplate.notification.Status equals lookUpStatus.LookupId
                                                 join lookUpTemplateType in await regionUnitOfWork.LookupRepository.GetAsync()
                                                 on notificationTemplate.notification.TemplateTypeID equals lookUpTemplateType.LookupId
                                                 join document in await regionUnitOfWork.DocumentStreamRepository.GetAsync()
                                                 on new
                                                 {
                                                     DocumentEntityID = notificationTemplate.notification.NotificationTemplateID,
                                                     DocumentEntityTypeID = (int)EntityTypes.NotificationTemplate,
                                                     IsDeleted = false
                                                 }
                                                  equals new
                                                  {
                                                      DocumentEntityID = document.EntityId,
                                                      DocumentEntityTypeID = document.EntityTypeId,
                                                      document.IsDeleted
                                                  }
                                                 into temdocument
                                                 from documentResult in temdocument.DefaultIfEmpty()
                                                 where notificationTemplate.notification.IsDeleted == false
                                                 select new NotificationTemplateViewModel
                                                 {
                                                     ID = notificationTemplate.notification.UUID,
                                                     NotificationTemplateContentUniqueID = notificationTemplate.notificationContent.UUID,
                                                     NotificationID = notificationTemplate.notification.NotificationTemplateID,
                                                     CategoryID = lookupCategory.LookupId,
                                                     CategoryUniqueID = lookupCategory.Uuid,
                                                     CategoryName = lookupCategory.Name,
                                                     Name = notificationTemplate.notification.Name,
                                                     Subject = notificationTemplate.notificationContent.Subject,
                                                     Content = notificationTemplate.notificationContent.Content,
                                                     TemplateTypeUniqueID = lookUpTemplateType.Uuid,
                                                     TemplateTypeName = lookUpTemplateType.Name,
                                                     StatusUniqueID = lookUpStatus.Uuid,
                                                     StatusName = lookUpStatus.Name,
                                                     IsDeleted = notificationTemplate.notification.IsDeleted,
                                                    // EntityUniqueID = ((await regionUnitOfWork.LookupEntityRepository.GetAsync()).FirstOrDefault(x => x.LookupEntityId == notificationTemplate.notification.EntityTypeID).EntityID == 25) ? adminContext.AdminLegalHolds.FirstOrDefault(x => x.CaseLegalHoldID == notificationTemplate.notification.EntityID).UUID : adminContext.AdminCases.FirstOrDefault(x => x.CaseID == notificationTemplate.notification.EntityID).UUID,
                                                   //  EntityTypeUniqueID = adminContext.AdminEntities.FirstOrDefault(x => x.EntityID == notificationTemplate.notification.EntityTypeID).UUID,
                                                     Documents = temdocument.Select(document => new DocumentStreamModel
                                                     {
                                                         ID = documentResult.Uuid != Guid.Empty ? documentResult.Uuid :Guid.Empty ,
                                                         Name = documentResult.Name,
                                                         Extension = documentResult.Extension,
                                                         Comments = documentResult.Comments,
                                                         FileSize = documentResult.FileSize,
                                                         FileData = documentResult.FileData
                                                     }).ToList()
                                                 };


                return notificationTemplateResult;
            }
     
            catch (Exception ex)
            {
                logger.LogError("Error in {Name} - {Message} /n {Trace}",
                   methodName, ex.Message, ex.StackTrace);
                throw;
            }
        }


    }
}
