using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{
    /// <summary>
    /// Class for EntityLegalHoldNoticeHistoryRepository
    /// </summary>
    /// <seealso cref="EntityLegalHoldNoticeHistory
    /// Ligl.LegalManagement.Repository.RegionContext&gt;" />
    /// <seealso cref="Ligl.LegalManagement.Repository.DataAccess.Interface.ICaseAdditionalFieldRepository" />
    public class EntityLegalHoldNoticeHistoryRepository(RegionContext context)
        : GenericRepository<EntityLegalHoldNoticeHistory, RegionContext>(context), IEntityLegalHoldNoticeHistoryRepository
    {
        private readonly RegionContext _regionContext = context;

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="rowId">The row identifier.</param>
        /// <returns></returns>
        public Task<EntityLegalHoldNoticeHistory?> GetByIdAsync(Guid rowId)
        {
            var caseAdditionalField = _regionContext.EntityLegalHoldNoticeHistory?.FirstOrDefault(e => e.UUID == rowId);
            return Task.FromResult(caseAdditionalField);
        }

    }
}
