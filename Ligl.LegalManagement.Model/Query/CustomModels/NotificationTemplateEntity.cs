using System.Runtime.Serialization;
namespace Ligl.LegalManagement.Model.Query.CustomModels
{
    public partial class NotificationTemplateEntity //: BaseEntity
    {
        [IgnoreDataMember]
        public int NotificationTemplateID { get; set; }
        [IgnoreDataMember]
        public System.Guid UUID { get; set; }
        [IgnoreDataMember]
        public int CategoryID { get; set; }

        public string? Name { get; set; }
        [IgnoreDataMember]
        public int TemplateTypeID { get; set; }
        [IgnoreDataMember]
        public Nullable<int> Status { get; set; }
        [IgnoreDataMember]
        public Nullable<int> EntityTypeID { get; set; }
        [IgnoreDataMember]
        public Nullable<int> EntityID { get; set; }

        [DataMember(Name = "ID")]
        public Guid? ID { get; set; }
        [DataMember(Name = "isDeleted")]
        public bool? IsDeleted { get; set; }

    }
}
