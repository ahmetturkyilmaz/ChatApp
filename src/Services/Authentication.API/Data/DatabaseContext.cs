using Microsoft.EntityFrameworkCore;

using Authentication.API.Entities;
using BC = BCrypt.Net.BCrypt;

namespace Authentication.API.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }



        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
                {
                    Id = 1,
                    Name = "Ahmet",
                    Surname = "Turkyilmaz",
                    Email = "ahmet@gmail.com",
                    PasswordHash = BC.HashPassword("birseyler")
                },
                new User
                {
                    Id = 1,
                    Name = "Cagrı",
                    Surname = "Ceylan",
                    Email = "cagrı@gmail.com",
                    PasswordHash = BC.HashPassword("ikiseyler")
                }
            );
        }
    }
}