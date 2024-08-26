using Ligl.LegalManagement.Repository.Domain;
using Ligl.Core.Sdk.Shared.Repository.Region;
using Ligl.Core.Sdk.Shared.Repository.Region.Interface;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace Ligl.LegalManagement.Repository
{
    public sealed  class RegionContext : RegionBaseContext
    {
        private readonly string? _connectionString;

 
        public DbSet<CaseAdditionalField> CaseAdditionalFields { get; set; }
        public  DbSet<LegalCaseDetail> CaseLegalHolds { get; set; }
        public  DbSet<EntityLegalHoldNotice> EntityLegalHoldNotices { get; set; }
        public  DbSet<QuestionnaireTemplateEntity> QuestionnaireTemplateEntities { get; set; }
        public  DbSet<ReminderAndEscalation> ReminderAndEscalation { get; set; }
        public DbSet<MatterNotificationTemplate> MatterNotificationTemplates { get; set; }
        public  DbSet<QuestionnaireTemplateAssociationEntity> QuestionnaireTemplateAssociationEntities { get; set; }
        public  DbSet<EntityApproval> EntityApprovals { get; set; }
        public  DbSet<EntityLegalHoldNoticeHistory> EntityLegalHoldNoticeHistory { get; set; }
        public  DbSet<InterviewEntity> InterviewEntities { get; set; }
        public DbSet<CaseCustodian> CaseCustodians { get; set; }
        public  DbSet<CaseStakeHolder> CaseStakeHolderEntities { get; set; }
        public  DbSet<StakeHolder> StakeHolderEntities { get; set; }
        public DbSet<CaseEntityApprovalBatchEntity> CaseEntityApprovalBatchEntities { get; set; }
        public DbSet<ClientCustodianEntity> ClientCustodianEntities { get; set; }
        public DbSet<EntityLHNAdditionalEntity> EntityLHNAdditionalEntities { get; set; }
        public DbSet<vw_ApprovalBatchEntity> vw_ApprovalBatchEntities { get; set; }
        public DbSet<CasePartyEntity> CasePartyEntities {  get; set; }
        public DbSet<CustodianAdditionalFieldsEntity> custodianAdditionalFieldsEntities { get; set; }        
        public DbSet<CaseDateRangeEntity> CaseDateRangeEntities { get; set; }
        public DbSet<NotificationTemplateContentEntity> notificationTemplateContentEntityRepository { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<QuestionnaireResponse>QuestionnaireResponses { get; set; }
        public DbSet<CaseLegalHoldHistory> CaseLegalHoldHistories { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="RegionContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="regionContextBuilder"></param>
        public RegionContext(DbContextOptions<RegionBaseContext> options, IRegionBaseContextBuilder regionContextBuilder) : base(options, regionContextBuilder)
        {
            _connectionString = regionContextBuilder.GenerateContextAsync().Result;
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// <para>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run. However, it will still run when creating a compiled model.
        /// </para>
        /// <para>
        /// See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and
        /// examples.
        /// </para>
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// The base implementation does nothing.
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        /// <remarks>
        /// <para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// <para>
        /// See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
        /// for more information and examples.
        /// </para>
        /// </remarks>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_connectionString != null)
                optionsBuilder.UseSqlServer(_connectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 1,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null);
                    });
            base.OnConfiguring(optionsBuilder);
        }
    }
}
