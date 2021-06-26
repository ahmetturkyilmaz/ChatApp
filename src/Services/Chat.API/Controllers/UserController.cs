using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Chat.API.Entities;
using Chat.API.Helpers;
using Chat.API.Models;
using Chat.API.Models.request;
using Chat.API.Models.response;
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
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] SignupRequest model)
        {
            var response = await _userService.Create(model);
            return Ok(response);
        }

        [Authorize]
        [ProducesResponseType(typeof(UserDto), (int) HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
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
        [Route("user-rooms/{id}", Name = "GetUserWithRooms")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUserWithRooms(int id)
        {
            var user = await _userService.GetUserWithRooms(id);
            return Ok(user);
        }
        [Authorize]
        [Route("me", Name = "GetUserByJWT")]
        [ProducesResponseType(typeof(UserResponse), (int) HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetUserByJWT(int id)
        {
            var storedUserId = (string) HttpContext.Items["user"];
            var user = await _userService.GetById(int.Parse(storedUserId));
            return Ok(user);
        }

        [Authorize]
        [Route("me/rooms", Name = "GetUserRooms")]
        [ProducesResponseType(typeof(RoomResponse), (int) HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<RoomResponse>> GetUserRooms()
        {
            var storedUserId = (string) HttpContext.Items["user"];
            var user = await _userService.GetUserRooms(int.Parse(storedUserId));
            return Ok(user);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateUser([FromBody] UserUpdateRequest user)
        {
            var storedUser = (string) HttpContext.Items["user"];
            var isUpdated = await _userService.Update(user, int.Parse(storedUser));

            return Ok(isUpdated);
        }
    }
}