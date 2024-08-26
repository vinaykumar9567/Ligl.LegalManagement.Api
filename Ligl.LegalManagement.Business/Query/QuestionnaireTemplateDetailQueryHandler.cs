using Ligl.LegalManagement.Model.Query;
using Ligl.LegalManagement.Repository.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ligl.LegalManagement.Business.Query
{

    /// <summary>
    /// Class for QuestionnaireTemplateDetailQueryHandler
    /// </summary>
    /// <seealso cref="QuestionnaireTemplateDetailQuery" />
    public class QuestionnaireTemplateDetailQueryHandler(
        IRegionUnitOfWork regionUnitOfWork,
        ILogger<QuestionnaireTemplateDetailQueryHandler> logger
       ) : IRequestHandler<QuestionnaireTemplateDetailQuery, IQueryable<QuestionnaireTemplateViewModel>>
    {
        private const string ClassName = nameof(QuestionnaireTemplateDetailQueryHandler);

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        /// <exception cref="AccessViolationException">Invalid user exception</exception>
        public async Task<IQueryable<QuestionnaireTemplateViewModel>> Handle(QuestionnaireTemplateDetailQuery request,
            CancellationToken cancellationToken)
        {
            const string methodName = $"{ClassName} - {nameof(Handle)}";
            try
            {
                logger.LogInformation(message: "Started execution of {methodName}", methodName);
                var questionnaireTemplateAssociationEntity = await regionUnitOfWork.questionnaireTemplateAssociationEntityRepository.GetAsync();

                var result = from questionTemplate in await regionUnitOfWork.QuestionnaireTemplateEntityRespository.GetAsync()
                             join lookUp in await regionUnitOfWork.LookupRepository.GetAsync() on questionTemplate.QuestionnaireCategoryID equals
                                 lookUp.LookupId
                             where !questionTemplate.IsDeleted && !lookUp.IsDeleted
                             select new QuestionnaireTemplateViewModel
                             {
                                 ID = questionTemplate.UUID,
                                 QuestionnaireTemplateName = questionTemplate.QuestionnaireTemplateName! ,
                                 QuestionnaireCategoryUniqueID =lookUp.Uuid,
                                 Description = questionTemplate.Description!,
                                 HasQuestionnaires = questionnaireTemplateAssociationEntity.Any(x => x.QuestionnaireTemplateID == questionTemplate.QuestionnaireTemplateID && !x.IsDeleted)
                             };
                return result;
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
