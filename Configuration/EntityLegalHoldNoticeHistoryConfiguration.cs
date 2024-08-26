using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Ligl.LegalManagement.Repository.Configuration
{

    /// <summary>
    /// Class for EntityLegalHoldNoticeHistoryConfiguration
    /// </summary>
    /// <seealso cref="EntityLegalHoldNoticeHistory" />
    public class EntityLegalHoldNoticeHistoryConfiguration : IEntityTypeConfiguration<EntityLegalHoldNoticeHistory>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<EntityLegalHoldNoticeHistory> builder)
        {
            builder
                   .HasKey(e => e.EntityLegalHoldNoticeHistoryID);
            builder.ToTable("EntityLegalHoldNoticeHistory", "vertical");

            builder.Property(e => e.EntityLegalHoldNoticeHistoryID).HasMaxLength(100);
            builder.Property(e => e.UUID).HasMaxLength(100);
            builder.Property(e => e.EntityLegalHoldNoticeHistoryID).HasMaxLength(100);
            builder.Property(e => e.EntityTypeID).HasMaxLength(100);
            builder.Property(e => e.EntityID).HasMaxLength(100);
            builder.Property(e => e.LHNStatusID).HasMaxLength(100);
            builder.Property(e => e.AcknowledgedOn).HasColumnType("datetime");
            builder.Property(e => e.Comments).HasMaxLength(100);
            builder.Property(e => e.SentMailCount).HasMaxLength(100);
            builder.Property(e => e.ReminderCount).HasMaxLength(100);
            builder.Property(e => e.LastReminderSentOn).HasMaxLength(100);
            builder.Property(e => e.LastEscalationSentOn).HasMaxLength(100);
            builder.Property(e => e.CQRCode).HasMaxLength(100);
            builder.Property(e => e.CaseLegalHoldID).HasMaxLength(100);
            builder.Property(e => e.Status).HasMaxLength(100);
            builder.Property(e => e.Reason).HasMaxLength(100);
            builder.Property(e => e.LastResendDate).HasMaxLength(100);
            builder.Property(e => e.AcknowledgedType).HasMaxLength(100);

            builder.HasQueryFilter(e => e.IsDeleted == false);


        }
    }
}
