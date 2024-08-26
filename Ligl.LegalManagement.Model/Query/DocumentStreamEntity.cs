using System.Runtime.Serialization;
namespace Ligl.LegalManagement.Model.Query
{
    [DataContract]  
    public partial class DocumentStreamEntity //:BaseEntity
    {
        [DataMember(Name = "DocumentStreamID")]
        public int DocumentStreamID { get; set; }

        [DataMember(Name = "UUID")]
        public Guid UUID { get; set; }

        [DataMember(Name = "filePath")]
        public string FilePath { get; set; }

        [DataMember(Name = "name")]
        public string   Name { get; set; }

        [DataMember(Name = "entityID")]
        public int EntityID { get; set; }

        [DataMember(Name = "entityTypeID")]
        public int EntityTypeID { get; set; }

        [DataMember(Name = "extension")]
        public string Extension { get; set; }

        [DataMember(Name = "comments")]
        public string?   Comments { get; set; }

        [DataMember(Name = "fileSize")]
        public decimal? FileSize { get; set; }

        [DataMember(Name = "fileData")]
        public byte[]? FileData { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "lHNPreservingTypeID")]
        public int LHNPreservingTypeID { get; set; }

        public  Guid? ID { get; set; }


        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
