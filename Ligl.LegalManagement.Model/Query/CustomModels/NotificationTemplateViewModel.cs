namespace Ligl.LegalManagement.Model.Query.CustomModels
{
    public partial class NotificationTemplateViewModel : NotificationTemplateEntity
    {
        public Nullable<System.Guid> CategoryUniqueID { get; set; }
        public Nullable<System.Guid> TemplateTypeUniqueID { get; set; }
        public Nullable<System.Guid> StatusUniqueID { get; set; }
        public Nullable<System.Guid> EntityTypeUniqueID { get; set; }
        public Nullable<System.Guid> EntityUniqueID { get; set; }
    }
}
