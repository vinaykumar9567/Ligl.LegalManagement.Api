
using Ligl.LegalManagement.Model.Query.CustomModels;
using MediatR;

namespace Ligl.LegalManagement.Model.Query
{
     public record EmailTemplatesQueryDetails() : IRequest<IQueryable<NotificationTemplateViewModel>>;
}
