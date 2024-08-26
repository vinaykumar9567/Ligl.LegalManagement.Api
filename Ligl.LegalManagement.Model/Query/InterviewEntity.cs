using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ligl.LegalManagement.Model.Query
{
    [DataContract]
    public partial class InterviewEntity //: BaseEntity
    {
        [DataMember(Name = "interviewID")]
        public int InterviewID { get; set; }

        [DataMember(Name = "UUID")]
        public Guid UUID { get; set; }

        [DataMember(Name = "entityID")]
        public int EntityID { get; set; }

        [DataMember(Name = "entityTypeID")]
        public int EntityTypeID { get; set; }

        [DataMember(Name = "interviewer")]
        public string Interviewer { get; set; }

        [DataMember(Name = "interviewDate")]
        public DateTime InterviewDate { get; set; }

        [DataMember(Name = "interviewPlace")]
        public string InterviewPlace { get; set; }

        [DataMember(Name = "notes")]
        public string Notes { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "caseLegalHoldID")]
        public int CaseLegalHoldID { get; set; }

        [DataMember(Name = "createdOn")]
        public  DateTime CreatedOn { get; set; }

        [DataMember(Name = "createdBy")]
        public  string CreatedBy { get; set; }

        [DataMember(Name = "modifiedOn")]
        public  DateTime ModifiedOn { get; set; }

        [DataMember(Name = "modifiedBy")]
        public string ModifiedBy { get; set; }

        [DataMember(Name = "ID")]
        public  Guid? ID { get; set; }

        [DataMember(Name = "isDeleted")]
        public bool? IsDeleted { get; set; }
    }
}
