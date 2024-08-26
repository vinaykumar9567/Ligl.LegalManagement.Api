using Ligl.LegalManagement.Model.Query;
using MediatR;
using Microsoft.Extensions.Logging;
using Ligl.LegalManagement.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Ligl.LegalManagement.Model.Common;
using Ligl.Core.Sdk.Shared.Business.Region.Cache.Interface;

namespace Ligl.LegalManagement.Business.Query
{
    /// <summary>
    /// Class for GetCaseStakeHoldersDetailsQueryHandler
    /// </summary>
    /// <seealso cref="GetCaseStakeHoldersDetailsQuery" />
    public class GetCaseStakeHoldersDetailsQueryHandler(IRegionUnitOfWork regionUnitOfWork,  ILookUpBusiness lookUpBusiness ,ILogger<GetCaseStakeHoldersDetailsQueryHandler> logger) : IRequestHandler<GetCaseStakeHoldersDetailsQuery, List<CasesMetaDataViewModel>>
    {
        private const string ClassName = nameof(GetCaseStakeHoldersDetailsQueryHandler);

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="AccessViolationException">Invalid user exception</exception>
        public async Task<List<CasesMetaDataViewModel>> Handle(GetCaseStakeHoldersDetailsQuery request, CancellationToken cancellationToken)
        {
            const string methodName = $"{ClassName} - {nameof(Handle)}";
            try
            {
                logger.LogInformation(message: "Started execution of {methodName}", methodName);           
                Guid? awaitingAcknowledgement = lookUpBusiness.GetDomainValueById(id: DPNStatus.AwaitingAcknowledgement.id).Uuid;
                Guid? stealthMode =  lookUpBusiness.GetDomainValueById(id: DPNStatus.StealthMode.id).Uuid;
                Guid? revoke = lookUpBusiness.GetDomainValueById(id: DPNStatus.Revoke.id).Uuid;
                Guid? acknowledged = lookUpBusiness.GetDomainValueById(id: DPNStatus.Acknowledged.id).Uuid;
                Guid? released =  lookUpBusiness.GetDomainValueById(id: DPNStatus.Released.id).Uuid;
                var caseList = (from stakeholder in await regionUnitOfWork.stakeHolderEntity.GetAsync()
                                join caseStakeHolder in await regionUnitOfWork.caseStakeHolderEntity.GetAsync() on stakeholder.StakeHolderID equals caseStakeHolder.StakeHolderId
                                join cases in await regionUnitOfWork.CaseRepository.GetAsync() on caseStakeHolder.CaseId equals cases.CaseId
                                join CaseLegalHolds in await regionUnitOfWork.CaseLegalHoldDetailRepository.GetAsync() on cases.CaseId equals CaseLegalHolds.CaseID
                                join elhn in await regionUnitOfWork.EntityLegalHoldNoticeRepository.GetAsync() on CaseLegalHolds.CaseLegalHoldID equals elhn.CaseLegalHoldID
                                join lookups in await regionUnitOfWork.LookupRepository.GetAsync() on elhn.LHNStatusID equals lookups.LookupId
                                where stakeholder.UUID == request.CaseId && !stakeholder.IsDeleted.Value && !caseStakeHolder.IsDeleted
                                && !cases.IsDeleted && !CaseLegalHolds.IsDeleted && !elhn.IsDeleted && (lookups.Uuid == awaitingAcknowledgement || lookups.Uuid == acknowledged || lookups.Uuid == released || lookups.Uuid == stealthMode)
                                select new CasesMetaDataViewModel
                                {
                                    StakeHolderID = stakeholder.UUID,
                                    CaseID = cases.Uuid,
                                    CaseName = cases.Name
                                }
                       ).Distinct().ToList();
                return caseList;
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
