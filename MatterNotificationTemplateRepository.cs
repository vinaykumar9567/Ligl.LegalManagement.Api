using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{
    /// <summary>
    /// Class for MatterNotificationTemplateRepository
    /// </summary>
    /// <seealso cref="MatterNotificationTemplate" />
    /// <seealso cref="IMatterNotificationTemplate" />
    public class MatterNotificationTemplateRepository : GenericRepository<MatterNotificationTemplate, RegionContext>, IMatterNotificationTemplate
    {
        private readonly RegionContext _regionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatterNotificationTemplateRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public MatterNotificationTemplateRepository(RegionContext context) : base(context)
        {
            _regionContext = context;
        }
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="caseId">The row identifier.</param>
        /// <returns></returns>
        public Task<MatterNotificationTemplate?> GetByIdAsync(Guid caseId)
        {
            var caseDetail = _regionContext.MatterNotificationTemplates?.FirstOrDefault(e => e.UUID == caseId);
            return Task.FromResult(caseDetail);
        }
    }
}
