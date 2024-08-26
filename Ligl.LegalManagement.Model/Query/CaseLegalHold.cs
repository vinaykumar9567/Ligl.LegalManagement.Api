using System.Runtime.Serialization;
namespace Ligl.LegalManagement.Model.Query
{
    [DataContract]
    public partial class CaseLegalHold //: BaseEntity
    {
        [DataMember(Name = "caseLegalHoldID")]
        public int CaseLegalHoldID { get; set; }

        [DataMember(Name = "clientID")]

        public int ClientID { get; set; }

        [DataMember(Name = "UUID")]
        public Guid UUID { get; set; }

        [DataMember(Name = "legalHoldName")]
        public string? LegalHoldName { get; set; }

        [DataMember(Name = "caseID")]
        public int CaseID { get; set; }
        [DataMember(Name = "lHNCustodianTemplateID")]
        public int LHNCustodianTemplateID { get; set; }

        [DataMember(Name = "custodianQuestionnaireTemplateID")]
        public int? CustodianQuestionnaireTemplateID { get; set; }

        [DataMember(Name = "lHNStakeHolderTemplateID")]
        public int? LHNStakeHolderTemplateID { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "stakeHolderQuestionnaireTemplateID")]
        public int? StakeHolderQuestionnaireTemplateID { get; set; }

        [DataMember(Name = "numberOfDays")]
        public int? NumberOfDays { get; set; }

        [DataMember(Name = "dateRangeID")]
        public long? DateRangeID { get; set; }

        [DataMember(Name = "keyWordID")]
        public long? KeyWordID { get; set; }

        [DataMember(Name = "createdOn")]
        public DateTime CreatedOn { get; set; }

        [DataMember(Name = "CreatedBy")]
        public string CreatedBy { get; set; } = null!; 


        [DataMember(Name = "modifiedOn")]
        public  DateTime ModifiedOn { get; set; }


        [DataMember(Name = "modifiedBy")]
        public  string ModifiedBy { get; set; } =null!;


        [DataMember(Name = "isDeleted")]
        public bool? IsDeleted { get; set; }

    }
}
