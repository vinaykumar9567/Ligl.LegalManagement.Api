using Ligl.LegalManagement.Model.Query;
using Ligl.LegalManagement.Repository.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace Ligl.LegalManagement.Business.Query
{

    /// <summary>
    /// Class for CaseCustodianDetailQueryHandler
    /// </summary>
    /// <seealso cref="CaseCustodianDetailQuery" />
    public class CaseCustodianDetailQueryHandler(IRegionUnitOfWork regionUnitOfWork, ILogger<CaseCustodianDetailQueryHandler> logger) : IRequestHandler<CaseCustodianDetailQuery, IQueryable<CaseCustodianViewModel>>
    {
        private const string ClassName = nameof(CaseCustodianDetailQueryHandler);
        
        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="AccessViolationException">Invalid user exception</exception>
        public async Task<IQueryable<CaseCustodianViewModel>> Handle(CaseCustodianDetailQuery request, CancellationToken cancellationToken)
        {
            const string methodName = $"{ClassName} - {nameof(Handle)}";
            try
            {
                logger.LogInformation(message: "Started execution of {methodName}", methodName);

                var caseid = (await regionUnitOfWork.CaseRepository.GetAsync()).FirstOrDefault(x => x.Uuid == request.CaseId && !x.IsDeleted)?.CaseId;
                var CaseLegalHoldId = (await regionUnitOfWork.CaseLegalHoldDetailRepository.GetAsync()).FirstOrDefault(x => x.UUID == request.CaseLegalHoldID && !x.IsDeleted)?.CaseLegalHoldID;
                int @DPN_NOTINITIATED = 72;
                int @CASECUSTODIANS_ENTITY_ID = 2;
                int @ENTITY_LEGALHOLD_NOTICE_ID = 21;
                //int @NOTINITIATED = 48;
                //int @L_DepartmentID;
                var dytty = await regionUnitOfWork.CustodianRepository.GetAsync();
                var result = from cc in await regionUnitOfWork.CaseCustodianRepository.GetAsync()
                             join c in await regionUnitOfWork.CustodianRepository.GetAsync() on cc.CustodianId equals c.CustodianId
                             join em in await regionUnitOfWork.EmployeeMasterRepository.GetAsync() on c.EmployeeMasterId equals em.EmployeeMasterId
                             join d in await regionUnitOfWork.DepartmentRepository.GetAsync() on c.DepartmentId equals d.DepartmentId
                             join cem in (
                                 from clientCustodian in await regionUnitOfWork.ClientCustodianEntityRepository.GetAsync()
                                 join caseParty in await regionUnitOfWork.casePartyEntityRepository.GetAsync() on clientCustodian.ClientID equals caseParty.PartyId
                                 join party in await regionUnitOfWork.PartyRepository.GetAsync() on clientCustodian.ClientID equals party.PartyId
                                 where !clientCustodian.IsDeleted && !caseParty.IsDeleted
                                 select new
                                 {
                                     clientCustodian.CustodianID,
                                     clientCustodian.ClientID,
                                     caseParty.CaseId,
                                     ClientName = party.FullName
                                 }
                             ) on new { cc.CustodianId, cc.CaseId } equals new { CustodianId = cem.CustodianID, cem.CaseId } into cemJoin
                             from cem in cemJoin.DefaultIfEmpty()
                             join l in await regionUnitOfWork.LookupRepository.GetAsync() on cc.Status equals l.LookupId   into lJoin
                             from l in lJoin.DefaultIfEmpty()
                             join en in await regionUnitOfWork.EntityLegalHoldNoticeRepository.GetAsync() on new { EntityTypeID = @CASECUSTODIANS_ENTITY_ID, EntityID = cc.CaseCustodianId } equals new { en.EntityTypeID, en.EntityID } into enJoin

                             from en in enJoin.DefaultIfEmpty() 
                             join ul in await regionUnitOfWork.UserLoginRepository.GetAsync()  on en.SentBy  equals ul.Uuid.ToString().ToUpper() into ulJoin
                             from ul in ulJoin.DefaultIfEmpty()
                             join ld in await regionUnitOfWork.LookupRepository.GetAsync() on new { LookupID = (en.LHNStatusID == 0 ? en.LHNStatusID : @DPN_NOTINITIATED) } equals new { LookupID = ld.LookupId } into ldJoin
                             from ld in ldJoin.DefaultIfEmpty()
                             join ds in await regionUnitOfWork.DocumentStreamRepository.GetAsync() on new { EntityID = en.EntityLegalHoldNoticeID, EntityTypeID = @ENTITY_LEGALHOLD_NOTICE_ID } equals new { EntityID = (int)ds.EntityId, EntityTypeID = ds.EntityTypeId } into dsJoin
                             from ds in dsJoin.DefaultIfEmpty()
                             join clh in await regionUnitOfWork.CaseLegalHoldDetailRepository.GetAsync() on new { CaseID = cc.CaseId } equals new { clh.CaseID } into clhJoin
                             from clh in clhJoin.DefaultIfEmpty()
                             join qt in await regionUnitOfWork.QuestionnaireTemplateEntityRespository.GetAsync() on clh.CustodianQuestionnaireTemplateID equals qt.QuestionnaireTemplateID into qtJoin
                             from qt in qtJoin.DefaultIfEmpty()
                             join caf in await regionUnitOfWork.custodianAdditionalFieldsEntityRepository.GetAsync() on new {  c.EmployeeMasterId,  c.CustodianId } equals new { EmployeeMasterId = caf.EmployeeMasterID, CustodianId = caf.CustodianID } into cafJoin
                             from caf in cafJoin.DefaultIfEmpty()
                             join ena in await regionUnitOfWork.EntityLHNAdditionalEntityRepository.GetAsync() on en.EntityLegalHoldNoticeID equals ena.EntityLegalHoldNoticeID into enaJoin
                             from ena in enaJoin.DefaultIfEmpty()
                             join eab in await regionUnitOfWork.CaseEntityApprovalBatchEntityRepository.GetAsync() on new { EntityTypeID = @CASECUSTODIANS_ENTITY_ID, EntityID = cc.CaseCustodianId } equals new { eab.EntityTypeID, eab.EntityID } into eabJoin
                             from eab in eabJoin.DefaultIfEmpty()
                       
                             join vab in await regionUnitOfWork.vw_ApprovalBatchEntityRepository.GetAsync() on eab.ApprovalBatchID equals vab.ApprovalBatchID into vabJoin
                             from vab in vabJoin.DefaultIfEmpty()
                             join ack in await regionUnitOfWork.LookupRepository.GetAsync() on en.AcknowledgedType equals ack.LookupId into ackJoin
                             from ack in ackJoin.DefaultIfEmpty()
                                   where clh.CaseLegalHoldID == 164
                                 //clh.CaseID== caseid && clh.CaseLegalHoldID== CaseLegalHoldId
                                 //    cc.IsDeleted == false && c.IsDeleted==true && ( clh.CaseLegalHoldID == 164)
                                 //   && ((c.IsLhn ?? !c.IsLhn) == !c.IsLhn)  &&  ((c.IsPrimary ?? !c.IsPrimary) == !c.IsPrimary)
                                 //   && (L_DepartmentID.Value== 0 || d.DepartmentId == @L_DepartmentID)
                             select new CaseCustodianViewModel
                             {
                                ID = cc.Uuid,
                               
                                 //DpnID = en.UUID,
                                 CaseId = cc.CaseId,
                                 CustodianID = c.Uuid,
                                 CustodianAlias = c.Alias,
                                 FullName = c.FullName,
                                 FirstName = c.FirstName,
                                 LastName = c.LastName,
                                 Email = c.Email,
                                 DepartmentID = d.DepartmentId,
                                 DepartmentName = d.Name,
                                // ClientName = cem.FullName,
                                 EmploymentStartDate = c.EmploymentStartDate,
                                 EmploymentEndDate = c.EmploymentEndDate,
                                 MobileNo = c.MobileNo,
                                 WorkPhoneNo = c.WorkPhoneNo,
                                 //DpnStatusID = ld.Uuid,
                                 Status = l.Status,
                                 StatusName = l.Name,
                                 Priority = cc.Priority,
                                 DPNStatusName = ld.Name,
                                 RequestSentBy = ul.FullName,
                                 RequestSentOn = en.SentOn,
                                 AcknowledgedOn = en.AcknowledgedOn,
                                 //DocumentStreamID = ds.Uuid,
                                 Designation = c.Designation,
                                 CQRCode = en.CQRCode,
                                 //QuestionnaireTemplateID = qt.UUID,
                                 QuestionnaireTemplateName = qt.QuestionnaireTemplateName,
                                 IsUserActive = c.IsUserActive,
                                 CaseLegalHoldID = clh.CaseLegalHoldID,
                                 EmployeeCode = c.EmployeeCode,
                                 MiddleName = c.MiddleName,
                                 OfficeAddressBuildingCode = c.OfficeAddressBuildingCode,
                                 OfficeAddressCampusCode = c.OfficeAddressCampusCode,
                                 OfficeAddressMailingCode = c.OfficeAddressMailingCode,
                                 OfficeAddressZipcode = c.OfficeAddressZipcode,
                                 OfficeAddressCity = c.OfficeAddressCity,
                                 OfficeAddressState = c.OfficeAddressState,
                                 HomeDepartmentCode = c.HomeDepartmentCode,
                                 AccountType = c.AccountType,
                                 HistoricEmployeeID = c.HistoricEmployeeId,
                                 AccountManagerFirstName = c.AccountManagerFirstName,
                                 AccountManagerMiddleName = c.AccountManagerMiddleName,
                                 AccountManagerLastName = c.AccountManagerLastName,
                                 WorkFaxNo = caf.WorkFaxNo,
                                 AlternateEmail = caf.AlternateEmail,
                                 SecondaryEmail = caf.SecondaryEmail,
                                 Column1 = caf.Column1,
                                 Column2 = caf.Column2,
                                 Column3 = caf.Column3,
                                 AcknowledgedType = ack.Name,

                              //   AcknowledgeTypeUUID = ack.Uuid,
                                 ReasonForProxyAcknowledged = ack.LookupId == 8843 ? en.Reason : null,
                                 Reason = ack.LookupId == 8842 || en.LHNStatusID == 8603 || en.LHNStatusID == 76 ? en.Reason : null,
                                 //RowNum = casecustodian.OrderByDescending(cc2 => cc2.CaseCustodianId).Select((cc2, index) => new { cc2, index }).FirstOrDefault(x => x.cc2.CaseCustodianId == cc.CaseCustodianId).index + 1,
                                 ApprovalStatusName = vab.ApprovalStatusName,
                               //  ApprovalStatusUniqueID = vab.ApprovalStatusUniqueID,
                                 ApprovalTypeName = vab.ApprovalTypeName,
                               //  ApprovalTypeUniqueID = vab.ApprovalTypeUniqueID,
                                 ApprovedOn = vab.ApprovedOn,
                                 //ApprovalSentOn = (DateTime)vab.ApprovalSentOn,
                                 ApprovalSentBy = vab.ApprovalSentBy,
                                 ApprovalComments = vab.Comments,
                               //  ApprovalBatchUniqueID = vab.ApprovalBatchUniqueID,
                                 ApprovalBatchName = vab.ApprovalBatchName,
                               //EmployeeUUID = (Guid?)em.Uuid,
                                 CaseCustodianID = cc.CaseCustodianId

                             };
                return result;
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
