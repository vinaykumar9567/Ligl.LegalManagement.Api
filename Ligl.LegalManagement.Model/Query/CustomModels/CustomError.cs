using Ligl.LegalManagement.Model.Query.Constants;
namespace Ligl.LegalManagement.Model.Query.CustomModels
{
    /// <summary>
    ///     EntityOperation Error
    /// </summary>
    [Serializable]
    public class CustomError : Exception
    {
        #region Constructors

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public CustomError()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <param name="stackTrace"></param>
        /// <param name="type"></param>
        /// <param name="faultType"></param>
        /// <param name="severity"></param>
        public CustomError(int errorCode, string errorMessage, string stackTrace = null!,
            ExceptionType type = ExceptionType.Fault, FaultTypes faultType = FaultTypes.None,
            ErrorSeverityTypes severity = ErrorSeverityTypes.Warning)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            StackTrace = stackTrace;
            Type = type;
            Severity = severity;
            FaultType = faultType;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Error Code of EntityOperation Failure
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        ///     Error Message of EntityOperation
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     To Define the type of exception
        /// </summary>
        public ExceptionType Type { get; set; }

        /// <summary>
        ///     To Define the Type of Fault
        /// </summary>
        public FaultTypes FaultType { get; set; }

        /// <summary>
        ///     Type of Severity for the exception
        /// </summary>
        public ErrorSeverityTypes Severity { get; set; }

        /// <summary>
        ///     Stack trace of the EntityOperation
        /// </summary>
        public string StackTrace { get; set; } = null!;

        #endregion
    }
}
