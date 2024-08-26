using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Ligl.LegalManagement.Repository.Configuration
{

    /// <summary>
    /// Class for CustodianAdditionalFieldsConfiguration
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration&lt;AppConfigDetail&gt;" />
    public class CustodianAdditionalFieldsConfiguration : IEntityTypeConfiguration<CustodianAdditionalFieldsEntity>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<CustodianAdditionalFieldsEntity> builder)
        {
            builder.HasKey(x => x.CustodianAdditionalFieldsID);
            builder.ToTable("CustodianAdditionalFields", "vertical");

            builder.Property(e => e.CustodianAdditionalFieldsID).HasColumnName("CustodianAdditionalFieldsID");
            builder.Property(e => e.CustodianID).HasMaxLength(50);
            builder.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.ModifiedBy).HasMaxLength(50);
            builder.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.WorkFaxNo).HasColumnName("WorkFaxNo");
            builder.Property(e => e.SecondaryEmail).HasColumnName("SecondaryEmail");
            builder.Property(e => e.ExtendedAttributes).HasMaxLength(50);
            builder.Property(e => e.EntityID).HasMaxLength(50);
            builder.Property(e => e.Location).HasMaxLength(50);






            builder.Property(e => e.UUID)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("UUID");

            builder.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}
