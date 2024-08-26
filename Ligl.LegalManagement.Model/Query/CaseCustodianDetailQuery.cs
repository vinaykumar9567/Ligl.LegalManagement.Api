using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ligl.LegalManagement.Model.Query
{

    public record CaseCustodianDetailQuery(Guid CaseId, Guid CaseLegalHoldID) : IRequest<IQueryable<CaseCustodianViewModel>>;

}
