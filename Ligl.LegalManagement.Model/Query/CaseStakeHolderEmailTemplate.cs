using System.Runtime.Serialization;
namespace Ligl.LegalManagement.Model.Query
{
    [DataContract]  
    public class CaseStakeHolderEmailTemplate //: BaseEntity
    {
        [DataMember(Name = "emailTemplateUniqueID")]
        public Guid? EmailTemplateUniqueID { get; set; }

        [DataMember(Name = "caseUniqueID")]
        public Guid CaseUniqueID { get; set; }

        [DataMember(Name = "caseStakeHolderModels")]
        public List<CaseStakeHolderModel> CaseStakeHolderModels { get; set; }

        [DataMember(Name = "caseLegalHoldUniqueID")]
        public Guid CaseLegalHoldUniqueID { get; set; }
    }
}
