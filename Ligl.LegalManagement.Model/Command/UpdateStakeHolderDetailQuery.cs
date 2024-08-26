using Ligl.LegalManagement.Model.Query;
using MediatR;
namespace Ligl.LegalManagement.Model.Command
{
  
    public record UpdateStakeHolderDetailQuery(Guid Caseid,CaseStakeHolderModel CaseStakeHolderModel) : IRequest<Unit>;
}
