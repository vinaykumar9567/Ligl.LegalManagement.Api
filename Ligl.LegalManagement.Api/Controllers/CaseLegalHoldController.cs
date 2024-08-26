using Ligl.LegalManagement.Api.Middleware;
using Ligl.LegalManagement.Model.Command;
using Ligl.LegalManagement.Model.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
namespace Ligl.LegalManagement.Api.Controllers
{
    /// <summary>
    /// Class for CaseLegalHoldController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.OData.Routing.Controllers.ODataController" />
    [CustomAuthorization()]
    public class CaseLegalHoldController(ISender sender, ILogger<CaseLegalHoldController> logger) : ODataController
    {

        private const string ClassName = nameof(CaseLegalHoldController);

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
         /// <param name="CaseId">The case identifier.</param>     
        /// <returns></returns>
        [EnableQuery]       
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CaseLegalHoldModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAsync(Guid Caseid)
        {
            Caseid = Guid.Parse("FA5E2F24-9EF7-48EA-AC38-315685570E8A");
            const string methodName = $"{ClassName} - {nameof(GetAsync)}";
            try
            {
                logger.LogInformation("Started execution of {MethodName}", methodName);
                var request = new LegalDetailQuery(Caseid);
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
        /// Puts the specified StakeHolderViewModel.
        /// </summary>
        /// <param name="CaseId"></param>
        /// <param name="StakeHolderViewModel">The StakeHolderViewModel.</param>
        /// <returns></returns>
        [EnableQuery]
       
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Put()
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError("Error in {MethodName} - {Message} /n {StackTrace}",
                    "", e.Message, e.StackTrace);
                return StatusCode(500, e.Message);
            }
            finally
            {
                logger.LogInformation("Completed execution of {MethodName}", "");
            }
        }

        /// <summary>
        /// Posts the specified legal group command.
        /// </summary>
        /// <param name="caseId"></param>
        /// <param name="caseLegalHoldModel">The caseLegalHoldModel.</param>
        /// <returns></returns>
        [HttpPost("v1/[controller]/{caseId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAsync(Guid CaseId, [FromBody] CaseLegalHoldModel caseLegalHoldModel)
        {
            const string methodName = $"{ClassName} - {nameof(PostAsync)}";
            try
            {
                logger.LogInformation("Started execution of {MethodName}", methodName);

                var request = new LegalHoldCreateCommand(CaseId, caseLegalHoldModel);
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
