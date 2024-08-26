namespace Ligl.LegalManagement.Model.Common;
/// <summary>
/// Struct for DPNStatus
/// </summary>
public readonly struct DPNStatus
{
    public static readonly (Guid id, int primaryId) NotInitiated = (Guid.Parse("844C467A-9958-4F53-ACF7-E4DF55165E73"), 72);
    public static readonly (Guid id, int primaryId) AwaitingAcknowledgement = (Guid.Parse("1AC041F3-4AB6-4255-A102-384187A1F3F6"), 73);
    public static readonly (Guid id, int primaryId) SentReminder = (Guid.Parse("B942DB1A-2738-4B31-90F2-3004181FA26F"), 74);
    public static readonly (Guid id, int primaryId) Acknowledged = (Guid.Parse("6C91993C-509D-48EE-BBB7-2687C0FDB9E8"), 75);
    public static readonly (Guid id, int primaryId) Released = (Guid.Parse("4D0CAA75-0713-4483-8F67-5DA7226362A7"), 76);
    public static readonly (Guid id, int primaryId) StealthMode = (Guid.Parse("02AE5BAC-F7DE-4C35-9A69-B1FA8A8D2114"), 609);
    public static readonly (Guid id, int primaryId) Revoke = (Guid.Parse("6C91993C-509D-48EE-BBB7-2687C0FDB9E8"), 610);
    public static readonly (Guid id, int primaryId) EscalationSent = (Guid.Parse("6C91993C-509D-48EE-BBB7-2687C0FDB9E8"), 6118);
    public static readonly (Guid id, int primaryId) Resend = (Guid.Parse("AD4B2BC1-C207-4E82-BA26-D3518EF8E8C3"), 8630);
}
/// <summary>
/// Struct for StatusType
/// </summary>
public readonly struct StatusType
{
    public static readonly (Guid id, int primaryId) Active = (Guid.Parse("63498DFF-1F1E-464C-96A6-0B82BC18688D"), 39);
    public static readonly (Guid id, int primaryId) NotInitiated = (Guid.Parse("4EAFC28F-9A54-40AD-B519-F8B7C1E8311E"), 48);


}
/// <summary>
/// Struct for defaultDrKwID
/// </summary>
public readonly struct defaultDrKwID
{
    public static readonly int DateRangeID = 1;
    public static readonly int KeyWordID = 1;
}
/// <summary>
/// Struct for NotificationTemplateTypes
/// </summary>
public readonly struct NotificationTemplateTypes
{
    public static readonly Guid Email = (Guid.Parse("A55DB506-4C9D-4332-B4BE-EF39DDB75636"));   
}
/// <summary>
/// Struct for LookUpEnums
/// </summary>
public readonly struct LookUpEnums

{
    public static readonly (Guid id, int primaryId) DefaultLanguageSetting = (Guid.Parse("55FA1820-77A9-4320-897F-5D51F7444EB2"), 425);

}
/// <summary>
/// Struct for Status
/// </summary>
public readonly struct Status
{
    public static readonly (Guid id, int primaryId) Active = (Guid.Parse("63498DFF-1F1E-464C-96A6-0B82BC18688D"), 39);

}
/// <summary>
/// Struct for EntityType
/// </summary>
public readonly struct EntityType
{
    public static readonly (Guid id, int primaryId) CaseCustodian = (Guid.Parse("F0352A6B-852E-4F8B-A217-742492CB19F4"), 2);
    public static readonly (Guid id, int primaryId) CaseStakeHolder = (Guid.Parse("0B0EB6DA-A1E2-403F-BAA0-C064D4B8F61D"), 16);
    public static readonly (Guid id, int primaryId) Interview = (Guid.Parse("B18FB8F3-8ECA-417F-A007-E6EC25763E77"), 17);
}
/// <summary>
/// Struct for ReminderandEscalationTemplate
/// </summary>
public readonly struct ReminderandEscalationTemplate
{
  
       public static readonly Guid ReminderandEscalationTemplateID = (Guid.Parse("7EFC6AC1-5518-43E9-B042-325C87070101"));  

}
/// <summary>
/// Struct for SQLErrorCodes
/// </summary>
public readonly struct SQLErrorCodes
{
    public static readonly string NotFound = "1001";
}



