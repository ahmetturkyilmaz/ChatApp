using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Message.API.Helpers;
using Message.API.Models;
using Message.API.Services;

namespace Message.API.Controllers
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
        [HttpGet("room/{roomId}")]
        public async Task<ActionResult<MessageDto>> GetAlByRoomId(int roomId)
        {
            var result = await _service.GetAllMessagesByRoomId(roomId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(MessageDto), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<Entities.Message>> Create(MessageDto message)
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