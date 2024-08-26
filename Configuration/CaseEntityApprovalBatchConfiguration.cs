using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Ligl.LegalManagement.Repository.Configuration
{  /// <summary>
   /// Class for CaseEntityApprovalBatchConfiguration
   /// </summary>
   /// <seealso cref="CaseEntityApprovalBatchEntity" />
    public class CaseEntityApprovalBatchConfiguration : IEntityTypeConfiguration<CaseEntityApprovalBatchEntity>
    {
  
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<CaseEntityApprovalBatchEntity> builder)
        {
            builder.HasKey(x => x.CaseEntityApprovalBatchID);
            builder.ToTable("CaseEntityApprovalBatch", "vertical");



            builder.Property(e => e.CaseEntityApprovalBatchID);
            builder.Property(e => e.UUID);
            builder.Property(e => e.CaseID);
            builder.Property(e => e.Status).HasMaxLength(50);
            builder .Property(e => e.EntityTypeID);
                     builder.Property(e => e.EntityID);
            builder.Property(e => e.ApprovalBatchID);
            builder.Property(e => e.IsDeleted);
            builder.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}
