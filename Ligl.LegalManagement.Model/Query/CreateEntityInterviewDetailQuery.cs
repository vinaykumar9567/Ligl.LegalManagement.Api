using MediatR;
namespace Ligl.LegalManagement.Model.Query
{
       public record CreateEntityInterviewDetailQuery(Guid CaseId, InterviewEntityViewModel InterviewEntityViewModel) : IRequest <Unit>;
}
    