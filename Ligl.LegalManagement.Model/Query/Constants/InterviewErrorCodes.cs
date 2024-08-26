namespace Ligl.LegalManagement.Model.Query.Constants
{
    /// <inheritdoc />
    /// <summary>
    /// InterviewErrorCodes
    /// </summary>
    public class InterviewErrorCodes : BaseErrorCodes
    {
        public const int EntityNotExists = 230001;
        public const int NullEntityType = 230002;


        public InterviewErrorCodes()
        {
            ErrorString[EntityNotExists] = "Selected entity is not valid";
            ErrorString[NullEntityType] = "Entity type ID is not valid";
        }
    }
}
