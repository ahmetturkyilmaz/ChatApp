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
using Microsoft.AspNetCore.Cors;


namespace Chat.API.Controllers
{
    [EnableCors("AllowAll")]
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
        public async Task<ActionResult<ResponseModel<JwtResponse>>> Authenticate([FromBody] LoginRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new {message = "Username or password is incorrect"});

            return Ok(new ResponseModel<JwtResponse>(HttpStatusCode.OK, response));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> RegisterUser([FromBody] SignupRequest model)
        {
            var response = await _userService.Create(model);
            return Ok(new ResponseModel<UserResponse>(HttpStatusCode.OK, response));
        }

        [Authorize]
        [ProducesResponseType(typeof(ResponseModel<List<UserResponse>>), (int) HttpStatusCode.OK)]
        [HttpGet("users")]
        public async Task<ActionResult<List<UserResponse>>> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(new ResponseModel<List<UserResponse>>(HttpStatusCode.OK, users));
        }

        [Authorize]
        [Route("{id}", Name = "GetUserById")]
        [ProducesResponseType(typeof(ResponseModel<UserResponse>), (int) HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetUserById(int id)
        {
            var user = await _userService.GetById(id);
            return Ok(new ResponseModel<UserResponse>(HttpStatusCode.OK, user));
        }

        [Authorize]
        [Route("user-rooms/{id}", Name = "GetUserWithRooms")]
        [ProducesResponseType(typeof(ResponseModel<UserResponse>), (int) HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<ResponseModel<UserResponse>>> GetUserWithRooms(int id)
        {
            var user = await _userService.GetUserWithRooms(id);
            return Ok(new ResponseModel<UserResponse>(HttpStatusCode.OK, user));
        }

        [Authorize]
        [Route("me", Name = "GetUserByJWT")]
        [ProducesResponseType(typeof(ResponseModel<UserResponse>), (int) HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<ResponseModel<UserResponse>>> GetUserByJWT(int id)
        {
            var storedUserId = (string) HttpContext.Items["user"];
            var user = await _userService.GetById(int.Parse(storedUserId));
            return Ok(new ResponseModel<UserResponse>(HttpStatusCode.OK, user));
        }

        [Authorize]
        [Route("me/rooms", Name = "GetUserRooms")]
        [ProducesResponseType(typeof(ResponseModel<List<RoomDto>>), (int) HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<RoomDto>>>> GetUserRooms()
        {
            var storedUserId = (string) HttpContext.Items["user"];
            var rooms = await _userService.GetUserRooms(int.Parse(storedUserId));
            return Ok((new ResponseModel<List<RoomDto>>(HttpStatusCode.OK, rooms)));
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