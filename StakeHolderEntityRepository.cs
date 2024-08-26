using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{
    /// <summary>
    /// Class for StakeHolderEntityRepository
    /// </summary>
    /// <seealso cref="StakeHolder" />
    /// <seealso cref="IStakeHolderEntityRepository" />
    public class StakeHolderEntityRepository : GenericRepository<StakeHolder, RegionContext>, IStakeHolderEntityRepository
    {
        private readonly RegionContext _regionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="StakeHolderEntityRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public StakeHolderEntityRepository(RegionContext context) : base(context)
        {
            _regionContext = context;
        }
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="caseId">The row identifier.</param>
        /// <returns></returns>
        public Task<StakeHolder?> GetByIdAsync(Guid caseId)
        {
            var caseDetail = _regionContext.StakeHolderEntities?.FirstOrDefault(e => e.FirstName=="");
            return Task.FromResult(caseDetail);
        }
    }
}
