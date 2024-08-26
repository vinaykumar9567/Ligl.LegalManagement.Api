using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{
    /// <summary>
    /// Class for CustodianAdditionalFieldsEntityRepository
    /// </summary>
    /// <seealso cref="CustodianAdditionalFieldsEntity" />
    /// <seealso cref="ICustodianAdditionalFieldsEntityRepository" />
    public class CustodianAdditionalFieldsEntityRepository : GenericRepository<CustodianAdditionalFieldsEntity, RegionContext>, ICustodianAdditionalFieldsEntityRepository
    {
        private readonly RegionContext _regionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustodianAdditionalFieldsEntityRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CustodianAdditionalFieldsEntityRepository(RegionContext context) : base(context)
        {
            _regionContext = context;
        }
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="caseId">The row identifier.</param>
        /// <returns></returns>
        public Task<CustodianAdditionalFieldsEntity?> GetByIdAsync(Guid caseId)
        {
            var caseDetail = _regionContext.custodianAdditionalFieldsEntities?.FirstOrDefault(e => e.UUID == caseId);
            return Task.FromResult(caseDetail);
        }   
    }
}
