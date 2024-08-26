using System.Runtime.Serialization;

namespace Ligl.LegalManagement.Model.Query
{
    [DataContract]
    public partial class QuestionnaireModel : QuestionnaireEntity
    {

        [DataMember(Name = "renderTypeUniqueID")]
        public  Guid RenderTypeUniqueID { get; set; }

        [DataMember(Name = "parentUniqueID")]
        public  Guid ParentUniqueID { get; set; }

        [DataMember(Name = "entityTypeUniqueID")]
        public Guid EntityTypeUniqueID { get; set; }

        [DataMember(Name = "statusUniqueID")]
        public Guid StatusUniqueID { get; set; }

        [DataMember(Name = "languageUniqueID")]
        public Guid LanguageUniqueID { get; set; }
    }
}
