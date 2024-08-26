using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{

    /// <summary>
    /// Class for EntityApprovalRepository
    /// </summary>
    /// <seealso cref="EntityApproval" />
    /// <seealso cref="IEntityApprovalRepository" />
    public class EntityApprovalRepository : GenericRepository<EntityApproval, RegionContext>, IEntityApprovalRepository
    {
        private readonly RegionContext _regionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityApprovalRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EntityApprovalRepository(RegionContext context) : base(context)
        {
            _regionContext = context;
        }
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="caseId">The row identifier.</param>
        /// <returns></returns>
        public Task<EntityApproval?> GetByIdAsync(Guid caseId)
        {
            var caseDetail = _regionContext.EntityApprovals?.FirstOrDefault(e => e.UUID == caseId);
            return Task.FromResult(caseDetail);
        }
    }
}
