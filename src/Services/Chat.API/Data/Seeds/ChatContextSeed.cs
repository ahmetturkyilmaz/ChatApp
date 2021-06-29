using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.API.Entities;

namespace Chat.API.Data.Seeds
{
    public class ChatContextSeed
    {
        public static async Task SeedAsync(DatabaseContext databaseContext)
        {
            if (!databaseContext.User.Any())
            {
                databaseContext.User.AddRange(GetPreconfiguredUsers());
                await databaseContext.SaveChangesAsync();
            }

            if (!databaseContext.Rooms.Any())
            {
                databaseContext.Rooms.AddRange(GetPreconfiguredRooms());
                await databaseContext.SaveChangesAsync();
            }

  
        }

        private static IEnumerable<User> GetPreconfiguredUsers()
        {
            return new List<User>
            {
                new User()
                {
                    Name = "ahmet", Surname = "türkyılmaz", Email = "turkyilmazah@gmail.com", PasswordHash = "123456"
                }
            };
        }

        private static IEnumerable<Room> GetPreconfiguredRooms()
        {
            return new List<Room>
            {
                new Room()
                {
                    Name = "Room 1", CreatedAt = DateTime.Now
                }
            };
        }


    }
}