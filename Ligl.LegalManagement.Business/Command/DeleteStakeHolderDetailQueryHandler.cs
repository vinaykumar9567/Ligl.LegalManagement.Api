using Ligl.Core.Sdk.Shared.Business;
using Ligl.LegalManagement.Model.Query;
using Ligl.LegalManagement.Model.Query.Constants;
using Ligl.LegalManagement.Model.Query.CustomModels;
using Ligl.LegalManagement.Repository.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;



namespace Ligl.LegalManagement.Business.Command
{
    /// <summary>
    /// Class for DeleteStakeHolderDetailQueryHandler
    /// </summary>
    /// <seealso cref="DeleteStakeHolderDetailQuery" />
    public class DeleteStakeHolderDetailQueryHandler(IRegionUnitOfWork regionUnitOfWork, IUserContextBusiness userContextBusiness, ILogger<DeleteStakeHolderDetailQueryHandler> logger) : IRequestHandler<DeleteStakeHolderDetailQuery, Unit>
    {
        private const string ClassName = nameof(DeleteStakeHolderDetailQueryHandler);

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="AccessViolationException">Invalid user exception</exception>
        public async Task<Unit> Handle(DeleteStakeHolderDetailQuery request, CancellationToken cancellationToken)
        {
            const string methodName = $"{ClassName} - {nameof(Handle)}";
            try
            {
                logger.LogInformation(message: "Started execution of {methodName}", methodName);
        
                if (request?.StakeHolderID == null || request.StakeHolderID == Guid.Empty)
                {
                    logger.LogError("Error in {methodName} - invalid user StakeHolderId", methodName);
                    throw new AccessViolationException("Invalid user StakeHolderId");
                }
             var stakeholdupdate = (await regionUnitOfWork.stakeHolderEntity.GetAsync()).FirstOrDefault(stakeholder => stakeholder.UUID == request.StakeHolderID && !stakeholder.IsDeleted.Value);
              
                if (stakeholdupdate == null)
                    throw new CustomError(CaseErrorCodes.StakeholderNotFound,
                        BaseErrorProvider.GetErrorString<CaseErrorCodes>(CaseErrorCodes.StakeholderNotFound),
                        $"{ClassName} - {nameof(Handle)}");
                var user = userContextBusiness.GetContext;
                stakeholdupdate.ModifiedOn = DateTime.UtcNow;
                stakeholdupdate.ModifiedBy = user.userIdString;
                stakeholdupdate.IsDeleted = true;
              
                regionUnitOfWork.stakeHolderEntity.Update(stakeholdupdate);            
                regionUnitOfWork.Save();
               return Unit.Value;
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
