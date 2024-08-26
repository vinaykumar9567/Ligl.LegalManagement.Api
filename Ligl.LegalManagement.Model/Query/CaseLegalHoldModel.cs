using Ligl.LegalManagement.Model.Query.CustomModels;
using System.Runtime.Serialization;
namespace Ligl.LegalManagement.Model.Query
{

    [DataContract]
    public partial class CaseLegalHoldModel 
    {

        [DataMember(Name = "custodianTemplateName")]
        public string? CustodianTemplateName { get; set; }

        [DataMember(Name = "stakeHolderTemplateName")]
        public string? StakeHolderTemplateName { get; set; }

        [DataMember(Name = "custodianQuestionnaireTemplateName")]
        public string? CustodianQuestionnaireTemplateName { get; set; }

        [DataMember(Name = "stakeHolderQuestionnaireTemplateName")]
        public string? StakeHolderQuestionnaireTemplateName { get; set; }

        [DataMember(Name = "isConfigEditable")]
        public bool IsConfigEditable { get; set; }

        [DataMember(Name = "RemAndEscTemplateUniqueID")]
        public Guid RemAndEscTemplateUniqueID { get; set; }

        [DataMember(Name = "documents")]
        public List<DocumentStreamModel> Documents { get; set; }

        [DataMember(Name = "approvalStatusName")]
        public string? ApprovalStatusName { get; set; }

        [DataMember(Name = "entityApprovalDetails")]
        public EntityApprovalModel EntityApprovalDetails { get; set; }

        [DataMember(Name = "notificationUniqueID")]
        public Guid? NotificationUniqueID { get; set; }

        [DataMember(Name = "noOfAttachments")]

        public int? NoOfAttachments { get; set; }

        [DataMember(Name = "isLHNTemplatesEditable")]

        public bool? IsLHNTemplatesEditable { get; set; }

        [DataMember(Name = "startDate")]
        public DateTime? StartDate { get; set; }

        [DataMember(Name = "endDate")]
        public DateTime? EndDate { get; set; }

        [DataMember(Name = "keyWords")]
        public string? KeyWords { get; set; }

        [DataMember(Name = "caseLegalHoldUniqueID")]
        public Guid? CaseLegalHoldUniqueID { get; set; }

        [DataMember(Name = "preservationType")]
        public int? PreservationType { get; set; }

        [DataMember(Name = "caseCustodianDataSourceGuid")]
        public Guid? CaseCustodianDataSourceGuid { get; set; }

        [DataMember(Name = "dateRangesInRange")]
        public bool? DateRangesInRange { get; set; }

        [DataMember(Name = "escalationAndReminderConfigDetails")]
        public EscalationReminderConfigModelDetails EscalationAndReminderConfigDetails { get; set; }
    }



    
    public partial class CaseLegalHoldModel : CaseLegalHold
    {
        [DataMember(Name = "caseUniqueID")]
        public Guid CaseUniqueID { get; set; }

        [DataMember(Name = "lHNCustodianTemplateUniqueID")]
        public Guid LHNCustodianTemplateUniqueID { get; set; }

        [DataMember(Name = "custodianQuestionnaireTemplateUniqueID")]
        public Guid CustodianQuestionnaireTemplateUniqueID { get; set; }

        [DataMember(Name = "lHNStakeHolderTemplateUniqueID")]
        public Guid LHNStakeHolderTemplateUniqueID { get; set; }

        [DataMember(Name = "statusUniqueID")]
        public  Guid StatusUniqueID { get; set; }

        [DataMember(Name = "stakeHolderQuestionnaireTemplateUniqueID")]
        public  Guid StakeHolderQuestionnaireTemplateUniqueID { get; set; }
    }

    [Serializable]
    public class EscalationReminderConfigModelDetails : ReminderAndEscalationModel
    {
        [DataMember(Name = "escalationReminderConfig")]
        public EscalationReminderConfig EscalationReminderConfig { get; set; }
    }
}
