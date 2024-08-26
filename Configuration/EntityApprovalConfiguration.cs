using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ligl.LegalManagement.Repository.Configuration
{


    /// <summary>
    /// Class for CaseLegalHoldConfiguration
    /// </summary>
    /// <seealso cref="Contact" />
    public class EntityApprovalConfiguration : IEntityTypeConfiguration<EntityApproval>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<EntityApproval> builder)
        {
            builder
                   .HasKey(d => d.EntityApprovalID);

            builder.ToTable("EntityApproval", "vertical");

            builder.Property(e => e.EntityApprovalID).HasMaxLength(100);
            builder.Property(e => e.UUID).HasMaxLength(500);
            builder.Property(e => e.EntityID).HasMaxLength(500);
            builder.Property(e => e.EntityTypeID).HasMaxLength(500);
            builder.Property(e => e.ApprovalStatusID).HasMaxLength(500);
            builder.Property(e => e.ApprovedOn).HasMaxLength(500);
            builder.Property(e => e.EmailTemplateID).HasMaxLength(500);
            builder.Property(e => e.ApprovalUserID).HasMaxLength(100);
            builder.Property(e => e.Comments).HasMaxLength(500);
            builder.Property(e => e.Status).HasMaxLength(100);
            builder.Property(e => e.IsDeleted).HasMaxLength(500);

            builder.HasQueryFilter(e => e.IsDeleted == false);


        }
    }
}
