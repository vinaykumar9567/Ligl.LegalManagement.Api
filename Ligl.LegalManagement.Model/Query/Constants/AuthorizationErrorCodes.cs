using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ligl.LegalManagement.Model.Query.Constants
{
    /// <summary>
    /// This class contain Authorization module related error codes
    /// </summary>
    public class AuthorizationErrorCodes : BaseErrorCodes
    {
        public const int UserNotFound = 130001;
        public const int ModuleNotFound = 130002;
        public const int DepartmentNotFound = 130003;
        public const int InValidUrl = 130004;
        public const int EntityNotAuthorized = 130005;
        public const int ErrorWhileAuthorizing = 130006;
        public AuthorizationErrorCodes() : base()
        {
            ErrorString[AuthorizationErrorCodes.UserNotFound] = "User not found";
            ErrorString[AuthorizationErrorCodes.ModuleNotFound] = "Module not found";
            ErrorString[AuthorizationErrorCodes.DepartmentNotFound] = "Department not found";
            ErrorString[AuthorizationErrorCodes.InValidUrl] = "The url is invalid or corrupted";
            ErrorString[AuthorizationErrorCodes.EntityNotAuthorized] = "The entity is not authorized for user";
            ErrorString[ErrorWhileAuthorizing] = "Error in Entity authorization";
        }
    }
}
