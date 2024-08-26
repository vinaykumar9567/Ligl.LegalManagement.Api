using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Ligl.LegalManagement.Repository.Configuration
{
    /// <summary>
    /// Class for StakeHolderEntityConfiguration
    /// </summary>
    /// <seealso cref="StakeHolder" />
    public class StakeHolderEntityConfiguration : IEntityTypeConfiguration<StakeHolder>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<StakeHolder> builder)
        {
            builder.HasKey(e => e.StakeHolderID);
            builder.ToTable("StakeHolder", "vertical");
            
            builder.Property(e=>e.StakeHolderID).HasMaxLength(50);
            builder.Property(e => e.UUID).HasMaxLength(100);
            builder.Property(e => e.FirstName).HasMaxLength(50);
            builder.Property(e => e.MiddleName).HasMaxLength(100);
            builder.Property(e => e.LastName).HasMaxLength(500);
            builder.Property(e => e.EmailAddress).HasMaxLength(500);
            builder.Property(e => e.FullName).HasComputedColumnSql().HasMaxLength(500);
            builder.Property(e=>e.IsDeleted).HasMaxLength(10);

            builder.HasQueryFilter(e => e.IsDeleted == false);

        }
    }
}
