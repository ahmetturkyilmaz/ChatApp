using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.API.Entities;
using Chat.API.Models;

namespace Chat.API.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetUsers();

        Task<UserDto> GetUserById(int id);

        Task<UserDto> GetUserByEmail(string email);
        Task<UserDto> CreateUser(UserDto user);
        Task<UserDto> UpdateUser(UserDto user);

        Task DeleteUser(int id);
    }
}
