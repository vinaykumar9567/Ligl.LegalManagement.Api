using Ligl.LegalManagement.Model.Query;
using System.Runtime.Serialization;
namespace Ligl.LegalManagement.Model.Command
{
    public partial class StakeHolderEntity //: BaseEntity
    {
        //public StakeHolderEntity()
        //{
        //    this.CaseStakeHolders = new HashSet<CaseStakeHolderEntity>();
        //}

        [DataMember(Name = "stakeHolderID")]
        public int StakeHolderID { get; set; }
        [DataMember(Name = "UUID")]
        public Nullable<System.Guid> UUID { get; set; }
        [DataMember(Name = "firstName")]
        public string?   FirstName { get; set; }
        [DataMember(Name = "middleName")]
        public string? MiddleName { get; set; }
        [DataMember(Name = "lastName")]
        public string? LastName { get; set; }
        [DataMember(Name = "emailAddress")]
        public string? EmailAddress { get; set; }
        [DataMember(Name = "departmentID")]       
        public int? DepartmentID { get; set; }
        [DataMember(Name = "status")]
        public int? Status { get; set; }
        [DataMember(Name = "fullName")]
        public string? FullName { get; set; }
        [DataMember(Name = "statusChangeReason")]
        public string? StatusChangeReason { get; set; }

        [DataMember(Name = "categoryID")]
        public Nullable<int> CategoryID { get; set; }

        [DataMember(Name = "caseStakeHolders")]
        public virtual ICollection<CaseStakeHolderEntity> CaseStakeHolders { get; set; }

        [DataMember(Name = "createdOn")]
        public DateTime CreatedOn { get; set; } 

        [DataMember(Name = "createdBy")]
        public string CreatedBy { get; set; } = null!;

        [DataMember(Name = "modifiedOn")]
        public DateTime ModifiedOn { get; set; }

        [DataMember(Name = "modifiedBy")]
        public string ModifiedBy { get; set; } = null!;

        [DataMember(Name = "id")]
        public Guid? ID { get; set; }

        [DataMember(Name = "IsDeleted")]
        public bool? IsDeleted { get; set; }
    }
}
