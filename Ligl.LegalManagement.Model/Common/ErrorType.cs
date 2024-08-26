namespace Ligl.LegalManagement.Model.Common
{

    /// <summary>
    /// Identifies the type of event that has caused by the application.
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// Fatal error or application crash.
        /// </summary> 
        Critical,

        /// <summary>
        /// Recoverable error.
        /// </summary>
        Error,

        /// <summary>
        /// Noncritical problem
        /// </summary>
        Warning,

        /// <summary>
        /// Informational message
        /// </summary>
        Information,

        /// <summary>
        /// This is the most verbose logging level
        /// </summary>
        Debug,

        /// <summary>
        /// Fatal is reserved for special exceptions/conditions
        /// </summary>
        Fatal,

        /// <summary>
        /// The accomplishment of a given task measured against preset known standards of accuracy
        /// </summary>
        Performance,

        /// <summary>
        /// User Logging and logout information.
        /// </summary>
        UserAudit
    }
}
