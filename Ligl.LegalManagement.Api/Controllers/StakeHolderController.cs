using Ligl.LegalManagement.Api.Middleware;
using Ligl.LegalManagement.Model.Command;
using Ligl.LegalManagement.Model.Query;
using Ligl.LegalManagement.Repository.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Ligl.LegalManagement.Api.Controllers
{
    /// <summary>
    /// Class for StakeHolderController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.OData.Routing.Controllers.ODataController" />

    [CustomAuthorization()]
    public class StakeHolderController(ISender sender, ILogger<StakeHolderController> logger) : ODataController
    {
        private const string ClassName = nameof(StakeHolderController);


        /// <summary>
        /// Gets the attorney asynchronous.
        /// </summary>
        /// <param name="CaseId">The case identifier.</param>
        /// <param name="CaseLegalHoldID">The court identifier.</param>
        /// <returns></returns>
      
        [EnableQuery]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<StakeHoldersViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAsync(Guid caseId,Guid CaseLegalHoldId)
        {
            const string methodName = $"{ClassName} - {nameof(GetAsync)}";

            try
            {
                logger.LogInformation("Started execution of {MethodName}", methodName);
                var request = new StakeHoldersDetailQuery(caseId, CaseLegalHoldId);
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
        /// Puts the specified caseStakeHolderModel.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="caseStakeHolderModel">The caseStakeHolderModel.</param>
        /// <returns></returns>
        [HttpPut("v1/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Put(Guid id, [FromBody] CaseStakeHolderModel caseStakeHolderModel)
        {
            const string methodName = $"{ClassName} - {nameof(Put)}";

            try
            {
                logger.LogInformation($"Started execution of {methodName}");
                var request = new UpdateStakeHolderDetailQuery( id, caseStakeHolderModel);
                var response = await sender.Send(request);
                return Created(response);
            }
            catch (Exception e)
            {
                logger.LogError($"Error in {methodName} - {e.Message}");
                return StatusCode(500, e.Message);
            }
            finally
            {
                logger.LogInformation($"Completed execution of {methodName}");
            }
        }


        /// <summary>
        /// Posts the specified CreateStakeHolderCommand.
        /// </summary>
        /// <param name="caseId"></param>
        /// <param name="CreateStakeHolderCommand">The CreateStakeHolderCommand.</param>
        /// <returns></returns>
        [HttpPost("v1/[controller]/{caseId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAsync(Guid caseId, [FromBody] CaseStakeHolderModel caseStakeHolderModel)
        {
            const string methodName = $"{ClassName} - {nameof(PostAsync)}";

            try
            {
                logger.LogInformation("Started execution of {MethodName}", "");

                var request = new CreateStakeHolderCommand(caseId, caseStakeHolderModel);
                var response = await sender.Send(request);

                return Ok(response);
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


        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="caseId"></param>
        /// <param name="stakeHolderID">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("v1/[controller]/{caseId}/{stakeHolderID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid caseId, Guid stakeHolderID)
        {
            const string methodName = $"{ClassName} - {nameof(DeleteAsync)}";

            try
            {
                logger.LogInformation(message: "Started execution of {methodName}", methodName);

                var request = new DeleteStakeHolderDetailQuery(caseId,stakeHolderID);
                _ = await sender.Send(request);
                return NoContent();
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
