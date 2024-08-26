using MediatR;
namespace Ligl.LegalManagement.Model.Query
{
    public record LegalCaseDetailQueryBYId(Guid CaseID) : IRequest<IQueryable<CaseLegalHoldModel>>;
    public record LegalDetailQuery(Guid CaseID) : IRequest<List<CaseLegalHoldModel>>;
    public record LegalHoldCreateCommand(Guid CaseID,  CaseLegalHoldModel caseLegalHoldModel) :  IRequest<Unit>;


}
