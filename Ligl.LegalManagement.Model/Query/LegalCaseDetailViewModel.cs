using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Ligl.LegalManagement.Model.Query
{
    [DataContract]
    public class LegalCaseDetailViewModel
    {
        [Key]
        [DataMember(Name = "CaseLegalHoldID")]
        public int CaseLegalHoldID { get; set; }

        [DataMember(Name = "UUID")]
        public System.Guid UUID { get; set; }

        [DataMember(Name = "LegalHoldName")]
        public string? LegalHoldName { get; set; }

        [DataMember(Name = "CaseID")]
        public int CaseID { get; set; }

        [DataMember(Name = "LHNCustodianTemplateID")]
        public int? LHNCustodianTemplateID { get; set; }

        [DataMember(Name = "CustodianQuestionnaireTemplateID")]
        public int? CustodianQuestionnaireTemplateID { get; set; }

        [DataMember(Name = "LHNStakeHolderTemplateID")]
        public int? LHNStakeHolderTemplateID { get; set; }

        [DataMember(Name = "Status")]
        public int? Status { get; set; }

        [DataMember(Name = "StakeHolderQuestionnaireTemplateID")]
        public int? StakeHolderQuestionnaireTemplateID { get; set; }

        [DataMember(Name = "NumberOfDays")]
        public int? NumberOfDays { get; set; }

        [DataMember(Name = "DateRangeID")]
        public long? DateRangeID { get; set; }

        [DataMember(Name = "KeyWordID")]
        public long? KeyWordID { get; set; }
    }
}
