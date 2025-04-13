using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Infrastucture.Persistence.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("tblmessage");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Content).HasColumnName("context").HasMaxLength(255);
            builder.Property(e => e.SenderId).HasColumnName("sender_id");
            builder.Property(e => e.ReceiverId).HasColumnName("receiver_id");
            builder.Property(e => e.IsSeen).HasColumnName("is_seen");
            builder
                .Property(e => e.SentAt)
                .HasColumnName("sent_at")
                .HasColumnType("DATETIMEOFFSET");

            builder
                .HasOne(e => e.Sender)
                .WithMany(e => e.SentMessages)
                .HasForeignKey(e => e.SenderId);

            builder
                .HasOne(e => e.Receiver)
                .WithMany(e => e.ReceivedMessages)
                .HasForeignKey(e => e.ReceiverId);
        }
    }
}
