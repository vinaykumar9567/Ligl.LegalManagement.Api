using System.Runtime.Serialization;
namespace Ligl.LegalManagement.Model.Query
{
    [DataContract]
    public partial class QuestionnaireEntity //: BaseEntity
    {
           public QuestionnaireEntity()
        {
            QuestionnaireResponses = new HashSet<QuestionnaireResponseEntity>();
        }
        [DataMember(Name = "questionnaireID")]
        public int QuestionnaireID { get; set; }

        [DataMember(Name = "UUID")]
        public Guid UUID { get; set; }

        [DataMember(Name = "questionnaireText")]
        public string QuestionnaireText { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "renderTypeID")]
        public int RenderTypeID { get; set; }

        [DataMember(Name = "parentID")]
        public int? ParentID { get; set; }

        [DataMember(Name = "options")]
        public string Options { get; set; }

        [DataMember(Name = "isMandate")]
        public bool IsMandate { get; set; }

        [DataMember(Name = "entityTypeID")]
        public int EntityTypeID { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "languageID")]
        public int LanguageID { get; set; }

        [DataMember(Name = "isEditable")]
        public bool IsEditable { get; set; }

        [DataMember(Name = "questionnaireResponses")]
        public virtual ICollection<QuestionnaireResponseEntity> QuestionnaireResponses { get; set; }
    }
}
