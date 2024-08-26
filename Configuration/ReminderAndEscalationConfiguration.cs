using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ligl.LegalManagement.Repository.Domain;
namespace Ligl.LegalManagement.Repository.Configuration
{
    /// <summary>
    /// Class for ReminderAndEscalationConfiguration
    /// </summary>
    /// <seealso cref="ReminderAndEscalation" />
    public class ReminderAndEscalationConfiguration : IEntityTypeConfiguration<ReminderAndEscalation>
    {    
          /// <summary>
         /// Configures the entity of type <typeparamref name="TEntity" />.
         /// </summary>
         /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<ReminderAndEscalation> builder)
        {
            builder
                   .HasKey(e => e.ReminderAndEscalationID);
                   builder.ToTable("ReminderAndEscalation", "config");

            builder.Property(e => e.ReminderAndEscalationID).HasMaxLength(100);
            builder.Property(e => e.UUID).HasMaxLength(500);
            builder.Property(e => e.NotificationTypeID).HasMaxLength(500);
            builder.Property(e => e.ReminderConfig).HasMaxLength(1000);
            builder.Property(e => e.EscalationConfig).HasMaxLength(1000);
            builder.Property(e => e.CaseLegalHoldID).HasMaxLength(500);
            builder.Property(e => e.Status).HasMaxLength(500);
            builder.Property(e => e.IsDeleted).HasMaxLength(500);

            builder.HasQueryFilter(e => e.IsDeleted == false);

        }
    }
}
