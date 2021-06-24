using System.Collections.Generic;
using System.Threading.Tasks;
using Message.API.Entities;
using Message.API.Models.request;
using Message.API.Models.response;

namespace Message.API.Services
{
    public interface IUserService
    {
        Task<JwtResponse> Authenticate(LoginRequest loginRequest);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(string id);
        Task<User> Create(SignupRequest signupRequest);
        Task<bool> Update( UserUpdateRequest user, int storedUserId);
        void Delete(string id);
    }
}
