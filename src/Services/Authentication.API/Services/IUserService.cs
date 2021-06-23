using auth_service_dotnet.Models.response;
using auth_service_dotnet.Models.Users;
using Authentication.API.Entities;
using Authentication.API.Model.request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Services
{
    public interface IUserService
    {
        Task<JwtResponse> Authenticate(LoginRequest loginRequest);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(string id);
        Task<User> Create(SignupRequest signupRequest);
        Task<bool> Update( UserUpdateRequest user, User storedUser);
        void Delete(string id);
    }
}
