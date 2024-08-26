using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Ligl.LegalManagement.Repository.Configuration
{

    /// <summary>
    /// Class for QuestionnaireResponseConfiguration
    /// </summary>
    /// <seealso cref="QuestionnaireResponse" />
    public class QuestionnaireResponseConfiguration : IEntityTypeConfiguration<QuestionnaireResponse>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<QuestionnaireResponse> entity)
        {
            entity.ToTable("QuestionnaireResponse", "vertical");
            entity.HasKey(x=>x.QuestionnaireResponseID).HasName("PK_QuestionnaireResponse");
             
            entity.Property(e => e.QuestionnaireResponseID).HasMaxLength(50);
            entity.Property(e => e.QuestionnaireID).HasMaxLength(50);
            entity.Property(e => e.ResponseText).HasMaxLength(500);
            entity.Property(e => e.EntityID).HasMaxLength(50);  
            entity.Property(e => e.IsDeleted).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
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



            entity.HasOne(d => d.Questionnaire).WithMany(p => p.QuestionnaireResponses)
                .HasForeignKey(d => d.QuestionnaireID)
                .HasConstraintName("FK_QuestionnaireID");
         
            entity.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}
