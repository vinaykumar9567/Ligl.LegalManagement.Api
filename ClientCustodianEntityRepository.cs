using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{
    /// <summary>
    /// Class for ClientCustodianEntityRepository
    /// </summary>
    /// <seealso cref="ClientCustodianEntity" />
    /// <seealso cref="IClientCustodianEntityRepository" />
    public class ClientCustodianEntityRepository : GenericRepository<ClientCustodianEntity, RegionContext>, IClientCustodianEntityRepository
    {
        private readonly RegionContext _regionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientCustodianEntityRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ClientCustodianEntityRepository(RegionContext context) : base(context)
        {
            _regionContext = context;
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="caseId">The row identifier.</param>
        /// <returns></returns>
        public Task<ClientCustodianEntity?> GetByIdAsync(Guid caseId)
        {
            var caseDetail = _regionContext.ClientCustodianEntities?.FirstOrDefault(e => e.UUID == caseId);
            return Task.FromResult(caseDetail);
        }

    }
}
