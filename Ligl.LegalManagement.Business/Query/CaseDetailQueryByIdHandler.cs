using Ligl.LegalManagement.Model.Query;
using Ligl.LegalManagement.Repository.Interface;
using Ligl.Core.Sdk.Shared.Model.Principal;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ligl.LegalManagement.Business.Query
{

    /// <summary>
    /// Class for CaseDetailQueryByIdHandler
    /// </summary>
    /// <seealso cref="CaseDetailQuery" />
    public class CaseDetailQueryByIdHandler(IRegionUnitOfWork regionUnitOfWork, ILogger<CaseDetailQueryByIdHandler> logger, IHttpContextAccessor contextAccessor,
        IPublisher publisher) : IRequestHandler<CaseDetailQueryById, CaseDetailViewModel>
    {
        private const string ClassName = nameof(CaseDetailQueryByIdHandler);

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="AccessViolationException">Invalid user exception</exception>
        public async Task<CaseDetailViewModel> Handle(CaseDetailQueryById request, CancellationToken cancellationToken)
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
                var caseDetails = await regionUnitOfWork.ViewCaseDetailRepository.GetByIdAsync(request.CaseId);

                return caseDetails == null
                    ? throw new Exception("Case not found")
                : new CaseDetailViewModel
                {
                    Name = caseDetails.Name,
                    CaseApprovalStatus = caseDetails.CaseApprovalStatus,
                    CaseId = caseDetails.CaseId,
                    ClientId = caseDetails.ClientId,
                    ClientName = caseDetails.ClientName,
                    Column1 = caseDetails.Column1,
                    Column2 = caseDetails.Column2,
                    Column3 = caseDetails.Column3,
                    Column4 = caseDetails.Column4,
                    Column5 = caseDetails.Column5,
                    Column6 = caseDetails.Column6,
                    Column7 = caseDetails.Column7,
                    Column8 = caseDetails.Column8,
                    Column9 = caseDetails.Column9,
                    Column10 = caseDetails.Column10,
                    Column11 = caseDetails.Column11,
                    Column12 = caseDetails.Column12,
                    Column13 = caseDetails.Column13,
                    Column14 = caseDetails.Column14,
                    Column15 = caseDetails.Column15,
                    Column16 = caseDetails.Column16,
                    Column17 = caseDetails.Column17,
                    Column18 = caseDetails.Column18,
                    Column19 = caseDetails.Column19,
                    Column20 = caseDetails.Column20,
                    Column21 = caseDetails.Column21,
                    Column22 = caseDetails.Column22,
                    Column23 = caseDetails.Column23,
                    Column24 = caseDetails.Column24,
                    Column25 = caseDetails.Column25,
                    Column26 = caseDetails.Column26,
                    Column27 = caseDetails.Column27,
                    Column28 = caseDetails.Column28,
                    Column29 = caseDetails.Column29,
                    Column30 = caseDetails.Column30,
                    Column31 = caseDetails.Column31,
                    Column32 = caseDetails.Column32,
                    Column33 = caseDetails.Column33,
                    Column34 = caseDetails.Column34,
                    Column35 = caseDetails.Column35,
                    Column36 = caseDetails.Column36,
                    Column37 = caseDetails.Column37,
                    Column38 = caseDetails.Column38,
                    Column39 = caseDetails.Column39,
                    Column40 = caseDetails.Column40,
                    CompanyName = caseDetails.CompanyName,
                    CompanyUniqueId = caseDetails.CompanyUniqueId,
                    CreatedBy = caseDetails.CreatedBy,
                    CreatedOn = caseDetails.CreatedOn,
                    CustodianCount = caseDetails.CustodianCount,
                    Description = caseDetails.Description,
                    Discount = caseDetails.Dscount,
                    DocketNumber = caseDetails.DocketNumber,
                    MatterId = caseDetails.MatterId,
                    MatterName = caseDetails.MatterName,
                    MatterPartyTypeId = caseDetails.MatterPartyTypeId,
                    MatterPartyTypeName = caseDetails.MatterPartyTypeName,
                    ModifiedBy = caseDetails.ModifiedBy,
                    ModifiedOn = caseDetails.ModifiedOn,
                    Organization = caseDetails.Organization,
                    OrganizationId = caseDetails.OrganizationId,
                    Prefix = caseDetails.Prefix,
                    RoleTypeId = caseDetails.RoleTypeId,
                    RoleTypeName = caseDetails.RoleTypeName,
                    RowNum = caseDetails.RowNum,
                    Severity = caseDetails.Severity,
                    SeverityId = caseDetails.SeverityId,
                    Status = caseDetails.Status,
                    StatusName = caseDetails.StatusName,
                    SubOrganization = caseDetails.SubOrganization,
                    SubOrganizationId = caseDetails.SubOrganizationId,
                    SubmissionDate = caseDetails.SubmissionDate
                };
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
