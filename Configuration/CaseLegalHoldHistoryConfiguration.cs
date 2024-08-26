using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Ligl.LegalManagement.Repository.Configuration
{

    /// <summary>
    /// Class for CaseLegalHoldHistoryConfiguration
    /// </summary>
    /// <seealso cref="IEntityTypeConfiguration&lt;CaseLegalHoldHistory&gt;" />
    public class CaseLegalHoldHistoryConfiguration : IEntityTypeConfiguration<CaseLegalHoldHistory>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<CaseLegalHoldHistory> builder)
        {
            builder.ToTable("CaseLegalHoldHistory", "vertical");

            builder.Property(e => e.CaseLegalHoldHistoryID).HasColumnName("CaseLegalHoldHistoryID");
            builder.Property(e => e.CaseID).HasColumnName("CaseID");
            builder.Property(e => e.CaseLegalHoldID).HasColumnName("CaseLegalHoldID");
            builder.Property(e => e.CreatedBy).HasMaxLength(50);
            builder.Property(e => e.CreatedOn).HasColumnType("datetime");
            builder.Property(e => e.CustodianQuestionnaireTemplateID).HasColumnName("CustodianQuestionnaireTemplateID");
            builder.Property(e => e.DateRangeID).HasColumnName("DateRangeID");
            builder.Property(e => e.KeywordsID).HasColumnName("KeywordsID");
            builder.Property(e => e.LegalHoldName).HasMaxLength(200);
            builder.Property(e => e.LHNCustodianTemplateID).HasColumnName("LHNCustodianTemplateID");
            builder.Property(e => e.LHNStakeHolderTemplateID).HasColumnName("LHNStakeHolderTemplateID");
            builder.Property(e => e.ModifiedBy).HasMaxLength(50);
            builder.Property(e => e.ModifiedOn).HasColumnType("datetime");
            builder.Property(e => e.StakeHolderQuestionnaireTemplateID).HasColumnName("StakeHolderQuestionnaireTemplateID");

            builder.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}
