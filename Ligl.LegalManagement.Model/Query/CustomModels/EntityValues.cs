using System.Runtime.Serialization;
namespace Ligl.LegalManagement.Model.Query.CustomModels
{
    /// <summary>
    /// Class for Entity Values 
    /// </summary>
    public class EntityValues
    {
        [IgnoreDataMember]
        public int? EntityTypeID { get; set; }
        public Guid? EntityTypeUniqueID { get; set; }
        public string EntityTypeName { get; set; }
        [IgnoreDataMember]
        public int? EntityID { get; set; }
        public Guid? EntityUniqueID { get; set; }
        public string EntityName { get; set; }

        public string ApprovalBatchName { get; set; }

        public bool? AllowNotification { get; set; }
    }
}
