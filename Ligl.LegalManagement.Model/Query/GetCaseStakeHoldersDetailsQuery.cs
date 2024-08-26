using MediatR;
namespace Ligl.LegalManagement.Model.Query
{
    public record GetCaseStakeHoldersDetailsQuery(Guid CaseId) : IRequest<List<CasesMetaDataViewModel>>;
}
