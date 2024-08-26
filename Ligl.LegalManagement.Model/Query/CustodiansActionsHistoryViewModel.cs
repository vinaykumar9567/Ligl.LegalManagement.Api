
using System.Runtime.Serialization;

namespace Ligl.LegalManagement.Model.Query
{
    [DataContract]
    public class CustodiansActionsHistoryViewModel
    {
        [DataMember(Name = "custodianName")]
        public string? CustodianName { get; set; }

        [DataMember(Name = "entityID")]
        public int EntityID { get; set; }


        [DataMember(Name = "UUID")]
        public Guid UUID { get; set; }

        [DataMember(Name = "sentOn")]
        public DateTime? SentOn { get; set; }

        [DataMember(Name = "lhnStatus")]
        public string? LhnStatus { get; set; }

        [DataMember(Name = "actionIntiatedBy")]
        public string? ActionIntiatedBy { get; set; }

        [DataMember(Name = "entityUniqueID")]
        public Guid? EntityUniqueID { get; set; }

        [DataMember(Name = "caseLegalHoldUniqueID")]
        public Guid? CaseLegalHoldUniqueID { get; set; }

        [DataMember(Name = "createdOn")]
        public DateTime? CreatedOn { get; set; }

        [DataMember(Name = "modifiedOn")]
        public DateTime? ModifiedOn { get; set; }


    }
}
