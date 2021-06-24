using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.API.Entities;

namespace Chat.API.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUserById(int id);

        Task<User> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
        Task<bool> UpdateUser(User user);

        Task<bool> DeleteUser(int id);
    }
}
