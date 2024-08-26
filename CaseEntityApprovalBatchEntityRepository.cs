using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{
    /// <summary>
    /// Class for CaseEntityApprovalBatchEntityRepository
    /// </summary>
    /// <seealso cref="RegionContext" />
    /// <seealso cref="ICaseEntityApprovalBatchEntityRepository" />
    public class CaseEntityApprovalBatchEntityRepository : GenericRepository<CaseEntityApprovalBatchEntity, RegionContext>, ICaseEntityApprovalBatchEntityRepository
    {
        private readonly RegionContext _regionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CaseEntityApprovalBatchEntityRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CaseEntityApprovalBatchEntityRepository(RegionContext context) : base(context)
        {
            _regionContext = context;
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="caseId">The row identifier.</param>
        /// <returns></returns>
        public Task<CaseEntityApprovalBatchEntity?> GetByIdAsync(Guid caseId)
        {
            var caseDetail = _regionContext.CaseEntityApprovalBatchEntities?.FirstOrDefault(e => e.UUID == caseId);
            return Task.FromResult(caseDetail);
        }
    }
}
