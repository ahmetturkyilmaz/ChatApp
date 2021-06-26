using System.Threading.Tasks;
using System.Net;
using Chat.API.Helpers;
using Chat.API.Models;
using Chat.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers
{
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
        [HttpGet("{roomId}")]
        public async Task<ActionResult<RoomDto>> GetById(int roomId)
        {
            var result = await _roomService.GetById(roomId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("{roomId}/join", Name = "CreateRoomUser")]
        [ProducesResponseType(typeof(MessageDto), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<MessageDto>> CreateRoomUser(int roomId)
        {
            var storedUserId = (string) HttpContext.Items["user"];

            await _userRoomService.PostRoomUser(int.Parse(storedUserId), roomId);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(MessageDto), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<MessageDto>> Create([FromBody] RoomDto roomDto)
        {
            var storedUserId = (string) HttpContext.Items["user"];

            var result = await _roomService.SaveRoom(int.Parse(storedUserId), roomDto);

            return Ok(result);
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