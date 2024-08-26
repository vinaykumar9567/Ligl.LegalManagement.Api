using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{
    /// <summary>
    /// Class for EntityLHNAdditionalEntityRepository
    /// </summary>
    /// <seealso cref="EntityLHNAdditionalEntity" />
    /// <seealso cref="IEntityLHNAdditionalEntityRepository" />
    public class EntityLHNAdditionalEntityRepository : GenericRepository<EntityLHNAdditionalEntity, RegionContext>, IEntityLHNAdditionalEntityRepository
    {
        private readonly RegionContext _regionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityLHNAdditionalEntityRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EntityLHNAdditionalEntityRepository(RegionContext context) : base(context)
        {
            _regionContext = context;
        }
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="caseId">The row identifier.</param>
        /// <returns></returns>
        public Task<EntityLHNAdditionalEntity?> GetByIdAsync(Guid caseId)
        {
            var caseDetail = _regionContext.EntityLHNAdditionalEntities?.FirstOrDefault(e => e.UUID == caseId);
            return Task.FromResult(caseDetail);
        }
    }
}
