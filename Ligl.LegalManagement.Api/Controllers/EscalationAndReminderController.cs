using Ligl.LegalManagement.Model.Query.CustomModels;
using Ligl.LegalManagement.Model.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Ligl.LegalManagement.Model.Command;
using Ligl.LegalManagement.Api.Middleware;

namespace Ligl.LegalManagement.Api.Controllers
{
    /// <summary>
    /// Controller for EscalationAndReminderController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.OData.Routing.Controllers.ODataController" />
    [CustomAuthorization()]
    public class EscalationAndReminderController(ISender sender, ILogger<EscalationAndReminderController> logger) : ODataController
    {
        private const string ClassName = nameof(EscalationAndReminderController);

        /// <summary>
        /// Puts the specified EscalationReminderConfigViewModel.
        /// </summary>
        /// <param name="id"></param>
      
        /// <returns></returns>
        [EnableQuery]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<EscalationReminderConfigViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            const string methodName = $"{ClassName} - {nameof(GetAsync)}";
            try
            {
                logger.LogInformation("Started execution of {MethodName}", methodName);
                var request = new EscalationReminderConfigDetailQuery(id);
                var response = await sender.Send(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                logger.LogError($"Error in {methodName} - {e.Message}", e.StackTrace);
                return StatusCode(500, e.Message);
            }
            finally
            {
                logger.LogInformation("Completed execution of {MethodName}", methodName);
            }
        }





        /// <summary>
        /// Puts the specified EscalationConfig.
        /// </summary>
        /// <param name="CaseId"></param>
        /// <param name="escalationConfig">The escalationConfig.</param>
        /// <returns></returns>
        [EnableQuery]
        [HttpPut("v1/[controller]/{Caseid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Put(Guid CaseId, [FromBody] EscalationConfig escalationConfig)
        {
            const string methodName = $"{ClassName} - {nameof(Put)}";
            try
            {
                logger.LogInformation("Started execution of {MethodName}", methodName);
                var request = new UpdateCaseLHEscalationDetailQuery(CaseId, escalationConfig);
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
