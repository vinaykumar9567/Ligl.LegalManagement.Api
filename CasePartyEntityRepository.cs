using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{
    /// <summary>
    /// Class for CaseAdditionalFieldRepository
    /// </summary>
    /// <seealso cref="CasePartyEntity
    /// Ligl.CaseManagement.Repository.RegionContext&gt;" />
    /// <seealso cref="Ligl.LegalManagement.Repository.DataAccess.Interface.ICaseAdditionalFieldRepository" />
    public class CasePartyEntityRepository(RegionContext context)
        : GenericRepository<CasePartyEntity, RegionContext>(context), ICasePartyEntityRepository
    {
        private readonly RegionContext _regionContext = context;
   


        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="casePrimaryId">The case primary identifier.</param>
        /// <returns></returns>
        public Task<CasePartyEntity?> GetByIdAsync(int casePrimaryId)
        {
            var caseAdditionalField = _regionContext.CasePartyEntities?.FirstOrDefault(e => e.CasePartyId == casePrimaryId);
            return Task.FromResult(caseAdditionalField);
        }
    }
}
