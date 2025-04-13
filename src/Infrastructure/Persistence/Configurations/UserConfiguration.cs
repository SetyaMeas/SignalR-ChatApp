using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Infrastucture.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("tbluser");
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Email).IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Username).HasMaxLength(255).HasColumnName("username");
            builder.Property(e => e.Email).HasMaxLength(255).HasColumnName("email");
            builder.Property(e => e.Password).HasMaxLength(255).HasColumnName("pwd");
            builder
                .Property(e => e.ProfileImage)
                .IsRequired(false)
                .HasMaxLength(255)
                .HasColumnName("pf_image");
            builder.Property(e => e.Salt).HasColumnName("salt").HasColumnType("VARBINARY(MAX)");
            builder
                .Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("DATETIMEOFFSET");

            builder
                .HasMany(e => e.SentMessages)
                .WithOne(e => e.Sender)
                .HasForeignKey(e => e.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(e => e.ReceivedMessages)
                .WithOne(e => e.Receiver)
                .HasForeignKey(e => e.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
