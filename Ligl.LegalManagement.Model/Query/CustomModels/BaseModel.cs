using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ligl.LegalManagement.Model.Query.CustomModels
{

    [XmlRoot(ElementName = "Initialize")]
    public class Initialize
    {
        [XmlAttribute(AttributeName = "Type")]
        public Guid Type { get; set; }
        [XmlText]
        public int Value { get; set; }
    }

    [XmlRoot(ElementName = "NotificationFrequency")]
    public class NotificationFrequency
    {
        [XmlAttribute(AttributeName = "frequencyType")]
        public Guid FrequencyType { get; set; }
        [XmlText]
        public int Value { get; set; }
    }

    public class BaseModel
    {
        [XmlElement(ElementName = "Initialize", IsNullable = true)]
        public Initialize? Initialize { get; set; }

        [XmlElement(ElementName = "NotificationFrequency", IsNullable = true)]
        public NotificationFrequency? NotificationFrequency { get; set; }

        public int NotificationCap { get; set; }

        public Guid EmailTemplate { get; set; }

        [XmlIgnore]
        public Guid? EscalationReminderConfigID { get; set; }

    }

    [XmlRoot(ElementName = "ReminderConfig")]
    public class ReminderConfig : BaseModel
    {
        public bool StopRemindersAfterEscalation { get; set; }
        public bool ReminderDeleted { get; set; }

        [XmlAttribute(AttributeName = "Type")]
        public Guid Type { get; set; }
    }

    [XmlRoot(ElementName = "EscalationConfig")]
    public class EscalationConfig : BaseModel
    {
        [XmlElement(ElementName = "EscalationEmail", IsNullable = true)]
        public string? EscalationEmail { get; set; }

        public bool EscalationDeleted { get; set; }

        [XmlAttribute(AttributeName = "Type")]
        public Guid Type { get; set; }
    }


    [XmlRoot(ElementName = "EscalationReminderConfig")]
    public class EscalationReminderConfig
    {

        [XmlElement(ElementName = "ReminderConfig", IsNullable = true)]
        public ReminderConfig ReminderConfig { get; set; } = null!;

        [XmlElement(ElementName = "EscalationConfig", IsNullable = true)]
        public EscalationConfig EscalationConfig { get; set; }= null!;

        [XmlIgnore]
        public Guid? EscalationReminderConfigID { get; set; }

    }

    [Serializable]
    public class EscalationReminderConfigModel : ReminderAndEscalationModel
    {
        public EscalationReminderConfig? EscalationReminderConfig { get; set; }
    }

  

    [Serializable]
    public class EscalationReminderConfigViewModel : ReminderAndEscalationModel
    {
        public EscalationReminderConfig EscalationReminderConfig { get; set; } = null!;
    }
}
