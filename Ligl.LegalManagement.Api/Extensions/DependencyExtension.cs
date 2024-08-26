using Ligl.LegalManagement.Api.Middleware;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.DataAccess;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ligl.Core.Sdk.Shared.Repository.Region.Interface;
using Ligl.Core.Sdk.Shared.Repository.Region;
using Ligl.Core.Sdk.Shared.Repository.Master.Interface;
using Ligl.Core.Sdk.Shared.Repository.Master;
using Ligl.Core.Sdk.Shared.Repository.Store.Interface;
using Ligl.Core.Sdk.Shared.Repository.Store;
using Ligl.LegalManagement.Repository.Interface;
using Ligl.LegalManagement.Repository;
using Ligl.Core.Sdk.Shared.Business.Region.Cache.Interface;
using Ligl.Core.Sdk.Shared.Business.Region.Cache;
using Ligl.Core.Sdk.Shared.Business;

namespace Ligl.LegalManagement.Api.Extensions
{

    /// <summary>
    /// Class for Dependency Extension
    /// </summary>
    public static class DependencyExtension
    {
        /// <summary>
        /// Adds the dependency.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddDependency(this IServiceCollection services)
        {
            var customAuthorizeService = new ServiceDescriptor(typeof(CustomAuthorization), new CustomAuthorization());

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

          
            services.AddScoped<IMasterBaseUnitOfWork, MasterBaseUnitOfWork>();
            services.AddScoped<IRegionBaseContextBuilder, RegionBaseContextBuilder>();
            services.AddScoped<IRegionBaseUnitOfWork, RegionBaseUnitOfWork>();
            services.AddScoped<IRegionUnitOfWork, RegionUnitOfWork>();
            services.AddScoped<IStoreBaseUnitOfWork, StoreBaseUnitOfWork>();
            services.AddScoped<IEntityBusiness, EntityBusiness>();
            services.AddScoped<IEntityTypeBusiness, EntityTypeBusiness>();
            services.AddScoped<ILookUpBusiness, LookUpBusiness>();
            services.AddScoped<IRoleBusiness, RoleBusiness>();
            services.AddScoped<IUserContextBusiness, UserContextBusiness>();
            services.Add(customAuthorizeService);
     
            services.AddScoped<ICaseAdditionalFieldRepository, CaseAdditionalFieldRepository>();
            services.AddScoped<ICaseLegalHoldDetailRepository, CaseLegalHoldDetailRepository>();

            services.AddScoped<IEntityLegalHoldNoticeRepository, EntityLegalHoldNoticeRepository>();
            services.AddScoped<IQuestionnaireTemplateEntity, QuestionnaireTemplateEntityRespository>();
            services.AddScoped<IMatterNotificationTemplate, MatterNotificationTemplateRepository>();
            services.AddScoped<IEntityApprovalRepository, EntityApprovalRepository>();
            services.AddScoped<IReminderAndEscalation, ReminderAndEscalationRepository>();
            services.AddScoped<IInterviewEntityRepository, InterviewEntityRepository>();
            services.AddScoped<ICaseCustodianRepository, CaseCustodianRepository>();
            services.AddScoped<IEntityLegalHoldNoticeHistoryRepository, EntityLegalHoldNoticeHistoryRepository>();
            services.AddScoped<ICaseStakeHolderEntity, CaseStakeHolderEntityRepository>();
            services.AddScoped<IQuestionnaireTemplateAssociationEntityRepository, QuestionnaireTemplateAssociationEntityRepository>();
            services.AddScoped<IStakeHolderEntityRepository, StakeHolderEntityRepository>();
            services.AddScoped<IClientCustodianEntityRepository, ClientCustodianEntityRepository>();
            services.AddScoped<IEntityLHNAdditionalEntityRepository, EntityLHNAdditionalEntityRepository>();
            services.AddScoped<IVWApprovalBatchEntityRepository, VWApprovalBatchEntityRepository>();
            services.AddScoped<ICaseEntityApprovalBatchEntityRepository, CaseEntityApprovalBatchEntityRepository>();
            services.AddScoped<ICustodianAdditionalFieldsEntityRepository, CustodianAdditionalFieldsEntityRepository>();    
            services.AddScoped<ICasePartyEntityRepository,CasePartyEntityRepository>();
            services.AddScoped<ICaseDateRangeEntityRepository,CaseDateRangeEntityRepository>();
            services.AddScoped<INotificationTemplateContentEntityRepository, NotificationTemplateContentEntityRepository>();   
            services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();
            services.AddScoped<IQuestionnaireResponseRepository, QuestionnaireResponseRepository>();
            services.AddScoped<ICaseLegalHoldHistoryRepository, CaseLegalHoldHistoryRepository>();
        }
    }
}
