
using Ligl.LegalManagement.Model.Query.CustomModels;
using MediatR;


namespace Ligl.LegalManagement.Model.Command
{

    public record UpdateCaseLHEscalationDetailQuery(Guid caseid, EscalationConfig escalationConfig) : IRequest<Unit>;

}
