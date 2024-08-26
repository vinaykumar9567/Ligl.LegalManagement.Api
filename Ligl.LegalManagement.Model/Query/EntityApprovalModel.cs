using System.Runtime.Serialization;

namespace Ligl.LegalManagement.Model.Query
{

    public partial class EntityApprovalModel : EntityApproval
    {
        [DataMember(Name = "entityTypeUniqueID")]
        public Guid EntityTypeUniqueID { get; set; }

        [DataMember(Name = "entityUniqueID")]
        public  Guid EntityUniqueID { get; set; }

        [DataMember(Name = "approvalStatusUniqueID")]
        public Guid ApprovalStatusUniqueID { get; set; }

        [DataMember(Name = "emailTemplateUniqueID")]
        public Guid EmailTemplateUniqueID { get; set; }

        [DataMember(Name = "approvalUserUniqueID")]
        public Guid ApprovalUserUniqueID { get; set; }

        [DataMember(Name = "statusUniqueID")]
        public Guid StatusUniqueID { get; set; }


         
    }
}
