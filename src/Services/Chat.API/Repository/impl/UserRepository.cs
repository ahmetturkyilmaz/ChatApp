﻿
using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.API.Entities;
using Chat.API.Exceptions;
using Chat.API.Repositories;

namespace Chat.API.Repository.impl
{
    public class UserRepository : IUserRepository
    {

    

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await context
                .Users
                .Find(user => true)
                .ToListAsync();
        }

        public Task<User> GetUserById(string id)
        {
            return context
                .Users
                .Find(user => user.Id == id)
                .FirstOrDefaultAsync();
            ;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Email, email);
            return await context
                .Users
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            var storedUsers = await GetUserByEmail(user.Email);
            if (storedUsers != null)
            {
                throw new UserAlreadyExistsException();
            }
            await context.Users.InsertOneAsync(user);
            return user;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var updateResult = await context.Users.ReplaceOneAsync(u => u.Id == user.Id, replacement: user);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Id, id);
            var deleteResult = await context.Users.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                   && deleteResult.DeletedCount > 0;
        }
    }
}