using Ligl.LegalManagement.Model.Query;
using Ligl.LegalManagement.Model.Query.Constants;
using Ligl.LegalManagement.Repository.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace Ligl.LegalManagement.Business.Query
{
    /// <summary>
    /// Class for CaseCustodianActionHIstoryDetailQeryHandler
    /// </summary>
    /// <seealso cref="CaseCustodianActionHIstoryDetailQery" />
    public class CaseCustodianActionHIstoryDetailQeryHandler(IRegionUnitOfWork regionUnitOfWork, ILogger<CaseCustodianActionHIstoryDetailQeryHandler> logger
       ) : IRequestHandler<CaseCustodianActionHIstoryDetailQery, IQueryable<CustodiansActionsHistoryViewModel>>
    {
        private const string ClassName = nameof(CaseCustodianActionHIstoryDetailQeryHandler);

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="AccessViolationException">Invalid user exception</exception>
        public async Task <IQueryable<CustodiansActionsHistoryViewModel>> Handle(CaseCustodianActionHIstoryDetailQery request, CancellationToken cancellationToken)
        {
            const string methodName = $"{ClassName} - {nameof(Handle)}";
            try
            {
                logger.LogInformation(message: "Started execution of {methodName}", methodName);        

                var custodiansActions = from enlh in await regionUnitOfWork.EntityLegalHoldNoticeHistory.GetAsync()
                                        join caseCustodians in await regionUnitOfWork.CaseCustodianRepository.GetAsync()
                                        on enlh.EntityID equals caseCustodians.CaseCustodianId
                                        join custodians in await regionUnitOfWork.CustodianRepository.GetAsync() on
                                        caseCustodians.CustodianId equals custodians.CustodianId
                                        join caseholds in await regionUnitOfWork.CaseLegalHoldDetailRepository.GetAsync() on
                                        enlh.CaseLegalHoldID equals caseholds.CaseLegalHoldID
                                        join lkp in await regionUnitOfWork.LookupEntityRepository.GetAsync() on
                                        enlh.LHNStatusID equals lkp.LookupEntityId
                                        where enlh.EntityTypeID == (int)EntityTypes.CaseCustodian && enlh.ResendCount == null //&& enlh.UserActions == null
                                        orderby enlh.ModifiedOn descending

                                        select new CustodiansActionsHistoryViewModel
                                        {
                                            CustodianName = custodians.FullName,
                                            SentOn = enlh.CreatedOn,
                                            LhnStatus = lkp.Name,
                                            EntityUniqueID = caseCustodians.Uuid,
                                            EntityID = caseCustodians.CaseCustodianId,
                                            CaseLegalHoldUniqueID = caseholds.UUID,
                                            UUID = enlh.UUID,
                                            ModifiedOn = enlh.ModifiedOn,
                                        };
                return custodiansActions;            
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
