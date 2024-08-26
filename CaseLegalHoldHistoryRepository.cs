using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;


namespace Ligl.LegalManagement.Repository.DataAccess
{
    /// <summary>
    /// Class for CaseLegalHoldHistoryRepository
    /// </summary>
    /// <seealso cref="CaseLegalHoldHistory
    /// Ligl.CaseManagement.Repository.RegionContext&gt;" />
    /// <seealso cref="Ligl.LegalManagement.Repository.DataAccess.Interface.ICaseLegalHoldHistoryRepository" />
    public class CaseLegalHoldHistoryRepository(RegionContext context)
        : GenericRepository<CaseLegalHoldHistory, RegionContext>(context), ICaseLegalHoldHistoryRepository;

      
}
