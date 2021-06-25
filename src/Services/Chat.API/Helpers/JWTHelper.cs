using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Chat.API.Repository;

namespace Chat.API.Helpers
{
    public class JWTHelper
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;

        public JWTHelper(RequestDelegate next, IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken) validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

                // Check if the user exists
                _unitOfWork.UserRepository.GetUserById(int.Parse(userId));

                // attach user to context on successful jwt validation
                context.Items["user"] = userId;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}