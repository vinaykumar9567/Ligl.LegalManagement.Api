using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Ligl.LegalManagement.Repository.Configuration
{

    /// <summary>
    /// Class for CaseDateRangeConfiguration
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration&lt;NotificationTemplateContentEntity&gt;" />
    public class NotificationTemplateContentConfiguration : IEntityTypeConfiguration<NotificationTemplateContentEntity>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<NotificationTemplateContentEntity> entity)
        {
            entity.ToTable("NotificationTemplateContent", "config");
            entity.HasKey(x=>x.NotificationTemplateContentID);
            entity.Property(e => e.NotificationTemplateContentID).HasColumnName("NotificationTemplateContentID");
            entity.Property(e => e.Subject).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Content).HasColumnName("Content");
            entity.Property(e => e.NotificationTemplateID).HasMaxLength(50);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.LanguageID).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue(1);
            entity.Property(e => e.UUID)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("UUID");

            entity.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}
