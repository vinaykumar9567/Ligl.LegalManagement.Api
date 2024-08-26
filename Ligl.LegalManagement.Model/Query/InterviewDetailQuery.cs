using MediatR;


namespace Ligl.LegalManagement.Model.Query
{
    public record InterviewDetailQuery : IRequest<IQueryable<InterviewEntityViewModel>>;
}
