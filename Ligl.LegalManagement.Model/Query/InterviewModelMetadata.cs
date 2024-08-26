using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Ligl.LegalManagement.Model.Query
{
    public class InterviewModelMetadata
    {

    }

    /// <summary>
    /// Adding the Partial Class for InterviewEntity
    /// </summary>
    [MetadataType(typeof(InterviewModelMetadata))]


    public partial class InterviewEntityViewModel
    {
        [DataMember(Name = "documents")]
        public List<DocumentStreamModel> Documents { get; set; }

        [DataMember(Name = "caseID")]
        public Guid CaseID { get; set; }

    }
}
