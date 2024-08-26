using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Ligl.LegalManagement.Repository.Configuration
{

    /// <summary>
    /// Class for QuestionnaireConfiguration
    /// </summary>
    /// <seealso cref="Questionnaire" />
    public class QuestionnaireConfiguration : IEntityTypeConfiguration<Questionnaire>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<Questionnaire> entity)
        {
            entity.ToTable("Questionnaire", "vertical");
            entity.HasKey(x => x.QuestionnaireID).HasName("PK_Questionnaire");
            entity.HasIndex(e => e.IsDeleted, "idx_CaseCustodians_IsDeleted");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.QuestionnaireID).HasMaxLength(50);
            entity.Property(e => e.QuestionnaireText).HasMaxLength(500);
            entity.Property(e => e.RenderTypeID).HasMaxLength(50);
            entity.Property(e => e.IsDeleted).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.ParentID).HasMaxLength(50);
            entity.Property(e => e.Options).HasMaxLength(50);
            entity.Property(e => e.IsMandate).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UUID)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("UUID");




            entity.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}
