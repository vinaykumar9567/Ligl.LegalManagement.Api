

namespace Ligl.LegalManagement.Model.Query.Constants
{
    /// <summary>
    /// This class contain Base error codes
    /// </summary>
    public partial class BaseErrorCodes
    {
        public Dictionary<int, string> ErrorString { get; set; }

        public const int GenericError = 999999;
        public const int UnhandledError = 888888;
        public const int UnknownSqlException = 666666;
        public const int UserNotAuthorized = 777777;
        public const int ValidationErrors = 555555;
        public const int DbUpdateException = 444444;
        public const int DbSaveException = 333333;
        public const int ThresholdLimitExceeded = 140001;
        public const int declineAppSettingisNull = 140002;
        public const int invalidAppconfig = 140003;


        public BaseErrorCodes()
        {
            ErrorString = new Dictionary<int, string>();
            ErrorString[BaseErrorCodes.GenericError] = "Generic error";
            ErrorString[BaseErrorCodes.UnhandledError] = "Unhandled error";
            ErrorString[BaseErrorCodes.UnknownSqlException] = "Unknown Sql exception";
            ErrorString[BaseErrorCodes.UserNotAuthorized] = "User not authorized";
            ErrorString[BaseErrorCodes.ValidationErrors] = "Validation failed for the entities";
            ErrorString[BaseErrorCodes.DbUpdateException] = "Unable to update the request";
            ErrorString[BaseErrorCodes.DbSaveException] = "Unable to save the request";
            ErrorString[BaseErrorCodes.ThresholdLimitExceeded] = "Please select less than or equal to {0} records to continue";
            ErrorString[BaseErrorCodes.declineAppSettingisNull] = "Decline AppSetting  is null";
            ErrorString[BaseErrorCodes.invalidAppconfig] = "Invalid appconfigID";
        }
    }
    /// <summary>
    /// Error Message with corresponding Error ID's
    /// </summary>
    public partial class BaseErrorProvider
    {
        #region Data members
        public static readonly string DEFAULT_ERRORMESSAGE = "Unknown Application Error";
        #endregion

        #region Constructors
        static BaseErrorProvider()
        {

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Errr
        /// </summary>
        /// <param name="salesCrmErrorCode"></param>
        /// <returns></returns>
        public static string GetErrorString<T>(int errorCode) where T : BaseErrorCodes, new()
        {
            T errorInfo = new T();
            String errorMessage = DEFAULT_ERRORMESSAGE;
            if (errorInfo != null && errorInfo.ErrorString != null && errorInfo.ErrorString.ContainsKey(errorCode))
            {
                errorMessage = errorInfo.ErrorString[errorCode] as String;
                if (string.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = DEFAULT_ERRORMESSAGE;
                }
            }
            else
                errorMessage = DEFAULT_ERRORMESSAGE;
            return $"SW{errorCode}-{errorMessage}";
        }
        #endregion  
    }
}
