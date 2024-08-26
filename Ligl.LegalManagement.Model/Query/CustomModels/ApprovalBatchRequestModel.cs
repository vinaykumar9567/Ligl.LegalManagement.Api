namespace Ligl.LegalManagement.Model.Query.CustomModels
{
        /// <summary>
        /// Approval Batch request object
        /// </summary>
        public class ApprovalBatchData// : BaseEntity

        {
            public Guid? ApprovalTypeUniqueID { get; set; }
            public string ApprovalBatchName { get; set; } = null!;
            public int? CustodiansCount { get; set; }
            public int? KeywordsCount { get; set; }
            public int? DataSourcesCount { get; set; }

            public int? DateRangesCount { get; set; }

            public Guid? ApprovalType { get; set; }

            public string ApprovalUsers { get; set; } = null!;

            public Guid? ApprovalStatus { get; set; }

            public string ApprovalStatusName { get; set; } = null!;

            public string ApprovalTypeName { get; set; } = null!;

            public DateTime? ApprovedOn { get; set; }

            public Guid? CaseUniqueID { get; set; }

            public Guid? ApprovalBatchUniqueID { get; set; }

            public int? CaseID { get; set; }

            public Guid? Status { get; set; }

            public Guid? ApprovalStatusUniqueID { get; set; }

            public bool IsCaseApprovalBatchEntryAvailable { get; set; }

            public List<ApprovalDetails> ApprovalDetails { get; set; } = new List<ApprovalDetails>();

        public string Comments { get; set; } = null!;
            public Guid? EmailTemplateID { get; set; }
        }


        /// <summary>
        /// Class to return Approval Details Batch data of scope
        /// </summary>

        public class ApprovalBatchDetailsData //: BaseEntity

        {

            public string ApprovalBatchName { get; set; } = null!;

            public Guid? ApprovalBatchUniqueID { get; set; }
        public List<CaseCustodianData> CustodiansData { get; set; } = null!;
            public List<CaseKeyWordData> KeywordsData { get; set; } = null!;
            public List<CaseDataSourceData> DataSourcesData { get; set; } = null!;

            public List<CaseDateRangeData> DateRangesData { get; set; } = null!;
            public Guid? CaseUniqueID { get; set; }

            public Guid? Status { get; set; }

        }

        /// <summary>
        /// Class to return custodian data of scope
        /// </summary>
        public class CaseCustodianData
        {
            public Guid? CaseCustodianID { get; set; }
            public Guid? CustodianID { get; set; }
            public string CustodianName { get; set; } = null!;

        }

        /// <summary>
        /// Class to return datasource data of scope
        /// </summary>
        public class CaseDataSourceData
        {
            public Guid? CaseDataSourceTypeUniqueID { get; set; }
            public string DataSourceName { get; set; } = null!;

        }

        /// <summary>
        /// Class to return keyword data of scope
        /// </summary>
        public class CaseKeyWordData
        {
            public Guid? CaseKeyWordsID { get; set; }
            public string KeyWords { get; set; } = null!;

            public DateTime? CreatedOn { get; set; }

            public bool? IsEnabled { get; set; }

        }

        /// <summary>
        /// Class to return daterange data of scope
        /// </summary>
        public class CaseDateRangeData
        {
            public Guid? CaseDateRangeID { get; set; }

            public System.DateTime StartDate { get; set; }
            public System.DateTime EndDate { get; set; }

        }

        /// <summary>
        /// Class to return batch usersinfo data of scope
        /// </summary>
        public class ApprovalBatchUserInfo
        {
            public Guid? ApprovalUserId { get; set; }

            public Guid? ApprovalType { get; set; }
            public Guid? ApprovalSubType { get; set; }
            public Guid ApprovalStatusId { get; set; }

            public Guid? ApprovalSubTypeStatusUniqueID { get; set; }

            public Guid? ApprovalBatchID { get; set; }
            public int ApprovalPriority { get; set; }
            public string Comments { get; set; } = null!;
            public Guid? Status { get; set; }
        }


        /// <summary>
        /// Class to return grouping data of scope
        /// </summary>
        public class ApprovalGroupData //: BaseEntity
        {
            public string Comments { get; set; } = null!;
            public Guid? EmailTemplateID { get; set; }

            public int? ApprovalType { get; set; }

            public string ApprovalUsers { get; set; } = null!;

            public int? ApprovalStatus { get; set; }

            public string ApprovalBatchName { get; set; } = null!;

            public Guid? CaseUniqueID { get; set; }

            public Guid? ApprovalBatchUniqueID { get; set; }

            public Guid? EntityUniqueID { get; set; }

            public Guid? EntityTypeUniqueID { get; set; }


            public Guid? ApprovalTypeUniqueID { get; set; }

            public int EntityTypeID { get; set; }

        public string ApprovalStatusName { get; set; } = null!;

            public string? ApprovalTypeName { get; set; }

            public DateTime? ApprovedOn { get; set; }

            public int? ApprovalBatchID { get; set; }

            public Guid? ApprovalStatusUniqueID { get; set; }

            public bool IsCaseApprovalBatchEntryAvailable { get; set; }

            public List<ApprovalDetails> ApprovalDetails { get; set; } = null!;

        }


}
