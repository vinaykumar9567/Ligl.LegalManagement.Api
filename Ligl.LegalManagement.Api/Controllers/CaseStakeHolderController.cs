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
    /// Class for CaseStakeHolderController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.OData.Routing.Controllers.ODataController" />
    [CustomAuthorization()]
    public class CaseStakeHolderController(ISender sender, ILogger<CaseLegalHold> logger) : ODataController
    {
        private const string ClassName = nameof(CaseStakeHolderController);
        /// <summary>
        /// Gets the specified row identifier.
        /// </summary>
        /// <param name="StakeHolderID">The row identifier.</param>
        /// <returns></returns>
        [EnableQuery]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<CasesMetaDataViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAsync(Guid StakeHolderID)
        {
            const string methodName = $"{ClassName} - {nameof(GetAsync)}";
            try
            {
                logger.LogInformation("Started execution of {MethodName}", methodName);
                var request = new GetCaseStakeHoldersDetailsQuery(StakeHolderID);
               
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
        /// Gets the specified row identifier.
        /// </summary>
        /// <param name="id">The row identifier.</param>
        /// <returns></returns>
        [EnableQuery]
        [HttpGet("v1/[controller]/StakeHolders/")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<StakeHoldersViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetStakeHolders(Guid CaseId, CaseStakeHolderModel CaseStakeHolderModel)
        {


            const string methodName = $"{ClassName} - {nameof(GetStakeHolders)}";

            try
            {
                logger.LogInformation("Started execution of {MethodName}", methodName);

                var request = new GetStakeHoldersDetailQuery(CaseId, CaseStakeHolderModel);
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
        /// <returns></returns>
        [EnableQuery]
        [HttpPut("v1/[controller]/{CaseId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<StakeHoldersViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> UpdateStakeHolder(Guid CaseId, [FromBody] CaseStakeHolderModel caseStakeHolderModel)
        {
            const string methodName = $"{ClassName} - {nameof(GetStakeHolders)}";
            try
            {
                logger.LogInformation("Started execution of {MethodName}", methodName);
                var request = new UpdateStakeHolderDetailQuery(CaseId, caseStakeHolderModel);
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


        ///// <summary>
        ///// Deletes the specified identifier.
        ///// </summary>
        ///// <param name="id">The identifier.</param>
        ///// <returns></returns>
        //[HttpDelete("v1/[controller]/{caseid}/{StakeHolderID}")]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> DeleteStakeHolder(Guid StakeHolderID)
        //{
        //    const string methodName = $"{ClassName} - {nameof(DeleteStakeHolder)}";

        //    try
        //    {
        //        logger.LogInformation($"Started execution of {methodName}");
        //        var request = new DeleteStakeHolderDetailQuery(StakeHolderID);
        //        var response = await sender.Send(request);
        //        return NoContent();
        //    }
        //    catch (Exception e)
        //    {
        //        logger.LogError($"Error in {methodName} - {e.Message}");
        //        return StatusCode(500, e.Message);
        //    }
        //    finally
        //    {
        //        logger.LogInformation($"Completed execution of {methodName}");
        //    }
        //}


    }
}
