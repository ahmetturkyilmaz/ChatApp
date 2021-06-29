using System.Threading.Tasks;
using System.Net;
using Chat.API.Helpers;
using Chat.API.Models;
using Chat.API.Models.response;
using Chat.API.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/rooms")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IRoomUserService _userRoomService;

        public RoomController(IRoomService roomService, IRoomUserService userRoomService)
        {
            _roomService = roomService;
            _userRoomService = userRoomService;
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseModel<RoomDto>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<RoomDto>> GetById(int id)
        {
            var result = await _roomService.GetById(id);
            return Ok(new ResponseModel<RoomDto>(HttpStatusCode.OK, result));
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/invite", Name = "CreateRoomUser")]
        [ProducesResponseType(typeof(ResponseModel<string>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseModel<string>>> CreateRoomUser(int id,
            [FromBody] InviteUserDto inviteUserDto)
        {
            await _userRoomService.PostRoomUser(inviteUserDto, id);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel<RoomDto>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseModel<RoomDto>>> Create([FromBody] RoomDto roomDto)
        {
            var storedUserId = (string) HttpContext.Items["user"];

            var result = await _roomService.SaveRoom(int.Parse(storedUserId), roomDto);

            return Ok(new ResponseModel<RoomDto>(HttpStatusCode.OK, result));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roomService.DeleteRoom(id);

            return NoContent();
        }
    }
}