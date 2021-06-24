using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.API.Entities;
using Chat.API.Models.request;
using Chat.API.Models.response;


namespace Chat.API.Services
{
    public interface IUserService
    {
        Task<JwtResponse> Authenticate(LoginRequest loginRequest);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> Create(SignupRequest signupRequest);
        Task<bool> Update( UserUpdateRequest user, int storedUserId);
        void Delete(int id);
    }
}
