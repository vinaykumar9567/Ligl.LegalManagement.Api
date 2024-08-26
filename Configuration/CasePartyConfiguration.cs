using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.Configuration
{
    /// <summary>
    /// Class for CasePartyConfiguration
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration&lt;AppConfigDetail&gt;" />
    public class CasePartyConfiguration : IEntityTypeConfiguration<CasePartyEntity>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<CasePartyEntity> builder)
        {
            builder.HasKey(x=>x.CasePartyId);
            builder.ToTable("CaseParty", "vertical");

            builder.Property(e => e.CasePartyId).HasColumnName("CasePartyID");
            builder.Property(e => e.CaseId).HasColumnName("CaseID");
            builder.Property(e => e.CreatedBy).HasMaxLength(50);
            builder.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.ModifiedBy).HasMaxLength(50);
            builder.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.PartyId).HasColumnName("PartyID");
            builder.Property(e => e.Uuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("UUID");

            builder.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}
