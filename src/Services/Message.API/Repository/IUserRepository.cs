using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Message.API.Entities;

namespace Message.API.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUserById(string id);

        Task<User> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
        Task<bool> UpdateUser(User user);

        Task<bool> DeleteUser(string id);
    }
}
