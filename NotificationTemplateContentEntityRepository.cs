using Ligl.Core.Sdk.Shared.Repository;
using Ligl.LegalManagement.Repository.Domain;
using Ligl.LegalManagement.Repository.DataAccess.Interface;
namespace Ligl.LegalManagement.Repository.DataAccess
{

    /// <summary>
    /// Class for NotificationTemplateContentEntityRepository
    /// </summary>
    /// <seealso cref="NotificationTemplateContentEntity    
    /// Ligl.LegalManagement.Repository.NotificationTemplateContentEntity&gt;" />
    /// <seealso cref="Ligl.LegalManagement.Repository.DataAccess.Interface.INotificationTemplateContentEntityRepository" />

    public class NotificationTemplateContentEntityRepository(RegionContext context)
        : GenericRepository<NotificationTemplateContentEntity, RegionContext>(context), INotificationTemplateContentEntityRepository
    {
        private readonly RegionContext _regionContext = context;

    }
}
