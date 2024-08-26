using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{
    /// <summary>
    /// Class for QuestionnaireResponseRepository
    /// </summary>
    /// <seealso cref="QuestionnaireResponse,RegionBaseContext" />
    /// <seealso cref="IQuestionnaireResponseRepository" />
    public class QuestionnaireResponseRepository(RegionContext context)
        : GenericRepository<QuestionnaireResponse, RegionContext>(context), IQuestionnaireResponseRepository
    {
        private readonly RegionContext _regionContext = context;
    }
}
