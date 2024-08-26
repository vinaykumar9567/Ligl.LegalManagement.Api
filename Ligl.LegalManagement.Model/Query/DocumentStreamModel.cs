using System.Runtime.Serialization;

namespace Ligl.LegalManagement.Model.Query
{
    [DataContract]
    public partial class DocumentStreamModel : DocumentStreamEntity
    {

        [DataMember(Name = "entityUniqueID")]
        public Guid EntityUniqueID { get; set; }

        [DataMember(Name = "entityTypeUniqueID")]
        public Guid EntityTypeUniqueID { get; set; }

        [DataMember(Name = "statusUniqueID")]
        public Guid StatusUniqueID { get; set; }

        [DataMember(Name = "lHNPreservingTypeUniqueID")]
        public Guid LHNPreservingTypeUniqueID { get; set; }

        [DataMember(Name = "isLHNNoticeTemplate")]

        public bool IsLHNNoticeTemplate { set; get; }
    }
}
