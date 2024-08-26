using System.Runtime.Serialization;
namespace Ligl.LegalManagement.Model.Query
{
    [DataContract]
    public class CaseCustodianViewModel// : BaseEntity
    {
        [DataMember(Name = "dpnID")]
        public Guid DpnID { get; set; }

        [DataMember(Name = "custodianID")]
        public System.Guid CustodianID { get; set; }

        [DataMember(Name = "custodianAlias")]
        public string? CustodianAlias { get; set; }

        [DataMember(Name = "fullName")]
        public string? FullName { get; set; }
        [DataMember(Name = "firstName")]
        public string? FirstName { get; set; }
        [DataMember(Name = "lastName")]
        public string? LastName { get; set; }
        [DataMember(Name = "email")]
        public string? Email { get; set; }
        [DataMember(Name = "departmentName")]
        public string? DepartmentName { get; set; }
        [DataMember(Name = "clientName")]
        public string? ClientName { get; set; }
        [DataMember(Name = "employmentStartDate")]
        public Nullable<System.DateTime> EmploymentStartDate { get; set; }
        [DataMember(Name = "employmentEndDate")]
        public Nullable<System.DateTime> EmploymentEndDate { get; set; }
        [DataMember(Name = "mobileNo")]
        public string? MobileNo { get; set; }
        [DataMember(Name = "workPhoneNo")]
        public string? WorkPhoneNo { get; set; }
        [DataMember(Name = "dpnStatusID")]
        public Nullable<System.Guid> DpnStatusID { get; set; }
        [DataMember(Name = "priority")]
        public Nullable<int> Priority { get; set; }
        [DataMember(Name = "dPNStatusName")]
        public string? DPNStatusName { get; set; }
        [DataMember(Name = "requestSentBy")]
        public string? RequestSentBy { get; set; }
        [DataMember(Name = "requestSentOn")]
        public Nullable<System.DateTime> RequestSentOn { get; set; }
        [DataMember(Name = "acknowledgedOn")]
        public Nullable<System.DateTime> AcknowledgedOn { get; set; }
        [DataMember(Name = "documentStreamID")]
        public Nullable<System.Guid> DocumentStreamID { get; set; }
        [DataMember(Name = "designation")]
        public string? Designation { get; set; }
        [DataMember(Name = "CQRCode")]
        public string? CQRCode { get; set; }
        [DataMember(Name = "questionnaireTemplateID")]
        public Nullable<System.Guid> QuestionnaireTemplateID { get; set; }
        [DataMember(Name = "questionnaireTemplateName")]
        public string? QuestionnaireTemplateName { get; set; }
        [DataMember(Name = "isQuestionnaireAvailable")]
        public bool IsQuestionnaireAvailable { get; set; }
        [DataMember(Name = "isUserActive")]
        public Nullable<bool> IsUserActive { get; set; }
        [DataMember(Name = "categoryID")]
        public Nullable<System.Guid> CategoryID { get; set; }
        [DataMember(Name = "employeeCode")]
        public string? EmployeeCode { get; set; }
        [DataMember(Name = "middleName")]
        public string? MiddleName { get; set; }
        [DataMember(Name = "officeAddressBuildingCode")]
        public string? OfficeAddressBuildingCode { get; set; }
        [DataMember(Name = "officeAddressCampusCode")]
        public string? OfficeAddressCampusCode { get; set; }
        [DataMember(Name = "officeAddressMailingCode")]
        public string? OfficeAddressMailingCode { get; set; }
        [DataMember(Name = "officeAddressZipcode")]
        public string? OfficeAddressZipcode { get; set; }
        [DataMember(Name = "officeAddressCity")]
        public string? OfficeAddressCity { get; set; }
        [DataMember(Name = "officeAddressState")]
        public string? OfficeAddressState { get; set; }
        [DataMember(Name = "homeDepartmentCode")]
        public string? HomeDepartmentCode { get; set; }
        [DataMember(Name = "accountType")]
        public string? AccountType { get; set; }
        [DataMember(Name = "historicEmployeeID")]
        public string? HistoricEmployeeID { get; set; }
        [DataMember(Name = "accountManagerFirstName")]
        public string? AccountManagerFirstName { get; set; }
        [DataMember(Name = "accountManagerMiddleName")]
        public string? AccountManagerMiddleName { get; set; }
        [DataMember(Name = "accountManagerLastName")]
        public string? AccountManagerLastName { get; set; }
        [DataMember(Name = "workFaxNo")]
        public string? WorkFaxNo { get; set; }
        [DataMember(Name = "alternateEmail")]
        public string? AlternateEmail { get; set; }
        [DataMember(Name = "secondaryEmail")]
        public string? SecondaryEmail { get; set; }
        [DataMember(Name = "column1")]
        public string? Column1 { get; set; }

        [DataMember(Name = "column2")]
        public string? Column2 { get; set; }

        [DataMember(Name = "column3")]
        public string? Column3 { get; set; }

        [DataMember(Name = "column4")]
        public string? Column4 { get; set; }

        [DataMember(Name = "column5")]
        public string? Column5 { get; set; }

        [DataMember(Name = "column6")]
        public string? Column6 { get; set; }

        [DataMember(Name = "column7")]
        public string? Column7 { get; set; }

        [DataMember(Name = "column8")]
        public string? Column8 { get; set; }

        [DataMember(Name = "column9")]
        public string? Column9 { get; set; }

        [DataMember(Name = "column10")]
        public string? Column10 { get; set; }

        [DataMember(Name = "column11")]
        public string? Column11 { get; set; }

        [DataMember(Name = "column12")]
        public string? Column12 { get; set; }

        [DataMember(Name = "column13")]
        public string? Column13 { get; set; }

        [DataMember(Name = "column14")]
        public string? Column14 { get; set; }

        [DataMember(Name = "column15")]
        public string? Column15 { get; set; }

        [DataMember(Name = "column16")]
        public string? Column16 { get; set; }

        [DataMember(Name = "column17")]
        public string? Column17 { get; set; }

        [DataMember(Name = "column18")]
        public string? Column18 { get; set; }

        [DataMember(Name = "column19")]
        public string? Column19 { get; set; }

        [DataMember(Name = "column20")]
        public string? Column20 { get; set; }
        [DataMember(Name = "entityID")]
        public Nullable<int> EntityID { get; set; }
        [DataMember(Name = "location")]
        public string? Location { get; set; }
        [DataMember(Name = "officeName")]
        public string? OfficeName { get; set; }
        [DataMember(Name = "officeType")]
        public string? OfficeType { get; set; }
        [DataMember(Name = "division")]
        public string? Division { get; set; }
        [DataMember(Name = "reportingManager")]
        public string? ReportingManager { get; set; }
        [DataMember(Name = "affiliation")]
        public string? Affiliation { get; set; }
        [DataMember(Name = "accountManagerFullName")]
        public string? AccountManagerFullName { get; set; }
        [DataMember(Name = "reason")]
        public string? Reason { get; set; }
        [DataMember(Name = "title")]

        public string? Title { get; set; }
        [DataMember(Name = "acknowledgedBy")]
        public string? AcknowledgedBy { get; set; }
        [DataMember(Name = "questionnaireBy")]
        public string? QuestionnaireBy { get; set; }
        [DataMember(Name = "resendCount")]
        public Nullable<int> ResendCount { get; set; }
        [DataMember(Name = "lastResendDate")]

        public Nullable<System.DateTime> LastResendDate { get; set; }
        [DataMember(Name = "statusName")]
        public string? StatusName { get; set; }
        [DataMember(Name = "approvalStatusName")]
        public string? ApprovalStatusName { get; set; }
        [DataMember(Name = "approvalStatusUniqueID")]
        public Guid? ApprovalStatusUniqueID { get; set; }
        [DataMember(Name = "approvalBatchUniqueID")]
        public Guid? ApprovalBatchUniqueID { get; set; }
        [DataMember(Name = "approvalBatchName")]
        public string? ApprovalBatchName { get; set; }
        [DataMember(Name = "approvedOn")]
        public DateTime? ApprovedOn { get; set; }
        [DataMember(Name = "employeeUUID")]
        public Guid? EmployeeUUID { get; set; }
        [DataMember(Name = "caseCustodianID")]
        public int CaseCustodianID { get; set; }
        [DataMember(Name = "caseCustodianUniqueID")]
        public Guid CaseCustodianUniqueID { get; set; }
        [DataMember(Name = "acknowledgedType")]
        public string? AcknowledgedType { get; set; }
        [DataMember(Name = "reasonForProxyAcknowledged")]
        public string? ReasonForProxyAcknowledged { get; set; }
        [DataMember(Name = "requestContactDetails")]
        public string? RequestContactDetails { get; set; }
        [DataMember(Name = "userActions")]
        public string?   UserActions { get; set; }

        [DataMember(Name = "caseId")]
        public int CaseId { get; set; }

        [DataMember(Name = "departmentId")]
        public int DepartmentID { get; set; }


        [DataMember(Name = "status")]
        public int? Status { get; set; }

        [DataMember(Name = "caseLegalHoldID")]
        public int? CaseLegalHoldID { get; set; }

        [DataMember(Name = "acknowledgeTypeUUID")]
        public Guid? AcknowledgeTypeUUID { get; set; }

        [DataMember(Name = "approvalTypeName")]
        public string? ApprovalTypeName { get; set; }


        [DataMember(Name = "ApprovalTypeUniqueID")]
        public Guid? ApprovalTypeUniqueID { get; set; }

        [DataMember(Name = "ApprovalSentOn")]
        public DateTime ApprovalSentOn { get; set; }


        [DataMember(Name = "approvalSentBy")]
        public string? ApprovalSentBy { get; set; }


        [DataMember(Name = "approvalComments")]
        public string? ApprovalComments { get; set; }

        [DataMember(Name = "rowNum")]
        public int? RowNum { get; set; }

        public  Guid? ID { get; set; }

        public  Guid? ModuleItemID { get; set; }
        public  Guid? ModuleActionID { get; set; }
        public  Guid? ModuleID { get; set; }
        public  Guid? EmployeeID { get; set; }
        public Guid? CaseID { get; set; } 

    }
}
