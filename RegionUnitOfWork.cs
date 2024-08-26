using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Interface;
using Ligl.Core.Sdk.Shared.Repository.Region;
using Ligl.LegalManagement.Repository.DataAccess;



namespace Ligl.LegalManagement.Repository
{
    /// <summary>
    /// Class for RegionUnitOfWork
    /// </summary>
    /// <seealso cref="Ligl.Core.Sdk.Shared.Repository.Region.RegionBaseUnitOfWork" />
    /// <seealso cref="Ligl.LegalManagement.Repository.Interface.IRegionUnitOfWork" />
    public class RegionUnitOfWork(RegionContext regionContext,

        ICaseAdditionalFieldRepository caseAdditionalFieldRepository, ICaseLegalHoldDetailRepository caseLegalHoldDetailRepository, IEntityLegalHoldNoticeRepository entityLegalHoldNoticeRepository,IQuestionnaireTemplateEntity questionnaireTemplateEntityRespository
        , IMatterNotificationTemplate matterNotificationTemplateRepository, IEntityApprovalRepository entityApprovalRepository, IReminderAndEscalation reminderAndEscalationRepository,IInterviewEntityRepository interviewEntityRepository,
         ICaseCustodianRepository caseCustodianRepository,IEntityLegalHoldNoticeHistoryRepository entityLegalHoldNoticeHistoryRepository,ICaseStakeHolderEntity caseStakeHolderEntity,
         ICaseEntityApprovalBatchEntityRepository caseEntityApprovalBatchEntityRepository, IClientCustodianEntityRepository clientCustodianEntityRepository, IEntityLHNAdditionalEntityRepository entityLHNAdditionalEntityRepository,
         IVWApprovalBatchEntityRepository vw_ApprovalBatchEntityRepository, ICustodianAdditionalFieldsEntityRepository custodianAdditionalFieldsEntityRepository,
         ICasePartyEntityRepository casePartyEntityRepository,ICaseDateRangeEntityRepository caseDateRangeEntityRepository, INotificationTemplateContentEntityRepository notificationTemplateContentEntityRepository,
         IQuestionnaireTemplateAssociationEntityRepository questionnaireTemplateAssociationEntityRepository,IStakeHolderEntityRepository stakeHolderEntity,
         IQuestionnaireResponseRepository questionnaireResponseRepository, ICaseLegalHoldHistoryRepository caseLegalHoldHistoryRepository, IQuestionnaireRepository questionnaireRepository)
        : RegionBaseUnitOfWork(regionContext), IRegionUnitOfWork
    {
        private bool _disposed;
        private readonly RegionContext _regionContext = regionContext;

        public ICaseAdditionalFieldRepository CaseAdditionalFieldRepository { get; set; } = caseAdditionalFieldRepository;
          public ICaseLegalHoldDetailRepository CaseLegalHoldDetailRepository { get; set; } = caseLegalHoldDetailRepository;
        public IEntityLegalHoldNoticeRepository EntityLegalHoldNoticeRepository { get; set; } = entityLegalHoldNoticeRepository;
        public IQuestionnaireTemplateEntity QuestionnaireTemplateEntityRespository { get; set; } = questionnaireTemplateEntityRespository;
        public IMatterNotificationTemplate MatterNotificationTemplateRepository  { get; set; } = matterNotificationTemplateRepository;
        public IReminderAndEscalation ReminderAndEscalationRepository { get; set; } = reminderAndEscalationRepository;
        public IEntityApprovalRepository EntityApprovalRepository { get; set; } = entityApprovalRepository;
        public IInterviewEntityRepository InterviewEntityRepository { get; set; } = interviewEntityRepository;
        public ICaseCustodianRepository CaseCustodianRepository { get; set; } = caseCustodianRepository;
        public ICaseStakeHolderEntity caseStakeHolderEntity { get; set; } = caseStakeHolderEntity;
        public IEntityLegalHoldNoticeHistoryRepository EntityLegalHoldNoticeHistory { get; set; } = entityLegalHoldNoticeHistoryRepository;
        public IQuestionnaireTemplateAssociationEntityRepository questionnaireTemplateAssociationEntityRepository { get; set; } = questionnaireTemplateAssociationEntityRepository;
        public IStakeHolderEntityRepository stakeHolderEntity { get; set; } = stakeHolderEntity;
        public ICaseEntityApprovalBatchEntityRepository CaseEntityApprovalBatchEntityRepository { get; set; } = caseEntityApprovalBatchEntityRepository;
        public IClientCustodianEntityRepository ClientCustodianEntityRepository {  get; set; }=clientCustodianEntityRepository;
        public IEntityLHNAdditionalEntityRepository EntityLHNAdditionalEntityRepository { get; set; } = entityLHNAdditionalEntityRepository;
        public IVWApprovalBatchEntityRepository vw_ApprovalBatchEntityRepository { get; set; }=vw_ApprovalBatchEntityRepository;
        public ICustodianAdditionalFieldsEntityRepository custodianAdditionalFieldsEntityRepository { get; set; } = custodianAdditionalFieldsEntityRepository;
        public ICasePartyEntityRepository casePartyEntityRepository { get; set; } = casePartyEntityRepository;
        public ICaseDateRangeEntityRepository CaseDateRangeEntityRepository { get; set; } = caseDateRangeEntityRepository;
        public INotificationTemplateContentEntityRepository notificationTemplateContentEntityRepository { get; set; }=notificationTemplateContentEntityRepository;
        public IQuestionnaireRepository questionnaireRepository { get; set; }=questionnaireRepository;
        public IQuestionnaireResponseRepository questionnaireResponseRepository { get; set; } = questionnaireResponseRepository;
        public ICaseLegalHoldHistoryRepository CaseLegalHoldHistoryRepository { get; set; } = caseLegalHoldHistoryRepository;
      
        /// <summary>
        /// Saves this instance.
        /// </summary>
        public override void Save()
        {
            _regionContext.SaveChanges();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        public override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _regionContext.Dispose();
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            _regionContext.Dispose();
        }
    }
}
