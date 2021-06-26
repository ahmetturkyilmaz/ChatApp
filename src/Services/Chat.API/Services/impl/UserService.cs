using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Chat.API.Entities;
using Chat.API.Exceptions;
using Chat.API.Helpers;
using Chat.API.Models;
using Chat.API.Models.request;
using Chat.API.Models.response;
using Chat.API.Repository;
using BC = BCrypt.Net.BCrypt;
using Chat.API.Util;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace Chat.API.Services.impl
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
            _userRepository = unitOfWork.UserRepository;
        }

        public async Task<JwtResponse> Authenticate(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetUserByEmail(loginRequest.Email);
            if (user == null || !BC.Verify(loginRequest.Password, user.PasswordHash))
            {
                // authentication failed
                throw new AuthenticationFailedException("Username or Password is invalid");
            }

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);
            return new JwtResponse(token, user.Email, user.Name, user.Surname);
        }


        public async Task<UserDto> Create(SignupRequest signupRequest)
        {
            var user = new UserDto(signupRequest.Email.Trim(),
                signupRequest.FirstName.Trim(),
                signupRequest.LastName.Trim(),
                signupRequest.Password.Trim());

            UserValidator.ValidateUser(user);
            user.PasswordHash = BC.HashPassword(user.PasswordHash);
            var result = await _userRepository.CreateUser(user);
            await _unitOfWork.Save();
            return result;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<UserResponse> GetById(int id)

        {
            var user = await _userRepository.GetUserById(id);

            return new UserResponse(user.Id, user.Email, user.Name, user.Surname);
        }

        public async Task<UserResponseWithRooms> GetUserWithRooms(int id)

        {
            var user = await _userRepository.GetUserWithRooms(id);

            return user;
        }

        public async Task<List<RoomResponse>> GetUserRooms(int id)
        {
            var userWithRooms = await GetUserWithRooms(id);
            var rooms = userWithRooms.Rooms;
            if (rooms == null)
            {
                throw new RoomNotFoundException();
            }

            List<RoomResponse> roomResponses = new List<RoomResponse>();

            foreach (var room in rooms)
            {
                roomResponses.Add(new RoomResponse(room.Id, room.Name, room.CreatedAt));
            }

            return roomResponses;
        }

        public async Task<UserDto> Update(UserUpdateRequest updateRequest, int storedUserId)
        {
            var storedUser = await _userRepository.GetUserById(storedUserId);
            if (storedUser.Email != updateRequest.Email)
            {
                var user = await _userRepository.GetUserByEmail(updateRequest.Email);
                if (user != null)
                {
                    throw new UserAlreadyExistsException();
                }

                storedUser.Email = updateRequest.Email;
            }

            if (!BC.Verify(updateRequest.OldPassword, storedUser.PasswordHash))
            {
                throw new RequestValidationException();
            }

            storedUser.PasswordHash = BC.HashPassword(updateRequest.newPassword);
            UserValidator.ValidateUser(storedUser);
            var result = await _userRepository.UpdateUser(storedUser);

            await _unitOfWork.Save();

            return result;
        }

        private string GenerateJwtToken(UserDto user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim("id", user.Id.ToString())}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void Delete(int id)
        {
            _userRepository.DeleteUser(id);
            _unitOfWork.Save();
        }
    }
}