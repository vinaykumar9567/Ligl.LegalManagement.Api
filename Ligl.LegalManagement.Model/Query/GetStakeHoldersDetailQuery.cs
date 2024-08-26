using MediatR;
namespace Ligl.LegalManagement.Model.Query
{
 
    public record GetStakeHoldersDetailQuery(Guid CaseId,CaseStakeHolderModel CaseStakeHolderModel) : IRequest<List<StakeHoldersViewModel>>;
    public record StakeHoldersDetailQuery(Guid Caseid, Guid CaseLegalHoldId) : IRequest<List<StakeHoldersViewModel>>;

}
