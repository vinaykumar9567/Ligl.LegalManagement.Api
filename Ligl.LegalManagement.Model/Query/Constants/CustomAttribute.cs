using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ligl.LegalManagement.Model.Query.Constants
{

    public class EnumGuidAttribute : Attribute
    {
        public Guid Guid;
        public bool IsEndState;
        public bool IsQcAutomated;
        public bool isInQueueProcess;
        public Guid? ModuleId = null;



        /// <summary>
        /// Assigning the attribute value and custom attributes to filter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isEndState"></param>
        /// <param name="isQcAutomated"></param>
        public EnumGuidAttribute(string value, bool isEndState = false, bool isQcAutomated = false, bool isInQueue = false, string moduleId = null)
        {
            Guid = new Guid(value);
            IsEndState = isEndState;
            IsQcAutomated = isQcAutomated;
            isInQueueProcess = isInQueue;
            ModuleId = !string.IsNullOrEmpty(moduleId) ? new Guid(moduleId) : Guid.Empty;
        }
    }
}
