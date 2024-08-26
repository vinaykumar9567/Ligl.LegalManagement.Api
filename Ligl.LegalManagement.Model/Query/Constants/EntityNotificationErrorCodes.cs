using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ligl.LegalManagement.Model.Query.Constants
{
    /// <summary>
    /// EntityNotificationErrorCodes
    /// </summary>
    public class EntityNotificationErrorCodes : BaseErrorCodes
    {

        public const int ReminderEscalationConfigNotFound = 220033;
        public const int FailureInUpdatingConfig = 220032;
        public const int InValidEntityType = 220003;

        public EntityNotificationErrorCodes()
        {

            ErrorString[ReminderEscalationConfigNotFound] = "Reminder and escalation config not found";
            ErrorString[FailureInUpdatingConfig] = "Failure in updating the escalation and reminder config";
            ErrorString[InValidEntityType] = "Selected entity type is not a valid";


        }
    }
}
