using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.Domain;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
namespace Ligl.LegalManagement.Repository.DataAccess
{
    /// <summary>
    /// Class for QuestionnaireRepository
    /// </summary>
    /// <seealso cref="Questionnaire
    /// Ligl.CaseManagement.Repository.RegionContext&gt;" />
    /// <seealso cref="Ligl.LegalManagement.Repository.DataAccess.Interface.ICaseAdditionalFieldRepository" />
    public class QuestionnaireRepository(RegionContext context)
        : GenericRepository<Questionnaire, RegionContext>(context), IQuestionnaireRepository
    {
        private readonly RegionContext _regionContext = context;
    }
}
