using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;


namespace Ligl.LegalManagement.Repository.DataAccess
{

    /// <summary>
    /// Class for CaseAdditionalFieldRepository
    /// </summary>
    /// <seealso cref="CaseAdditionalField
    /// Ligl.CaseManagement.Repository.RegionContext&gt;" />
    /// <seealso cref="Ligl.LegalManagement.Repository.DataAccess.Interface.ICaseAdditionalFieldRepository" />
    public class CaseAdditionalFieldRepository(RegionContext context)
        : GenericRepository<CaseAdditionalField, RegionContext>(context), ICaseAdditionalFieldRepository;
    
}
