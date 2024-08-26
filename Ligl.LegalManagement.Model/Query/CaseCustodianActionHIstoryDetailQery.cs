using MediatR;
namespace Ligl.LegalManagement.Model.Query
{
      public record CaseCustodianActionHIstoryDetailQery : IRequest<IQueryable<CustodiansActionsHistoryViewModel>>;
}
