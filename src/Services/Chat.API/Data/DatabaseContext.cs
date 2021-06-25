﻿using Chat.API.Data.Configurations;
using Chat.API.Entities;
using Microsoft.EntityFrameworkCore;


namespace Chat.API.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entities.Message> Messages { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}