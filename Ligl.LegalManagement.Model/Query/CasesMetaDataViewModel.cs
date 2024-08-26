using System.Runtime.Serialization;
namespace Ligl.LegalManagement.Model.Query
{

    /// <summary>
    /// Class for CasesMetaDataViewModel
    /// </summary>
    [DataContract]
    public class CasesMetaDataViewModel
    {

        [DataMember(Name = "stakeHolderID")]
        public Guid? StakeHolderID { get; set; }

        [DataMember(Name = "caseID")]
        public Guid? CaseID { get; set; }

        [DataMember(Name = "caseName")]
        public string? CaseName { get; set; }

        [DataMember(Name = "caseLegalHoldID")]
        public Guid? CaseLegalHoldID { get; set; }


    }
}
