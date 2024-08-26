using Ligl.LegalManagement.Api.Middleware;
using Ligl.LegalManagement.Model.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
namespace Ligl.LegalManagement.Api.Controllers
{
    /// <summary>
    /// Class for CaseCustodianController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.OData.Routing.Controllers.ODataController" />
    [CustomAuthorization()]
    public class CaseCustodianController(ISender sender, ILogger<CaseLegalHoldController> logger) : ODataController
    {

        private const string ClassName = nameof(CaseCustodianController);

        /// <summary>
        /// Gets the  asynchronous.
        /// </summary>
        /// <param name="CaseId">The case identifier.</param>
        /// <param name="CaseLegalHoldID">The CaseLegalHoldID identifier.</param>
        /// <returns></returns>
        [EnableQuery] 
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<CaseCustodianViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAsync(Guid CaseId, Guid CaseLegalHoldID)
        {

            const string methodName = $"{ClassName} - {nameof(GetAsync)}";
            try
            {
                logger.LogInformation("Started execution of {MethodName}", methodName);

                var request = new CaseCustodianDetailQuery(CaseId, CaseLegalHoldID);
                var response = await sender.Send(request); 
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError("Error in {MethodName} - {Message} /n {StackTrace}",
                    methodName, e.Message, e.StackTrace);
                return StatusCode(500, e.Message);
            }
            finally
            {
                logger.LogInformation("Completed execution of {MethodName}", methodName);
            }
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="CaseId">The case identifier.</param>
        /// <returns></returns>
        [EnableQuery]
        [HttpGet("v1/[controller]/CustodianActions/{caseId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<CustodiansActionsHistoryViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetByCaseIdAsync(Guid caseId)
        {
            const string methodName = $"{ClassName} - {nameof(GetByCaseIdAsync)}";

            try
            {
                logger.LogInformation("Started execution of {MethodName}", methodName);
                var request = new CaseCustodianActionHIstoryDetailQery();
                var response = await sender.Send(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError("Error in {MethodName} - {Message} /n {StackTrace}",
                    methodName, e.Message, e.StackTrace);
                return StatusCode(500, e.Message);
            }
            finally
            {
                logger.LogInformation("Completed execution of {MethodName}", methodName);
            }
        }
    }
}
