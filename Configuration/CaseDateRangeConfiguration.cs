using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ligl.LegalManagement.Repository.Domain;

namespace Ligl.LegalManagement.Repository.Configuration
{

    /// <summary>
    /// Class for CaseDateRangeConfiguration
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration&lt;CaseDateRange&gt;" />
    public class CaseDateRangeConfiguration : IEntityTypeConfiguration<CaseDateRangeEntity>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<CaseDateRangeEntity> entity)
        {
            entity.ToTable("CaseDateRange", "vertical");
            entity.HasKey(e => e.CaseDateRangeID);
            entity.Property(e => e.CaseDateRangeID).HasColumnName("CaseDateRangeID");
            entity.Property(e => e.CaseID).HasColumnName("CaseID");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateRangeID).HasColumnName("DateRangeID");
            entity.Property(e => e.DateRangeName).HasMaxLength(50);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
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
