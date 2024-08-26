namespace Ligl.LegalManagement.Model.Query.CustomModels
{
    /// <summary>
    /// License Notification Response
    /// </summary>
    public class LicenseNotificationResponse
    {
        public DateTime ExpiryDate { get; set; }
        public string AllCapsStats { get; set; }
        public string ValidatorName { get; set; }
        public double ValidatorPercentage { get; set; }
        public Guid ValidatorUniqueID { get; set; }
        public Dictionary<string, LatestNotificationDetails> LatestNotificationDetails { get; set; }
    }

    /// <summary>
    /// Latest Notification Details
    /// </summary>
    public class LatestNotificationDetails
    {
        public string UpperBoundary { get; set; }
        public string LowerBoundary { get; set; }
    }

    /// <summary>
    /// Latest Notification Details Root
    /// </summary>
    public class LatestNotificationDetailsRoot
    {
        public string Key { get; set; }
        public LatestNotificationDetails Value { get; set; }
    }
}
