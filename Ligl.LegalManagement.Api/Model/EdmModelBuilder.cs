using Ligl.LegalManagement.Model.Query;
using Ligl.LegalManagement.Model.Query.CustomModels;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace Ligl.LegalManagement.Api.Model
{

    /// <summary>
    /// Class for EDM Model
    /// </summary>
    public class EdmModelBuilder
    {
        /// <summary>
        /// Gets the EDM Model
        /// </summary>
        /// <returns></returns>
        public static IEdmModel GetModels()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EnableLowerCamelCase();
            builder.EntitySet<EscalationReminderConfigViewModel>("EscalationAndReminder").EntityType.HasKey(x => x.UUID);
            builder.EntitySet<LegalCaseDetailViewModel>("CaseLegalHold").EntityType.HasKey(x => x.UUID);
            builder.EntitySet<InterviewEntityViewModel>("EntityInterview").EntityType.HasKey(x => x.UUID);
            builder.EntitySet<QuestionnaireTemplateViewModel>("CaseCustodianQuestionnaire").EntityType.HasKey(x => x.ID);
            builder.EntitySet<CasesMetaDataViewModel>("CaseStakeHolder").EntityType.HasKey(x => x.StakeHolderID);
            builder.EntitySet<CaseCustodianViewModel>("CaseCustodian").EntityType.HasKey(x => x.CaseCustodianUniqueID);
            builder.EntitySet<NotificationTemplateViewModel>("EmailTemplate").EntityType.HasKey(x=>x.NotificationTemplateID);
            builder.EntitySet<StakeHoldersViewModel>("StakeHolder").EntityType.HasKey(x => x.CaseID);
            return builder.GetEdmModel();
        }
    }
}
