using Ligl.LegalManagement.Api.Middleware;
using Ligl.LegalManagement.Model.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Ligl.LegalManagement.Api.Controllers
{

    /// <summary>
    /// Controller for EntityInterviewController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.OData.Routing.Controllers.ODataController" />

    [CustomAuthorization()]
    public class EntityInterviewController(ISender sender, ILogger<CaseLegalHoldController> logger) : ODataController
    {
        private const string ClassName = nameof(EntityInterviewController);
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <returns></returns>
        [EnableQuery]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<InterviewEntityViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAsync()
        {

            const string methodName = $"{ClassName} - {nameof(GetAsync)}";
            try
            {
                logger.LogInformation("Started execution of {MethodName}", methodName);

                var request = new InterviewDetailQuery();
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
        /// Posts the specified InterviewDetailQuery.
        /// </summary>
        /// <param name="caseId"></param>
        /// <param name="InterviewDetailQuery">InterviewDetailQuery.</param>
        /// <returns></returns>
        [HttpPost("v1/[controller]/{caseId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAsync(Guid caseId, [FromBody] InterviewEntityViewModel interviewModel)
        {
            const string methodName = $"{ClassName} - {nameof(PostAsync)}";

            try
            {
                logger.LogInformation(message: "Started execution of {methodName}", methodName);

               // var result = await validator.ValidateAsync(inHouseCreateCommandModels);
                //if (!result.IsValid)
                //{
                //    return BadRequest(result.Errors);
                //}

                var request = new CreateEntityInterviewDetailQuery(caseId, interviewModel);
                var response = await sender.Send(request);
                return Created(response);
            }
            catch (Exception e)
            {
                logger.LogError("Error in {Name} - {Message} /n {Trace}",
                    methodName, e.Message, e.StackTrace);
                return StatusCode(500, e.Message);
            }
            finally
            {
                logger.LogInformation(message: "Completed execution of {methodName}", methodName);
            }
        }


 

    }
}
