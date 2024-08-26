using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Ligl.LegalManagement.Repository.Configuration
{

    /// <summary>
    /// Class for MatterNotificationTemplateConfiguration
    /// </summary>
    /// <seealso cref="MatterNotificationTemplate" />
    public class MatterNotificationTemplateConfiguration : IEntityTypeConfiguration<MatterNotificationTemplate>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<MatterNotificationTemplate> builder)
        {
            builder.HasKey(x => x.NotificationTemplateID);
                builder.ToTable("NotificationTemplate", "config");

            builder.Property(e => e.NotificationTemplateID).HasMaxLength(100);      
            builder.Property(e => e.EntityTypeID).HasMaxLength(500);
            builder.Property(e => e.EntityID).HasMaxLength(500);
            builder.Property(e => e.UUID).HasMaxLength(500);
            builder.Property(e => e.CategoryID).HasMaxLength(500);
            builder.Property(e => e.Name).HasMaxLength(500);
            builder.Property(e => e.TemplateTypeID).HasMaxLength(500);
            builder.Property(e => e.Status).HasMaxLength(100);
            builder.Property(e => e.IsDeleted).HasMaxLength(500);

            builder.HasQueryFilter(e => e.IsDeleted == false);

        }
    }
}
