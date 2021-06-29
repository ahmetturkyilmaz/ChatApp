using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.API.Entities;
using Chat.API.Models;
using Chat.API.Models.request;
using Chat.API.Models.response;


namespace Chat.API.Services
{
    public interface IUserService
    {
        Task<JwtResponse> Authenticate(LoginRequest loginRequest);
        Task<List<UserResponse>> GetAll();
        Task<UserResponse> GetById(int id);
        Task<UserResponseWithRooms> GetUserWithRooms(int id);
        Task<List<RoomDto>> GetUserRooms(int id);
        Task<UserResponse> Create(SignupRequest signupRequest);
        Task<UserDto> Update(UserUpdateRequest user, int storedUserId);
        void Delete(int id);
    }
}