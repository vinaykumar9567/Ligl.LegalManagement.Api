using System.Runtime.Serialization;

namespace Ligl.LegalManagement.Model.Command
{

    public partial class StakeHolderViewModel : StakeHolderEntity
    {

        [DataMember(Name = "departmentUniqueID")]
        public Guid DepartmentUniqueID { get; set; }

        [DataMember(Name = "statusUniqueID")]
        public Guid StatusUniqueID { get; set; }

        [DataMember(Name = "categoryUniqueID")]
        public Guid CategoryUniqueID { get; set; }
    }

}
