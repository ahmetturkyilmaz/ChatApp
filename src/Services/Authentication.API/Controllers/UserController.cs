using auth_service_dotnet.Models.Users;
using Authentication.API.Entities;
using Authentication.API.Model.request;
using Authentication.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Authentication.API.Helpers;

namespace Authentication.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new {message = "Username or password is incorrect"});

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser([FromBody] SignupRequest model)
        {
            var response = await _userService.Create(model);
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(User), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [Authorize]
        [Route("{id:length(24)}", Name = "GetUserById")]
        [ProducesResponseType(typeof(User), (int) HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateUser([FromBody] UserUpdateRequest user)
        {
            var storedUser = (User) HttpContext.Items["user"];
            var isUpdated = await _userService.Update(user, storedUser);
            return Ok(isUpdated);
        }
    }
}