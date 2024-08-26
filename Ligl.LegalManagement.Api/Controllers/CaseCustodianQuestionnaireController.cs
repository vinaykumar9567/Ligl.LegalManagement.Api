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
    public class CaseCustodianQuestionnaireController(ISender sender, ILogger<CaseLegalHold> logger) : ODataController
    {
        private const string ClassName = nameof(CaseCustodianQuestionnaireController);

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <returns></returns>
        [EnableQuery]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<QuestionnaireTemplateViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAsync()
        {
            const string methodName = $"{ClassName} - {nameof(GetAsync)}";
            try
            {
                logger.LogInformation("Started execution of {MethodName}", methodName);
                var request = new QuestionnaireTemplateDetailQuery();
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
