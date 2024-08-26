using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{
    /// <summary>
    /// Class for InterviewEntityRepository
    /// </summary>
    /// <seealso cref="RegionContext" />
    /// <seealso cref="InterviewEntityRepository" />
    public class InterviewEntityRepository : GenericRepository<InterviewEntity, RegionContext>, IInterviewEntityRepository
    {
        private readonly RegionContext _regionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterviewEntityRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public InterviewEntityRepository(RegionContext context) : base(context)
        {
            _regionContext = context;
        }
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="caseId">The row identifier.</param>
        /// <returns></returns>
        public Task<InterviewEntity?> GetByIdAsync(Guid caseId)
        {
            var caseDetail = _regionContext.InterviewEntities?.FirstOrDefault(e => e.UUID == caseId);
            return Task.FromResult(caseDetail);
        }
    }
}
