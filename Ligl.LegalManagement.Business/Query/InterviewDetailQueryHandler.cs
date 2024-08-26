using Ligl.LegalManagement.Model.Query;
using Ligl.LegalManagement.Repository.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EntityTypes = Ligl.LegalManagement.Model.Query.Constants.EntityTypes;
namespace Ligl.LegalManagement.Business.Query
{
    /// <summary>
    /// Class for InterviewDetailQueryHandler
    /// </summary>
    /// <seealso cref="InterviewDetailQuery" />
    public class InterviewDetailQueryHandler(
        IRegionUnitOfWork regionUnitOfWork,
        ILogger<InterviewDetailQueryHandler> logger) : IRequestHandler<InterviewDetailQuery, IQueryable<InterviewEntityViewModel>>
    {
        private const string ClassName = nameof(InterviewDetailQueryHandler);

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="AccessViolationException">Invalid user exception</exception>
        public async Task<IQueryable<InterviewEntityViewModel>> Handle(InterviewDetailQuery request,
            CancellationToken cancellationToken)
        {
            const string methodName = $"{ClassName} - {nameof(Handle)}";
            try
            {
                var interviews = await regionUnitOfWork.InterviewEntityRepository.GetAsync();
                var users = await regionUnitOfWork.UserLoginRepository.GetAsync();
                var caseCustodians = await regionUnitOfWork.CaseCustodianRepository.GetAsync();
                var caseStakeHolders = await regionUnitOfWork.caseStakeHolderEntity.GetAsync();
                var entities = await regionUnitOfWork.EntityRepository.GetAsync();
                var legalHoldDetails = await regionUnitOfWork.CaseLegalHoldDetailRepository.GetAsync();
                var documents = await regionUnitOfWork.DocumentStreamRepository.GetAsync();

                // Process the data
                var result = from interview in interviews
                             join user in users on interview.ModifiedBy equals user.Uuid.ToString()
                             join casecustodian in caseCustodians
                                 on new { interview.EntityID, interview.EntityTypeID } equals new { EntityID = casecustodian.CaseCustodianId, EntityTypeID = (int)EntityTypes.CaseCustodian }
                                 into tempcasecustodian
                             from outercasecustodian in tempcasecustodian.DefaultIfEmpty()
                             join casestakeholder in caseStakeHolders
                                 on new { interview.EntityID, interview.EntityTypeID } equals new { EntityID = casestakeholder.StakeHolderId, EntityTypeID = (int)EntityTypes.CaseStakeHolder }
                                 into tempcasestakeholder
                             from outercasestakeholder in tempcasestakeholder.DefaultIfEmpty()
                             join entity in entities on interview.EntityTypeID equals entity.EntityId
                             join lhn in legalHoldDetails on interview.CaseLegalHoldID equals lhn.CaseLegalHoldID
                             select new
                             {
                                 EntityUniqueID = outercasecustodian != null ? outercasecustodian.Uuid : outercasestakeholder.Uuid,
                                 interview.EntityTypeID,
                                 interview.EntityID,
                                 interview.CaseLegalHoldID,
                                 Interview = interview,
                                 lastEditedBy = user.FullName,
                                entity.Uuid,
                                lhn.UUID
                             };

                // Perform the document join and projection
               var interviewList = await result.ToListAsync(); // Fetch the intermediate result

                var interviewWithDocuments = from interview in interviewList
                                             join document in documents
                                                 on new { DocumentEntityID = interview.Interview.InterviewID, DocumentEntityTypeID = (int)EntityTypes.Interview }
                                                 equals new { DocumentEntityID = document.EntityId, DocumentEntityTypeID = document.EntityTypeId }
                                                 into tempdocument
                                             from outerdoc in tempdocument.DefaultIfEmpty()
                                             select new InterviewEntityViewModel
                                             {
                                                 ID = interview.Interview.UUID,
                                                 Interviewer = interview.Interview.Interviewer! ,
                                                 InterviewDate = interview.Interview.InterviewDate,
                                                 InterviewPlace = interview.Interview.InterviewPlace,
                                                 Notes = interview.Interview.Notes,
                                                 EntityTypeUniqueID = interview.Interview.UUID,
                                                 EntityUniqueID = interview.EntityUniqueID,
                                                 CaseLegalHoldUniqueID = interview.Interview.UUID,
                                                 ModifiedBy = interview.lastEditedBy,
                                                 Documents = tempdocument.Select(doc => new DocumentStreamModel
                                                 {
                                                     ID = doc.Uuid,
                                                     Name = doc.Name,
                                                     Extension = doc.Extension,
                                                     Comments = doc.Comments,
                                                     FileSize = doc.FileSize,
                                                     FileData = doc.FileData
                                                 }).ToList()
                                             };

           
                return interviewWithDocuments.ToList().AsQueryable(); 
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
