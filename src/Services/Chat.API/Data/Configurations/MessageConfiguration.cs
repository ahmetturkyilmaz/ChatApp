using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Message.API.Entities;


namespace Chat.API.Data.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Entities.Message>
    {
        public void Configure(EntityTypeBuilder<Entities.Message> builder)
        {
            builder.ToTable("Messages");

            builder.Property(s => s.Content).IsRequired().HasMaxLength(500);

            builder.HasOne(s => s.ToRoom)
                .WithMany(m => m.Messages)
                .HasForeignKey(s => s.ToRoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
