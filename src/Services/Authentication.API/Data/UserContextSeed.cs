using Authentication.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Data
{
    public class UserContextSeed
    {
        public static void SeedData(IMongoCollection<User> userCollection)
        {
            bool existUser = userCollection.Find(user => true).Any();
            if (!existUser)
            {
                userCollection.InsertManyAsync(GetPreConfiguredUsers());
            }
        }

        private static IEnumerable<User> GetPreConfiguredUsers()
        {
            return new List<User>{ 
                new User() {
                    Id=1,
                    Name="Ahmet",
                    Email="turkyilmazah@gmail.com",
                    },
                new User() { }
            };
        }
    }
}
