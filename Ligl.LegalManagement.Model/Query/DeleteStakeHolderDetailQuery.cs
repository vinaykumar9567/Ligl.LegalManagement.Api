using MediatR;
namespace Ligl.LegalManagement.Model.Query
{
public record DeleteStakeHolderDetailQuery(Guid caseid,Guid StakeHolderID) : IRequest<Unit>;
}
