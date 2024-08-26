
using System.Runtime.Serialization;

namespace Ligl.LegalManagement.Model.Query.Constants
{
    public enum EntityTypes
    {
        Case = 1,
        CaseCustodian = 2,
        Matter = 3,
        CaseCustodianDataSource = 4,
        Volume = 5,
        ExportSet = 7,
        ProductionRequest = 8,
        CaseDataRange = 9,
        VolumeExportSet = 10,
        UserLogin = 11,
        Role = 12,
        CaseStakeHolder = 16,
        Interview = 17,
        RequestTracking = 18,
        ProductionSet = 19,
        EntityApproval = 20,
        EntityLegalHoldNotice = 21,
        HostingSet = 22,
        CategoryReference = 24,
        CaseLegalHold = 25,
        CasePartyContact = 33,
        CaseOtherParty = 1045,
        CaseCourt = 1046,
        NotificationTemplate = 34,
        CaeKeyWords = 36,
        PendingApproval = 49,
        Approved = 50,
        declined = 8618,
        caseDocument = 1039,
        CaseInHouseC = 33,
        InhouseEmail = 8825,
    }

    public enum StatusType
    {
        [EnumMember(Value = "39")]
        Active,
        [EnumMember(Value = "40")]
        InActive
    }


    public enum CacheKeys
    {
        Lookups,
        AppSettings,
        Modules,
        Organization,
        Roles,
        Modulestate,
        LookupExtension,
        Entities,
        EmailTemplates,
        MasterAppSettings,
        EntityMasterAppSettings,
        MasterLookup,
        WorkFlowTemplates,
        License,
        Timezones,
        LegalGroups,
        MasterAppConfigs,
        EmployeeMasterLookups,
        WorkFlowRefactors,
        AllAppConfigs
    }
    public enum ExceptionType
    {
        Error,
        Fault
    }
    public enum FaultTypes
    {
        None,
        Validation,
        StateTransition
    }
    public enum ErrorSeverityTypes
    {
        Critical,
        High,
        Medium,
        Low,
        Warning
    }
    //public enum SQLErrorCodes
    //{
    //    [EnumMember(Value = "1002")]
    //    Exists,
    //    [EnumMember(Value = "1003")]
    //    AuthFailed,
    //    [EnumMember(Value = "1001")]
    //    NotFound,
    //    [EnumMember(Value = "1005")]
    //    EntityNotAuthorized,
    //    [EnumMember(Value = "1006")]
    //    EDIGAliasMappingIsFailed,
    //    [EnumMember(Value = "1007")]
    //    EDIGCurrentValueExceeded9999,
    //}

}
