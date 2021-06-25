using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Chat.API.Entities;
using Chat.API.Helpers;
using Chat.API.Models.request;
using Chat.API.Services;


namespace Chat.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
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
        [Route("{id}", Name = "GetUserById")]
        [ProducesResponseType(typeof(User), (int) HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }

        [Authorize]
        [Route("me", Name = "GetUserByJWT")]
        [ProducesResponseType(typeof(User), (int) HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<User>> GetUserByJWT(int id)
        {
            var storedUserId = (int) HttpContext.Items["user"];
            var user = await _userService.GetById(id);
            return Ok(user);
        }

        [Authorize]
        [Route("me/rooms", Name = "GetUserByJWTWithRooms")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<User>> GetUserRoomsByJWT(int id)
        {
            var storedUserId = (int)HttpContext.Items["user"];
            var user = await _userService.GetById(id);
            return Ok(user);
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateUser([FromBody] UserUpdateRequest user)
        {
            var storedUser = (int) HttpContext.Items["user"];
            var isUpdated = await _userService.Update(user, storedUser);

            return Ok(isUpdated);
        }
    }
}