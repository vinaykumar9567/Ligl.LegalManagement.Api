using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Ligl.LegalManagement.Repository.Configuration
{

    /// <summary>
    /// Class for CaseLegalHoldConfiguration
    /// </summary>
    /// <seealso cref="Contact" />
    public class CaseStakeHolderConfiguration : IEntityTypeConfiguration<CaseStakeHolder>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<CaseStakeHolder> builder)
        {
            builder.HasKey(e => e.CaseStakeHolderId).HasName("PK_CaseStakeholders");
            builder.ToTable("CaseStakeHolder", "vertical");

            builder.Property(e => e.CaseStakeHolderId).HasColumnName("CaseStakeHolderID");
            builder.Property(e => e.CaseId).HasColumnName("CaseID");
            builder.Property(e => e.CreatedBy).HasMaxLength(50);
            builder.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.DpnStatusId).HasColumnName("DpnStatusID");
            builder.Property(e => e.ModifiedBy).HasMaxLength(50);
            builder.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.StakeHolderId).HasColumnName("StakeHolderID");
            builder.Property(e => e.Uuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("UUID");

            builder.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}
