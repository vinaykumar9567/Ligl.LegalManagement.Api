using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Ligl.LegalManagement.Repository.Configuration
{

    /// <summary>
    /// Class for CaseAdditionalFieldConfiguration
    /// </summary>
    /// <seealso cref="CaseAdditionalField" />
    public class CaseAdditionalFieldConfiguration : IEntityTypeConfiguration<CaseAdditionalField>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<CaseAdditionalField> builder)
        {
            builder.HasKey(e => e.CaseAdditionalFieldsId);

            builder.ToTable("CaseAdditionalFields", "vertical");

            builder.Property(e => e.CaseAdditionalFieldsId).HasColumnName("CaseAdditionalFieldsID");
            builder.Property(e => e.CaseId).HasColumnName("CaseID");
            builder.Property(e => e.Column1).HasMaxLength(500);
            builder.Property(e => e.Column10).HasMaxLength(500);
            builder.Property(e => e.Column11).HasMaxLength(500);
            builder.Property(e => e.Column12).HasMaxLength(500);
            builder.Property(e => e.Column13).HasMaxLength(500);
            builder.Property(e => e.Column14).HasMaxLength(500);
            builder.Property(e => e.Column15).HasMaxLength(500);
            builder.Property(e => e.Column16).HasMaxLength(500);
            builder.Property(e => e.Column17).HasMaxLength(500);
            builder.Property(e => e.Column18).HasMaxLength(500);
            builder.Property(e => e.Column19).HasMaxLength(500);
            builder.Property(e => e.Column2).HasMaxLength(500);
            builder.Property(e => e.Column20).HasMaxLength(500);
            builder.Property(e => e.Column21).HasMaxLength(500);
            builder.Property(e => e.Column22).HasMaxLength(500);
            builder.Property(e => e.Column23).HasMaxLength(500);
            builder.Property(e => e.Column24).HasMaxLength(500);
            builder.Property(e => e.Column25).HasMaxLength(500);
            builder.Property(e => e.Column26).HasMaxLength(500);
            builder.Property(e => e.Column27).HasMaxLength(500);
            builder.Property(e => e.Column28).HasMaxLength(500);
            builder.Property(e => e.Column29).HasMaxLength(500);
            builder.Property(e => e.Column3).HasMaxLength(500);
            builder.Property(e => e.Column30).HasMaxLength(500);
            builder.Property(e => e.Column31).HasColumnType("datetime");
            builder.Property(e => e.Column32).HasColumnType("datetime");
            builder.Property(e => e.Column33).HasColumnType("datetime");
            builder.Property(e => e.Column34).HasColumnType("datetime");
            builder.Property(e => e.Column35).HasColumnType("datetime");
            builder.Property(e => e.Column4).HasMaxLength(500);
            builder.Property(e => e.Column5).HasMaxLength(500);
            builder.Property(e => e.Column6).HasMaxLength(500);
            builder.Property(e => e.Column7).HasMaxLength(500);
            builder.Property(e => e.Column9).HasMaxLength(500);
            builder.Property(e => e.CreatedBy)
                .HasMaxLength(500)
                .HasDefaultValueSql("(suser_sname())");
            builder.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");
            builder.Property(e => e.ModifiedBy)
                .HasMaxLength(500)
                .HasDefaultValueSql("(suser_sname())");
            builder.Property(e => e.ModifiedOn).HasDefaultValueSql("(getutcdate())");
            builder.Property(e => e.Status).HasDefaultValue(1);
            builder.Property(e => e.Uuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("UUID");

            builder.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}
