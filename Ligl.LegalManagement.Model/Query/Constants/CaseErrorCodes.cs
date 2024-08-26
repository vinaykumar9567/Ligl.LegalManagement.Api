

namespace Ligl.LegalManagement.Model.Query.Constants
{
    /// <summary>
    /// This class contain case related error codes
    /// </summary>
    public class CaseErrorCodes : BaseErrorCodes
    {
        public const int CaselistNotFound = 200001;
        public const int PrefixValidation = 200002;
        public const int CaseNotFound = 200003;
        public const int CaseStatsNotFound = 200004;
        public const int CaseExists = 200005;
        public const int DatasourceNotFound = 200006;
        public const int DaterangeNotFound = 200007;
        public const int PartyNotFound = 200008;
        public const int StakeholderNotFound = 200009;
        public const int CustodianNotFound = 200010;
        public const int CaseOrDaterangeNotFound = 200011;
        public const int IdNotFound = 200012;
        public const int ContactAlreadyExists = 200013;
        public const int OptimumTemplateIDNotFound = 200014;
        public const int MatterConfigNotFound = 200015;
        public const int CaseValidation = 200016;
        public const int Emailalreadyexists = 200017;
        public const int CaseCustodianNotFound = 200018;
        public const int CaseCustodianDataSourceNotFound = 200019;
        public const int EDIGAliasMappingIsFailed = 200020;
        public const int EDIGCurrentValueExceeded9999 = 200021;
        public const int RoleAccessAppSettingNotFound = 200022;
        public const int RoleIDNotFound = 200023;
        public const int ConfigValueNotFound = 200024;
        public const int MailPlaceHoldersNotFound = 200025;
        public const int CaseDetailsNotFound = 200026;
        public const int OrganizationIsNull = 200027;
        public const int OrganizationIDIsNull = 200028;
        public const int NotAuthorizedToPerformAction = 200029;

        public const int ApprovalBatchNotFound = 200030;
        public const int ApprovalBatchUsersNotFound = 200031;
        public const int StatusNotFound = 200032;
        public const int incorrectKeyWordType = 200033;


        public CaseErrorCodes() : base()
        {
            ErrorString[CaseErrorCodes.CaselistNotFound] = "Project list not available";
            ErrorString[CaseErrorCodes.PrefixValidation] = "Project name or prefix already exist";
            ErrorString[CaseErrorCodes.CaseValidation] = "Project name already exist";
            ErrorString[CaseErrorCodes.CaseNotFound] = "Project not found";
            ErrorString[CaseErrorCodes.CaseStatsNotFound] = "Project stats not found";
            ErrorString[CaseErrorCodes.CaseExists] = "Project already exists";
            ErrorString[CaseErrorCodes.DatasourceNotFound] = "Project or data source not found";
            ErrorString[CaseErrorCodes.DaterangeNotFound] = "Project or date range not found";
            ErrorString[CaseErrorCodes.PartyNotFound] = "Project or party not found";
            ErrorString[CaseErrorCodes.StakeholderNotFound] = "Stakeholder not found";
            ErrorString[CaseErrorCodes.CustodianNotFound] = "Custodian or department not found";
            ErrorString[CaseErrorCodes.CaseOrDaterangeNotFound] = "Project or daterange not found";
            ErrorString[CaseErrorCodes.IdNotFound] = "{0} is not found";
            ErrorString[CaseErrorCodes.ContactAlreadyExists] = "Contact already exists";
            ErrorString[OptimumTemplateIDNotFound] = "Optimum templateID not found";
            ErrorString[MatterConfigNotFound] = "Matter config not found";
            ErrorString[CaseErrorCodes.Emailalreadyexists] = "Email already exists";
            ErrorString[CaseErrorCodes.CaseCustodianNotFound] = "Project custodian not found";
            ErrorString[CaseErrorCodes.CaseCustodianDataSourceNotFound] = "Project custodian datasource not found";
            ErrorString[CaseErrorCodes.EDIGAliasMappingIsFailed] = "EDIG alias mapping failed ";
            ErrorString[CaseErrorCodes.EDIGCurrentValueExceeded9999] = "EDIG current value exceeded 9999";
            ErrorString[CaseErrorCodes.RoleAccessAppSettingNotFound] = "Role access app setting not found";
            ErrorString[CaseErrorCodes.RoleIDNotFound] = "Role ID not found in role permission app settings";
            ErrorString[CaseErrorCodes.ConfigValueNotFound] = "Config value not found";
            ErrorString[CaseErrorCodes.MailPlaceHoldersNotFound] = "Mailplaceholders not found";
            ErrorString[CaseErrorCodes.CaseDetailsNotFound] = "Project details not found";
            ErrorString[CaseErrorCodes.OrganizationIsNull] = "Organization is null";
            ErrorString[CaseErrorCodes.OrganizationIDIsNull] = "Organization ID is null";
            ErrorString[CaseErrorCodes.NotAuthorizedToPerformAction] = "Not authorized to perform action";
            ErrorString[CaseErrorCodes.ApprovalBatchNotFound] = "ApprovalBatchID is Null";
            ErrorString[CaseErrorCodes.ApprovalBatchUsersNotFound] = "ApprovalBatch Users Not Found";
            ErrorString[CaseErrorCodes.StatusNotFound] = "Status Not Found";
            ErrorString[CaseErrorCodes.incorrectKeyWordType] = "Incorrect KeyWord Type";
        }
    }
}
