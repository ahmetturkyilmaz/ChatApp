using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Chat.API.Entities;
using Chat.API.Exceptions;
using Chat.API.Helpers;
using Chat.API.Models.request;
using Chat.API.Models.response;
using Chat.API.Repositories;
using BC = BCrypt.Net.BCrypt;
using Chat.API.Util;

namespace Chat.API.Services.impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            this._userRepository = userRepository;
            this._appSettings = appSettings.Value;
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


        public async Task<User> Create(SignupRequest signupRequest)
        {
            var user = new User(signupRequest.Email.Trim(),
                signupRequest.FirstName.Trim(),
                signupRequest.LastName.Trim(),
                signupRequest.Password.Trim());

            UserValidator.ValidateUser(user);
            user.PasswordHash = BC.HashPassword(user.PasswordHash);
            return await _userRepository.CreateUser(user);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<User> GetById(int id)

        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return user;
        }

        public async Task<bool> Update(UserUpdateRequest updateRequest, int storedUserId)
        {
            var storedUser = await GetById(storedUserId);
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
            return await _userRepository.UpdateUser(storedUser);
        }

        private string GenerateJwtToken(User user)
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
        }
    }
}