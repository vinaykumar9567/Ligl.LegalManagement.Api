using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Ligl.LegalManagement.Repository.Configuration
{
     /// <summary>
    /// Class for EntityLHNAdditionalConfiguration
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration&lt;EntityLHNAdditionalEntity&gt;" />
    public class EntityLHNAdditionalConfiguration : IEntityTypeConfiguration<EntityLHNAdditionalEntity>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<EntityLHNAdditionalEntity> builder)
        {
            builder.HasKey(x => x.EntityLHNAdditionalID);
            builder.ToTable("EntityLHNAdditional", "vertical");

            builder.Property(e => e.EntityLHNAdditionalID).HasMaxLength(50);
            builder.Property(e => e.EntityLegalHoldNoticeID);
            builder.Property(e => e.QuestionnaireBy).HasMaxLength(50);
            builder.Property(e => e.CreatedBy).HasMaxLength(50);
            builder.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.ModifiedBy).HasMaxLength(50);
            builder.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.AcknowledgedBy).HasColumnName("AcknowledgedBy");
            builder.Property(e => e.AcknowledgementUrl).HasColumnName("AcknowledgementUrl");
            builder.Property(e => e.UUID)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("UUID");

            builder.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}
