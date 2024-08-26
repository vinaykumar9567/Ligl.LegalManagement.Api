using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{

    /// <summary>
    /// Class for CaseStakeHolderEntityRepository
    /// </summary>
    /// <seealso cref="CaseStakeHolder" />
    /// <seealso cref="ICaseStakeHolderEntity" />
    public class CaseStakeHolderEntityRepository : GenericRepository<CaseStakeHolder, RegionContext>, ICaseStakeHolderEntity
    {
        private readonly RegionContext _regionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CaseStakeHolderEntityRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CaseStakeHolderEntityRepository(RegionContext context) : base(context)
        {
            _regionContext = context;
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="rowId">The row identifier.</param>
        /// <returns></returns>
        public Task<CaseStakeHolder?> GetByIdAsync(Guid caseId)
        {
            var caseDetail = _regionContext.CaseStakeHolderEntities?.FirstOrDefault(e => e.Uuid == caseId);
            return Task.FromResult(caseDetail);
        }
    }
}
