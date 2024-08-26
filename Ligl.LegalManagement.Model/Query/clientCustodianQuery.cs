using System.Runtime.Serialization;
namespace Ligl.LegalManagement.Model.Query
{
    [DataContract]
    public partial class clientCustodianQuery
    {
        [DataMember(Name = "custodianID")]
        public int CustodianID { get; set; }

        [DataMember(Name = "clientID")]
        public int ClientID { get; set; }

        [DataMember(Name = "fullName")]
        public string? FullName { get; set; }

        [DataMember(Name = "caseID")]
        public int CaseID { get; set; }
 
    }
}
