using System.Runtime.Serialization;
namespace Ligl.LegalManagement.Model.Query
{
     
    public partial class CaseStakeHolderModel : CaseStakeHolderEntity
    {

        [DataMember(Name = "caseUniqueID")]
        public Guid CaseUniqueID { get; set; }

        [DataMember(Name = "stakeHolderUniqueID")]
        public Guid StakeHolderUniqueID { get; set; }

        [DataMember(Name = "dpnStatusUniqueID")]
        public Guid DpnStatusUniqueID { get; set; }

        [DataMember(Name = "statusUniqueID")]
        public Guid StatusUniqueID { get; set; }
    }
}
