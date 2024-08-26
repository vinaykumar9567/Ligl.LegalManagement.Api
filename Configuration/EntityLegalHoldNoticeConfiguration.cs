using Ligl.LegalManagement.Repository.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Ligl.LegalManagement.Repository.Configuration
{

    /// <summary>
    /// Class for EntityLegalHoldNoticeConfiguration
    /// </summary>
    /// <seealso cref="EntityLegalHoldNotice" />

    public class EntityLegalHoldNoticeConfiguration : IEntityTypeConfiguration<EntityLegalHoldNotice>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<EntityLegalHoldNotice> builder)
        {
            builder.ToTable("EntityLegalHoldNotice", "vertical");
            builder.HasKey(x => x.EntityLegalHoldNoticeID);
            builder.Property(e => e.EntityLegalHoldNoticeID).HasMaxLength(100);
            builder.Property(e => e.UUID).HasColumnName("UUID");
            builder.Property(e => e.EntityTypeID).HasMaxLength(500);
            builder.Property(e => e.EntityID).HasMaxLength(500);
            builder.Property(e => e.LHNStatusID).HasMaxLength(500);
            builder.Property(e => e.AcknowledgedOn).HasMaxLength(500);
            builder.Property(e => e.Comments).HasMaxLength(500);
            builder.Property(e => e.SentMailCount).HasMaxLength(500);
            builder.Property(e => e.ReminderCount).HasMaxLength(100);
            builder.Property(e => e.EscalationCount).HasMaxLength(500);
            builder.Property(e => e.LastReminderSentOn).HasMaxLength(500);
            builder.Property(e => e.LastReminderSentOn).HasMaxLength(500);
            builder.Property(e => e.CQRCode).HasMaxLength(500);
            builder.Property(e => e.CaseLegalHoldID).HasMaxLength(500);
            builder.Property(e => e.Status).HasMaxLength(500);
            builder.Property(e => e.Reason).HasMaxLength(500);
            builder.Property(e => e.LastResendDate).HasMaxLength(100);
            builder.Property(e => e.ResendCount).HasMaxLength(500);
            builder.Property(e => e.AcknowledgedType);
            builder.Property(e => e.SentBy).HasMaxLength(500);
            builder.Property(e => e.SentOn).HasDefaultValueSql("(getutcdate())").HasColumnType("datetime");

            builder.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}
