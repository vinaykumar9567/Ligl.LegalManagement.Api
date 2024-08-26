using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.Configuration
{

    /// <summary>
    /// Class for QuestionnaireTemplateAssociationEntityConfiguration
    /// </summary>
    /// <seealso cref="QuestionnaireTemplateAssociationEntity" />
    public class QuestionnaireTemplateAssociationEntityConfiguration : IEntityTypeConfiguration<QuestionnaireTemplateAssociationEntity>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<QuestionnaireTemplateAssociationEntity> builder)
        {
            builder
                   .HasKey(e => e.QuestionnaireTemplateID);
                  builder .ToTable("QuestionnaireTemplateAssociation", "vertical");

            builder.Property(e => e.QuestionnaireTemplateID).HasMaxLength(100);
            builder.Property(e => e.UUID).HasMaxLength(500);
            builder.Property(e => e.QuestionnaireTemplateAssociationID).HasMaxLength(500);
            builder.Property(e => e.QuestionnaireID).HasMaxLength(500);       
            builder.Property(e => e.Status).HasMaxLength(500);
            builder.Property(e => e.IsDeleted).HasMaxLength(500);
            builder.Property(e => e.DisplayOrder).HasMaxLength(500);

            builder.HasQueryFilter(e => e.IsDeleted == false);

        }
    }
}
