using System.Runtime.Serialization;


namespace Ligl.LegalManagement.Model.Query
{

    [DataContract]  
    public partial class EntityApproval
    {
        [DataMember(Name = "entityApprovalID")]
        public int EntityApprovalID { get; set; }

        [DataMember(Name = "UUID")]
        public Guid UUID { get; set; }

        [DataMember(Name = "entityTypeID")]
        public int EntityTypeID { get; set; }

        [DataMember(Name = "entityID")]
        public int EntityID { get; set; }

        [DataMember(Name = "approvalStatusID")]
        public int ApprovalStatusID { get; set; }

        [DataMember(Name = "approvedOn")]
        public DateTime ApprovedOn { get; set; }

        [DataMember(Name = "emailTemplateID")]
        public  int EmailTemplateID { get; set; }

        [DataMember(Name = "approvalUserID")]
        public  int ApprovalUserID { get; set; }

        [DataMember(Name = "comments")]
        public string? Comments { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "createdBy")]
        public string? CreatedBy { get; set; } = null!;

        [DataMember(Name = "isDeleted")]
        public bool? IsDeleted { get; set; }
    }
}
