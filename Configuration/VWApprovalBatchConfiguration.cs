using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Ligl.LegalManagement.Repository.Configuration
{
    /// <summary>
    /// Class for CaseCustodianConfiguration
    /// </summary>
    /// <seealso cref="VWApprovalBatchConfiguration" />
    public class VWApprovalBatchConfiguration : IEntityTypeConfiguration<vw_ApprovalBatchEntity>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<vw_ApprovalBatchEntity> entity)
        {
            entity.ToView("vw_ApprovalBatch", "vertical");
            entity.HasNoKey();

            entity.Property(e => e.ApprovalBatchID).HasMaxLength(500);
            entity.Property(e => e.ApprovalBatchUniqueID).HasMaxLength(500);
            entity.Property(e => e.ApprovalBatchName).HasMaxLength(500);
            entity.Property(e => e.ApprovalStatusID).HasMaxLength(500);
            entity.Property(e => e.ApprovalStatusUniqueID).HasMaxLength(500);
            entity.Property(e => e.ApprovalTypeID).HasMaxLength(500);
            entity.Property(e => e.ApprovalTypeName).HasMaxLength(500);
            entity.Property(e => e.ApprovalTypeUniqueID).HasMaxLength(500);
            entity.Property(e => e.EmailTemplateID).HasMaxLength(500);
            entity.Property(e => e.EmailTemplateUniqueID).HasMaxLength(520);
    
            entity.Property(e => e.ApprovedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmailTemplateName);
            entity.Property(e => e.Comments).HasMaxLength(500);
            entity.Property(e => e.ApprovalSentBy).HasMaxLength(50);
            entity.Property(e => e.ApprovalSentOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
       

           
        }
    }
}
