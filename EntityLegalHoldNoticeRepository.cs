using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{
    /// <summary>
    /// Class for ViewCaseDetailRepository
    /// </summary>
    /// <seealso cref="RegionContext" />
    /// <seealso cref="EntityLegalHoldNoticeRepository" />
    public class EntityLegalHoldNoticeRepository : GenericRepository<EntityLegalHoldNotice, RegionContext>, IEntityLegalHoldNoticeRepository
    {
        private readonly RegionContext _regionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityLegalHoldNoticeRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EntityLegalHoldNoticeRepository(RegionContext context) : base(context)
        {
            _regionContext = context;
        }
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="caseId">The row identifier.</param>
        /// <returns></returns>
        public Task<EntityLegalHoldNotice> GetByIdAsync(Guid caseId)
        {
            var caseDetail = _regionContext.EntityLegalHoldNotices.FirstOrDefault(e => e.UUID == caseId);
            return Task.FromResult(caseDetail);
        }
    }
}
