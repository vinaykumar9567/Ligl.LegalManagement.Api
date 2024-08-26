using Ligl.LegalManagement.Model.Query.CustomModels;


namespace Ligl.Model.Model.Query.CustomModels
{
    /// <summary>
    /// EmailNotificationRequest S
    /// </summary>
    public class EmailNotificationRequest 
    {
      
        private double _expiryTimeInDays;
    

        public Guid EntityUniqueID { get; set; }

        public Guid CaseCustodianUniqueID { get; set; }

        public string Url { get; set; } = null!;

        public string ToUserEmail { get; set; } = null!;

        public string ToUserName { get; set; } = null!;

        public DateTime ExpirtDateTime { get; set; }

        public double ExpiryTimeInDays
        {
            get { return _expiryTimeInDays / 24; }
            set { _expiryTimeInDays = value; }
        }

        public string EncyToken { get; set; } = null!;

        public Guid? LegalGroupUniqueID { get; set; }

        public Guid? NotificationStatusUniqueID { get; set; }

        public LegalManagement.Model.Query.CustomModels.NotificationTemplateViewModel EmailTemplate { get; set; }

        public string CaseLegalHoldName { get; set; } = null!;

        public int? CaseLegalHoldId { get; set; }

        public int? CaseId { get; set; }

        public bool? IsQuestTempConfigured { get; set; }

        public bool? IsAllowEmail { get; set; }

        public string Content { get; set; } = null!;

        public string Comments { get; set; } = null!;

        public Guid? EntityStatusUniqueID { get; set; }

        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Department { get; set; } = null!;

        public string Designation { get; set; } = null!;

        public string ReportingManager { get; set; } = null!;
        public int? EntityID { get; set; }
        public int? EntityTypeID { get; set; }
        public LicenseNotificationResponse LicenseNotificationResponse { get; set; }
        public Guid? FirstApproverUUID { get; set; }
        public Guid? SecondApproverUUID { get; set; }
        public EntityState EntityState { get; set; }
        public string LoggedinUserUUID { get; set; } = null!;
        public string DeclinedDate { get; set; } = null!;
        public string CaseName { get; set; } = null!;
        public string CustodianName { get; set; } = null!;
        public string IntendedUser { get; set; } = null!;
        public Guid? ResendAcknowledgementUrlId { get; set; }
        public bool? IsResendLHN { get; set; }
        public int? FirstApprover { get; set; }
        public int? FecondApprover { get; set; }

        public List<CaseCustodianData> CaseCustodians { get; set; }=null!;

        public List<CaseDataSourceData> CaseDataSources { get; set; } = null!;
        public List<ApprovalBatchUserInfo> ApprovalBatchUsers { get; set; } = null!;

        public List<CaseDateRangeData> CaseDateRanges { get; set; } = null!;

        public List<CaseKeyWordData> CaseKeyWords { get; set; } = null!;

        public Guid? ApprovalBatchUniqueID { get; set; }


        public Guid? ApprovalTypeName { get; set; }

        public string ClientName { get; set; } = null!;

        public string CompanyName { get; set; } = null!;     

        public string EmployeeChangeDetails { get; set; } = null!;
        public string SourceType { get; set; } = null!;

        public string EmployeeLastSyncUpdateTime { get; set; } = null!;

        public string ActiveHolds { get; set; } = null!;
    }
}
