namespace Ligl.LegalManagement.Model.Query.CustomModels
{
    /// <summary>
    /// Partial Class AdminEmailTemplateModelMetadata
    /// </summary>
    public partial class NotificationTemplateViewModel
    {
        public Guid NotificationTemplateContentUniqueID { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string CategoryName { get; set; }
        public string TemplateTypeName { get; set; }
        public string StatusName { get; set; }
        public List<DocumentStreamModel> Documents { get; set; }

        public Guid CaseUniqueID { get; set; }
        public Guid LegalHoldUniqueID { get; set; }

        public int NotificationID { get; set; }
    }
}
