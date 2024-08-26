using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{
    /// <summary>
    /// Class for QuestionnaireTemplateEntityRespository
    /// </summary>
    /// <seealso cref="RegionContext" />
    /// <seealso cref="QuestionnaireTemplateEntityRespository" />
    public class QuestionnaireTemplateEntityRespository : GenericRepository<QuestionnaireTemplateEntity, RegionContext>, IQuestionnaireTemplateEntity
    {
        private readonly RegionContext _regionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireTemplateEntityRespository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public QuestionnaireTemplateEntityRespository(RegionContext context) : base(context)
        {
            _regionContext = context;
        }
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="caseId">The row identifier.</param>
        /// <returns></returns>
        public Task<QuestionnaireTemplateEntity?> GetByIdAsync(Guid caseId)
        {
            var caseDetail = _regionContext.QuestionnaireTemplateEntities?.FirstOrDefault(e => e.UUID == caseId);
            return Task.FromResult(caseDetail);
        }
    }
}
