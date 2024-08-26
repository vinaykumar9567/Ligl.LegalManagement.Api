using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{
    /// <summary>
    /// Class for VWApprovalBatchEntityRepository
    /// </summary>
    /// <seealso cref="vw_ApprovalBatchEntity" />
    /// <seealso cref="IVWApprovalBatchEntityRepository" />
    public class VWApprovalBatchEntityRepository : GenericRepository<vw_ApprovalBatchEntity, RegionContext>, IVWApprovalBatchEntityRepository
    {
        private readonly RegionContext _regionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="vw_ApprovalBatchEntity"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public VWApprovalBatchEntityRepository(RegionContext context) : base(context)
        {
            _regionContext = context;
        }
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="caseId">The row identifier.</param>
        /// <returns></returns>
        public Task<vw_ApprovalBatchEntity?> GetByIdAsync(Guid caseId)
        {
            var caseDetail = _regionContext.vw_ApprovalBatchEntities?.FirstOrDefault(e => e.ApprovalBatchUniqueID == caseId);
            return Task.FromResult(caseDetail);
        }
    }
}
