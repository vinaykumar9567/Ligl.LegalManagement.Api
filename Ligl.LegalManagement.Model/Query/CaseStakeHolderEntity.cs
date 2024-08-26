using Ligl.LegalManagement.Model.Command;
using System.Runtime.Serialization;
namespace Ligl.LegalManagement.Model.Query
{
    public partial class CaseStakeHolderEntity //: BaseEntity
    {
        [DataMember(Name = "caseStakeHolderID")]
        public int CaseStakeHolderID { get; set; }

        [DataMember(Name = "UUID")]
        public Guid UUID { get; set; }

        [DataMember(Name = "caseID")]
        public int CaseID { get; set; }

        [DataMember(Name = "stakeHolderID")]
        public int StakeHolderID { get; set; }

        [DataMember(Name = "dpnStatusID")]
        public int DpnStatusID { get; set; }

        [DataMember(Name = "status")]
        public Nullable<int> Status { get; set; }


        [DataMember(Name = "ID")]
        public  Guid? ID { get; set; }

        [DataMember(Name = "stakeHolder")]
        public virtual StakeHolderEntity StakeHolder { get; set; }


    }
}
