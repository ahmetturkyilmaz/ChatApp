using System.Net;
using System.Threading.Tasks;
using Chat.API.Helpers;
using Chat.API.Models;
using Chat.API.Models.response;
using Chat.API.Services;
using Microsoft.AspNetCore.Mvc;


namespace Chat.API.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [Authorize]
        [Route("room/{roomId}", Name = "GetAllByRoomId")]
        [HttpGet]
        public async Task<ActionResult<MessageDto>> GetAllByRoomId(int roomId)
        {
            var result = await _service.GetAllMessagesByRoomId(roomId);
            return Ok(result);
        }

        [Authorize]
        [Route("room/{roomId}/{now}/{next}", Name = "GetUserByJWTWithRooms")]
        public async Task<ActionResult<MessageResponse>> GetByPagination(int roomId, int now, int next)
        {
            var result = await _service.GetByPagination(roomId, now, next);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(MessageDto), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<MessageDto>> Create([FromBody] MessageDto message)
        {
            var result = _service.SaveMessage(message);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteMessage(id);
            return NoContent();
        }
    }
}