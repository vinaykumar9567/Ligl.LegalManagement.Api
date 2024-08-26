using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Ligl.LegalManagement.Repository.Configuration
{

    /// <summary>
    /// Class for CaseCustodianConfiguration
    /// </summary>
    /// <seealso cref="CaseCustodian" />
    public class CaseCustodianConfiguration : IEntityTypeConfiguration<CaseCustodian>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<CaseCustodian> entity)
        {
            entity.ToTable("CaseCustodians", "vertical");

            entity.HasIndex(e => new { e.CaseId, e.CustodianId, e.IsDeleted }, "_idx_case_custodians_idcusiddel");

            entity.HasIndex(e => e.IsDeleted, "idx_CaseCustodians_IsDeleted");

            entity.Property(e => e.CaseCustodianId).HasColumnName("CaseCustodianID");
            entity.Property(e => e.CaseCustodianUniqueId).HasColumnName("CaseCustodianUniqueID");
            entity.Property(e => e.CaseId).HasColumnName("CaseID");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustodianId).HasColumnName("CustodianID");
            entity.Property(e => e.DpnStatusId).HasColumnName("DpnStatusID");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Uuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("UUID");

            entity.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}
