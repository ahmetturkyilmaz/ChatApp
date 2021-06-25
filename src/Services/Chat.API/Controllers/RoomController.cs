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
        private readonly IRoomService _service;

        public RoomController(IRoomService roomService)
        {
            _service = roomService;
        }

        [Authorize]
        [HttpGet("{roomId}")]
        public async Task<ActionResult<RoomDto>> GetById(int roomId)
        {
            var result = await _service.GetById(roomId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(MessageDto), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<MessageDto>> Create([FromBody] RoomDto roomDto)
        {
            var storedUserId = (int) HttpContext.Items["user"];

            var result = await _service.SaveRoom(storedUserId, roomDto);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteRoom(id);

            return NoContent();
        }
    }
}