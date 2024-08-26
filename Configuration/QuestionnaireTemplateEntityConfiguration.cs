using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Ligl.LegalManagement.Repository.Configuration
{

    /// <summary>
    /// Class for QuestionnaireTemplateEntityConfiguration
    /// </summary>
    /// <seealso cref="QuestionnaireTemplateEntity" />
    public class QuestionnaireTemplateEntityConfiguration : IEntityTypeConfiguration<QuestionnaireTemplateEntity>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<QuestionnaireTemplateEntity> builder)
        {
            builder
                   .HasKey(e => e.QuestionnaireTemplateID);
                   builder.ToTable("QuestionnaireTemplate", "vertical");

            builder.Property(e => e.QuestionnaireTemplateID).HasMaxLength(100);
            builder.Property(e => e.UUID).HasMaxLength(500);
            builder.Property(e => e.QuestionnaireTemplateName).HasMaxLength(500);
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.QuestionnaireCategoryID).HasMaxLength(500);
            builder.Property(e => e.Status).HasMaxLength(500);
            builder.Property(e => e.IsDeleted).HasMaxLength(500);

            builder.HasQueryFilter(e => e.IsDeleted == false);

        }
    }
}
