

namespace Ligl.LegalManagement.Model.Query.CustomModels
{
    /// <summary>
    /// Approval Details 
    /// </summary>
    public class ApprovalDetails
    {
        public DateTime? ApprovedOn { get; set; }
        public string Comments { get; set; }
        public string UserName { get; set; }
        public Guid ApprovalStatusUniqueID { get; set; }
        public string ApprovalStatus { get; set; }
        public string CreatedOn { get; set; }
        public int? ApprovalPriority { get; set; }
        public int? ApprovalType { get; set; }

        public Guid ApprovalUserID { get; set; }
        public Guid? ApprovalSubTypeStatusUniqueID { get; set; }

        public string ApprovalSubTypeStatusName { get; set; }

        public Guid? ApprovalBatchID { get; set; }

        public Guid? ApprovalUserUniqueID { get; set; }

        public Guid? ApprovalTypeUniqueID { get; set; }
    }

}
