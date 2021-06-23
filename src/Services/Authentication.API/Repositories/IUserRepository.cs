using Authentication.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Repositories
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
