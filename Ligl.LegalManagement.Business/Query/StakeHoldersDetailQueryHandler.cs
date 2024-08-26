using Ligl.Core.Sdk.Shared.Business.Region.Cache.Interface;
using Ligl.LegalManagement.Model.Query;
using Ligl.LegalManagement.Repository.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ligl.LegalManagement.Business.Query
{

    /// <summary>
    /// Class for StakeHoldersDetailQueryHandler
    /// </summary>
    /// <seealso cref="StakeHoldersDetailQueries" />
    public class StakeHoldersDetailQueryHandler(IRegionUnitOfWork regionUnitOfWork, ILogger<StakeHoldersDetailQueryHandler> logger) : IRequestHandler<StakeHoldersDetailQuery, List<StakeHoldersViewModel>>
    {
        private const string ClassName = nameof(StakeHoldersDetailQueryHandler);

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="AccessViolationException">Invalid user exception</exception>
        public async Task<List<StakeHoldersViewModel>> Handle(StakeHoldersDetailQuery request, CancellationToken cancellationToken)
        {
            const string methodName = $"{ClassName} - {nameof(Handle)}";
            try
            {
                logger.LogInformation(message: "Started execution of {methodName}", methodName);

                var caseid = (await regionUnitOfWork.CaseRepository.GetAsync()).FirstOrDefault(x => x.Uuid == request.Caseid && !x.IsDeleted)?.CaseId;
                var CaseLegalHoldId = (await regionUnitOfWork.CaseLegalHoldDetailRepository.GetAsync()).FirstOrDefault(x => x.UUID == request.CaseLegalHoldId && !x.IsDeleted)?.CaseLegalHoldID;

                var result = from s in await regionUnitOfWork.stakeHolderEntity.GetAsync()
                             join d in await regionUnitOfWork.DepartmentRepository.GetAsync() on s.DepartmentID equals d.DepartmentId 
                             join l in await regionUnitOfWork.LookupRepository.GetAsync() on s.Status equals l.LookupId into sl
                             from l in sl.DefaultIfEmpty()
                             join lc in await regionUnitOfWork.LookupRepository.GetAsync() on s.CategoryID equals lc.LookupId into slc
                             from lc in slc.DefaultIfEmpty()
                             join cs in await regionUnitOfWork.caseStakeHolderEntity.GetAsync() on new { CaseID = Convert.ToInt32(caseid),  s.StakeHolderID } equals new { CaseID = cs.CaseId, StakeHolderID = cs.StakeHolderId } into scs
                             from cs in scs.DefaultIfEmpty()
                             join sl2 in await regionUnitOfWork.LookupRepository.GetAsync() on cs.Status equals sl2.LookupId into sl2c
                             from sl2 in sl2c.DefaultIfEmpty()
                             join dp in await regionUnitOfWork.EntityLegalHoldNoticeRepository.GetAsync() on cs.CaseStakeHolderId equals dp.EntityID into dpJoin
                             from dp in dpJoin.DefaultIfEmpty()
                             join clh in await regionUnitOfWork.CaseLegalHoldDetailRepository.GetAsync() on dp.CaseLegalHoldID equals clh.CaseLegalHoldID into clhJoin
                             from clh in clhJoin.DefaultIfEmpty()
                             join ul in await regionUnitOfWork.UserLoginRepository.GetAsync() on dp.CreatedBy equals ul.Uuid.ToString().ToUpper() into ulJoin
                             from ul in ulJoin.DefaultIfEmpty()
                             join dpl in await regionUnitOfWork.LookupRepository.GetAsync() on dp.LHNStatusID equals dpl.LookupId into dplJoin
                             from dpl in dplJoin.DefaultIfEmpty()
                             join ds in await regionUnitOfWork.DocumentStreamRepository.GetAsync() on dp.EntityLegalHoldNoticeID equals ds.EntityId into dsJoin
                             from ds in dsJoin.DefaultIfEmpty()
                             where clh.CaseID==caseid && clh.CaseLegalHoldID== CaseLegalHoldId
                             select new StakeHoldersViewModel
                             {
                                 ID = s.UUID != null ? s.UUID : Guid.Empty,
                                 CaseStakeHolderID = cs.Uuid != Guid.Empty ? cs.Uuid :Guid.Empty,
                                 CaseID = request.Caseid,
                                 FullName = s.FullName,
                                 FirstName = s.FirstName,
                                 MiddleName = s.MiddleName,
                                 LastName = s.LastName,
                                 EmailAddress = s.EmailAddress,
                                 DepartmentID = d.DepartmentId,
                                 DepartmentName = d.Name,
                                 DpnID = dp.UUID != Guid.Empty ? dp.UUID : Guid.Empty,
                                 DpnStatusID =  dpl.Uuid != null ? dpl.Uuid : Guid.Empty,
                                 DpnStatusName = dpl.Name,
                                 Status = l.Status,
                                 StakeholderStatusName = l.Name,
                                 StatusChangeReason = s.StatusChangeReason,
                                 CaseStakeholderStatusID = sl2.Uuid !=null ? sl2.Uuid : Guid.Empty,
                                 CaseStakeholderStatusName = sl2.Name,
                                 RequestSentBy = ul.FullName,
                                 //RequestSentOn = dp.,
                                //AcknowledgedOn =(DateTime) dp.AcknowledgedOn,
                                 CaseLegalHoldID = 1,
                                 Exists = s.UUID != null && !cs.IsDeleted,
                DocumentStreamID = ds.Uuid != Guid.Empty ? ds.Uuid : Guid.Empty,
                                 //IsQuestionnaireAvailable = dbContext.QuestionnaireResponses
                                 //                            .Any(qr => qr.EntityID == dp.EntityLegalHoldNoticeID && qr.IsDeleted == 0),
                                 //IncludeStakeholders = @IncludeStakeholders,
                                 //s.CreatedOn,
                                 //CategoryID = lc.Uuid,
                                 CategoryName = lc.Name
                             };

                var resultList = result.ToList();
                return resultList;

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
