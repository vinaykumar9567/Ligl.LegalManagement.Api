using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.DataAccess
{
    /// <summary>
    /// Class for ReminderAndEscalationRepository
    /// </summary>
    /// <seealso cref="ReminderAndEscalation" />
    /// <seealso cref="IReminderAndEscalation" />
    public class ReminderAndEscalationRepository : GenericRepository<ReminderAndEscalation, RegionContext>, IReminderAndEscalation
    {
        private readonly RegionContext _regionContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReminderAndEscalationRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ReminderAndEscalationRepository(RegionContext context) : base(context)
        {
            _regionContext = context;
        }
        public Task<ReminderAndEscalation?> GetByIdAsync()
        {
            var caseDetail = _regionContext.ReminderAndEscalation?.FirstOrDefault(e => e.ReminderAndEscalationID == 1);
            return Task.FromResult(caseDetail);
        }
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="UUID">The row identifier.</param>
        /// <returns></returns>
        public Task<ReminderAndEscalation?> GetReminderByIdAsync(Guid? UUID)
        {
            var caseDetail = _regionContext.ReminderAndEscalation?.FirstOrDefault(x => x.UUID == UUID);
            return Task.FromResult(caseDetail);
        }


    }
}
