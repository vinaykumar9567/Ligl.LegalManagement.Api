using System.Runtime.Serialization;

namespace Ligl.LegalManagement.Model.Query
{
    [DataContract]
    public partial class Entities 
    {
        [DataMember(Name = "ID")]
        public int ID { get; set; }

        [DataMember(Name = "parentID")]
        public int ParentID { get; set; }

        [DataMember(Name = "parentUUID")]
        public Guid ParentUUID { get; set; }

        [DataMember(Name = "parentName")]
        public string? ParentName { get; set; }
    }
}
