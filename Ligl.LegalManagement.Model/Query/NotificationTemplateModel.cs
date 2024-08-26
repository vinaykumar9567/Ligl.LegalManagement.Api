using System.Runtime.Serialization;

namespace Ligl.LegalManagement.Model.Query
{
    [DataContract]
    public partial class NotificationTemplateModel //:BaseEntity
    {

        [DataMember(Name = "notificationTemplateContentUniqueID")]
        public Guid NotificationTemplateContentUniqueID { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }

        [DataMember(Name = "categoryName")]
        public string CategoryName { get; set; }

        [DataMember(Name = "templateTypeName")]
        public string TemplateTypeName { get; set; }

        [DataMember(Name = "statusName")]
        public string StatusName { get; set; }

        [DataMember(Name = "documents")]
        public List<DocumentStreamModel> Documents { get; set; }


        [DataMember(Name = "caseUniqueID")]
        public Guid CaseUniqueID { get; set; }


        [DataMember(Name = "legalHoldUniqueID")]
        public Guid LegalHoldUniqueID { get; set; }


        [DataMember(Name = "notificationID")]
        public int NotificationID { get; set; }
    }
}
