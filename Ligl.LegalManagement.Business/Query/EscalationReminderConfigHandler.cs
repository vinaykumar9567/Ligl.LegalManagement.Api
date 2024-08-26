using Ligl.Core.Sdk.Shared.Model.Principal;
using Ligl.LegalManagement.Model.Query;
using Ligl.LegalManagement.Model.Query.CustomModels;
using MediatR;
using Microsoft.Extensions.Logging;
using Ligl.LegalManagement.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Ligl.Core.Sdk.Common.Helper;

namespace Ligl.LegalManagement.Business.Query
{
    /// <summary>
    /// Class for EscalationReminderConfigHandler
    /// </summary>
    /// <seealso cref="EscalationReminderConfigDetailQuery" />
    public class EscalationReminderConfigHandler(IRegionUnitOfWork regionUnitOfWork, ILogger<EscalationReminderConfigHandler> logger, IHttpContextAccessor contextAccessor) : IRequestHandler<EscalationReminderConfigDetailQuery, List<EscalationReminderConfigViewModel>>
    {
        private const string ClassName = nameof(EscalationReminderConfigHandler);

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="AccessViolationException">Invalid user exception</exception>
        public async Task <List<EscalationReminderConfigViewModel>> Handle(EscalationReminderConfigDetailQuery request, CancellationToken cancellationToken)
        {
            const string methodName = $"{ClassName} - {nameof(Handle)}";
            try
            {
                logger.LogInformation(message: "Started execution of {methodName}", methodName);

                if (contextAccessor.HttpContext.User is not CustomPrincipal currentPrincipal)
                {
                    logger.LogError("Error in {methodName} - invalid principal", methodName);
                    throw new AccessViolationException("Invalid user exception");
                }
                logger.LogInformation(message: "Started execution of {methodName}", methodName);       
                var isDeleted = false;
                var results = (from config in await regionUnitOfWork.ReminderAndEscalationRepository.GetAsync()
                               join caseLegalHold in await regionUnitOfWork.CaseLegalHoldDetailRepository.GetAsync() on
                                new { CaseLegalHoldID = config.CaseLegalHoldID.Value , IsDeleted = isDeleted }
                                equals
                                new {  caseLegalHold.CaseLegalHoldID,  caseLegalHold.IsDeleted  }
                                    into caseLHDetails
                               from caseLHDetail in caseLHDetails.DefaultIfEmpty()
                            //   where config.UUID == request.CaseId || request.CaseId != null
                               select new
                                EscalationReminderConfigViewModel
                               {
                                   UUID = config.UUID,
                                   ReminderConfig = config.ReminderConfig!,
                                   EscalationConfig = config.EscalationConfig!,
                                   CaseLegalHoldUniqueID = caseLHDetail.UUID != null ? caseLHDetail.UUID : Guid.Empty,
                                   EscalationReminderConfig = new EscalationReminderConfig
                                   {
                                       EscalationConfig = new EscalationConfig(),
                                       ReminderConfig = new ReminderConfig()
                                   }
                               } ).ToList();

                foreach (var result in results)
                {
                    result.EscalationReminderConfig.ReminderConfig = SerializationHelper.XMLDeserializeObject<ReminderConfig>( result.ReminderConfig);
                    result.EscalationReminderConfig.EscalationConfig = SerializationHelper.XMLDeserializeObject<EscalationConfig>(result.EscalationConfig);
                }
                return results;
              
            }
            catch (Exception e)
            {
                logger.LogError("Error in {Name} - {Message} /n {Trace}",
                    methodName, e.Message, e.StackTrace);
                throw;
            }
            finally
            {
                logger.LogInformation(message: "Completed execution of {methodName}", methodName);
            }
        }
    }
}
