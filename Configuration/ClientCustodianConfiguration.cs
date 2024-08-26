using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Ligl.LegalManagement.Repository.Configuration
{
  
    /// <summary>
    /// Class for ClientCustodianConfiguration
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration&lt;AppConfigDetail&gt;" />
    public class ClientCustodianConfiguration : IEntityTypeConfiguration<ClientCustodianEntity>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<ClientCustodianEntity> builder)
        {
            builder.HasKey(x => x.ClientCustodianID);
            builder.ToTable("ClientCustodian", "vertical");

            builder.Property(e => e.ClientCustodianID).HasMaxLength(50);
            builder.Property(e => e.CustodianID).HasColumnName("CustodianID");
            builder.Property(e => e.CreatedBy).HasMaxLength(50);
            builder.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.ModifiedBy).HasMaxLength(50);
            builder.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            builder.Property(e => e.ClientID).HasColumnName("ClientID");
            builder.Property(e => e.UUID)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("UUID");

            builder.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}
