using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Ligl.LegalManagement.Repository.Configuration
{


    /// <summary>
    /// Class for InterviewEntityConfiguration
    /// </summary>
    /// <seealso cref="InterviewEntity" />
    public class InterviewEntityConfiguration : IEntityTypeConfiguration<InterviewEntity>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<InterviewEntity> builder)
        {
            builder
                   .HasKey(e => e.InterviewID);
            builder.ToTable("Interview", "vertical");

            builder.Property(e => e.InterviewID).HasMaxLength(100);
            builder.Property(e => e.UUID).HasMaxLength(100);
            builder.Property(e => e.EntityID).HasMaxLength(100);
            builder.Property(e => e.EntityTypeID).HasMaxLength(100);            
            builder.Property(e => e.Interviewer).HasMaxLength(100);
            builder.Property(e => e.InterviewPlace).HasMaxLength(100);
            builder.Property(e => e.Notes).HasMaxLength(100);          
            builder.Property(e => e.CaseLegalHoldID).HasMaxLength(100);
            builder.Property(e => e.Status).HasMaxLength(100);
            builder.Property(e => e.InterviewDate).HasMaxLength(100);

            builder.HasQueryFilter(e => e.IsDeleted == false);


        }
    }
}
