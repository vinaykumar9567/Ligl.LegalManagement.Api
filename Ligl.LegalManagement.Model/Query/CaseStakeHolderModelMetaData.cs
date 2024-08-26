using System.Runtime.Serialization;
namespace Ligl.LegalManagement.Model.Query
{

    /// <summary>
    /// Added required properties to Partial Class
    /// </summary>

    [DataContract]
    public partial class CaseStakeHolderModel
    {
        [DataMember(Name = "stakeHolderModel")]
        public StakeHolderModel StakeHolderModel { get; set; }

        [DataMember(Name = "entityStatusUniqueID")]
        public Guid EntityStatusUniqueID { get; set; }

        [DataMember(Name = "entityUniqueID")]
        public Guid? EntityUniqueID { get; set; }

        [DataMember(Name = "comments")]
        public string? Comments { get; set; }


    }

    /// <summary>
    /// Added Included StakeHolder Property as it is required for the request
    /// </summary>
    public partial class StakeHolder
    {
        public bool? IncludeStakeholders { get; set; }
    }
}
