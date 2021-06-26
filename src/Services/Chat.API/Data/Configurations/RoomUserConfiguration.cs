using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.API.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.API.Data.Configurations
{
    public class RoomUserConfiguration : IEntityTypeConfiguration<RoomUser>
    {
        public void Configure(EntityTypeBuilder<RoomUser> builder)
        {
            builder.HasKey(t => new {t.RoomId, t.UserId});

            builder.HasOne(r => r.Room)
                .WithMany(t => t.RoomUsers)
                .HasForeignKey(r => r.RoomId);


            builder
                .HasOne(r => r.User)
                .WithMany(r => r.RoomUsers)
                .HasForeignKey(r => r.UserId); 
        }
    }
}