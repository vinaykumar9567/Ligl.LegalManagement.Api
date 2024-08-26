using Ligl.LegalManagement.Model.Command;
using MediatR;
namespace Ligl.LegalManagement.Model.Query
{
   public record CreateStakeHolderCommand(Guid CaseUniqueID, CaseStakeHolderModel caseStakeHolderModel) : IRequest<Unit>;
}
