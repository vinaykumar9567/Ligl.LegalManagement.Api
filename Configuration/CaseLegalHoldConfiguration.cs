using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ligl.LegalManagement.Repository.Configuration
{

    /// <summary>
    /// Class for CaseLegalHoldConfiguration
    /// </summary>
    /// <seealso cref="LegalCaseDetail" />
    public class CaseLegalHoldConfiguration : IEntityTypeConfiguration<LegalCaseDetail>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<LegalCaseDetail> builder)
        {
            builder
                   .HasKey(x => x.CaseLegalHoldID);
            builder.ToTable("CaseLegalHold", "vertical");

            builder.Property(e => e.CaseLegalHoldID).HasMaxLength(100);
            builder.Property(e => e.CaseID).HasMaxLength(100);           
            builder.Property(e => e.StakeHolderQuestionnaireTemplateID).HasMaxLength(500);
            builder.Property(e => e.NumberOfDays).HasMaxLength(500);
            builder.Property(e => e.CustodianQuestionnaireTemplateID).HasMaxLength(500);
            builder.Property(e => e.DateRangeID).HasMaxLength(500);
            builder.Property(e => e.KeyWordID).HasMaxLength(500);
            builder.Property(e => e.Status).HasMaxLength(500);
            builder.Property(e => e.IsDeleted).HasMaxLength(500);


            builder.HasQueryFilter(e => e.IsDeleted == false);

        }
    }
}
