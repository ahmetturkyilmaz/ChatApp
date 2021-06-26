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
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetById(int id)
        {
            var result = await _roomService.GetById(id);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/join", Name = "CreateRoomUser")]
        [ProducesResponseType(typeof(MessageDto), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<MessageDto>> CreateRoomUser(int id)
        {
            var storedUserId = (string) HttpContext.Items["user"];

            await _userRoomService.PostRoomUser(int.Parse(storedUserId), id);

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