using System.Threading.Tasks;
using System.Net;
using Message.API.Helpers;
using Message.API.Models;
using Message.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Message.API.Controllers
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
        [HttpGet("room/{roomId}")]
        public async Task<ActionResult<RoomDto>> GetById(int roomId)
        {
            var result = await _service.GetById(roomId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(MessageDto), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<Entities.Message>> Create(RoomDto roomDto)
        {
            var storedUserId = (int) HttpContext.Items["user"];

            var result = _service.SaveRoom(storedUserId, roomDto);

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