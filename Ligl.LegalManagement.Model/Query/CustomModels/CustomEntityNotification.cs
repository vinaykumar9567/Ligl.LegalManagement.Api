using Ligl.Model.Model.Query.CustomModels;
namespace Ligl.LegalManagement.Model.Query.CustomModels
{

    /// <summary>
    /// BaseEntityNotification
    /// </summary>
    public class BaseEntityNotification
    {
        public Guid? CaseLegalHoldUniqueID { get; set; }
        public Guid? EmailTemplateUniqueID { get; set; }
        public Guid? AlertTemplateUniqueID { get; set; }
        public List<EntityValues> NotificationTypes { get; set; } = null!;
        public Guid? EntityTypeUniqueID { get; set; }
        public Guid? ActionUniqueID { get; set; }
        public bool SendLhnOnRelease { get; set; }
        public List<CustomEntityNotification> EntityNotifications { get; set; } = null!;
    }
    public class CustomEntityNotification //: BaseEntity
    {
        public Guid? CaseUniqueID { get; set; }
        public Guid? ToUserUniqueID { get; set; }
        public Guid? EntityStatusUniqueID { get; set; }
        public Guid EntityUniqueID { get; set; }
        public  bool IsDeleted { get; set; }
        public string?   Comments { get; set; }
        public EmailNotificationRequest EmailMapperRequest { get; set; } = null!;
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? Reason { get; set; }

        public int? ReminderCount { get; set; }
        public int? EscalationCount { get; set; }
        public Guid? AcknowledgementDrivenType { get; set; }

        public int? AcknowledgedType { get; set; }

        public string? AcknowledgeType { get; set; }

        public bool IsProxy { get; set; }

        public string? ReasonForProxyAcknowledged { get; set; }


    }

    /// <summary>
    /// EntityLegalHoldNoticeNotification
    /// </summary>
    public class EntityLegalHoldNoticeNotification : BaseEntityNotification
    {
        public string? CCEmail { get; set; }
        public string? ToEmail { get; set; }
        public Guid? QuestionnaireTemplateUniqueID { get; set; }
        public Guid? CaseUniqueID { get; set; }
        public List<string> StakeholdersList { get; set; } = null!;

        public int? AcknowledgedType { get; set; }

        public bool IsProxy { get; set; }

        public string? ReasonForProxyAcknowledged { get; set; }

        public string? Reason { get; set; }


    }

    ///<summary>
    ///RequestConatctNotification
    /// </summary>
    /// 

    /// <summary>
    /// EntityLHNResponse
    /// </summary>
    public class EntityLHNResponse : CustomEntityNotification
    {
        public List<CustomError>? CustomErrors { get; set; }

    }
}
