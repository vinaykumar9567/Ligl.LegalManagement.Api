using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{

    /// <summary>
    /// Class for CaseCustodianRepository
    /// </summary>
    /// <seealso cref="RegionContext" />
    /// <seealso cref="ICaseCustodianRepository" />
    public class CaseCustodianRepository : GenericRepository<CaseCustodian, RegionContext>, ICaseCustodianRepository
    {
        private readonly RegionContext _regionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CaseCustodianRepository"/> class.
        /// Initializes a new instance of the <see cref="CaseCustodianRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CaseCustodianRepository(RegionContext context) : base(context)
        {
            _regionContext = context;
        }
        public Task<CaseCustodian?> GetByIdAsync(int caseId)
        {
            var caseCustodianDetail = _regionContext.CaseCustodians?.FirstOrDefault(e => e.CaseId == caseId);
            return Task.FromResult(caseCustodianDetail);
        }
    }
}
