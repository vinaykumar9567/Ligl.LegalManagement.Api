using System.Runtime.Serialization;

namespace Ligl.LegalManagement.Model.Query
{
    [DataContract]
    public partial class InterviewEntityViewModel : InterviewEntity
    {
        [DataMember(Name = "entityUniqueID")]
        public Guid EntityUniqueID { get; set; }

        [DataMember(Name = "entityTypeUniqueID")]
        public Guid EntityTypeUniqueID { get; set; }

        [DataMember(Name = "statusUniqueID")]
        public Guid StatusUniqueID { get; set; }

        [DataMember(Name = "caseLegalHoldUniqueID")]
        public Guid CaseLegalHoldUniqueID { get; set; }
    }
}
