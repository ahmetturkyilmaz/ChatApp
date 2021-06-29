using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Chat.API.Helpers;
using Chat.API.Models;
using Chat.API.Models.response;
using Chat.API.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace Chat.API.Controllers
{
    [EnableCors("AllowAll")]
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
        [Route("room/all/{roomId}", Name = "GetAllByRoomId")]
        [ProducesResponseType(typeof(ResponseModel<IList<MessageDto>>), (int) HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<List<MessageDto>>> GetAllByRoomId(int roomId)
        {
            var result = await _service.GetAllMessagesByRoomId(roomId);
            return Ok(new ResponseModel<List<MessageDto>>(HttpStatusCode.Accepted, result));
        }

        [Authorize]
        [Route("room/{roomId}", Name = "GetByPagination")]
        [ProducesResponseType(typeof(ResponseModel<List<MessageDto>>), (int) HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<List<MessageDto>>> GetByPagination([FromQuery(Name = "start")] int start, [FromQuery(Name = "count")] int count, int roomId)
        {
            var result = await _service.GetByPagination(roomId, start, count);
            return Ok(new ResponseModel<List<MessageDto>>(HttpStatusCode.Accepted, result));
        }

        [Authorize]
        [ProducesResponseType(typeof(ResponseModel<MessageDto>), (int) HttpStatusCode.OK)]
        [HttpPost]
        public async Task<ActionResult<MessageDto>> Create([FromBody] MessageDto message)
        {
            var storedUserId = (string) HttpContext.Items["user"];
            message.FromUserId = int.Parse(storedUserId);
            var result = await _service.SaveMessage(message);

            return Ok(new ResponseModel<MessageDto>(HttpStatusCode.Accepted, result));
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