using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MessageSender.API.Helpers;
using MessageSender.API.Models;
using Microsoft.AspNetCore.Cors;

namespace MessageSender.API.Controllers
{
    [EnableCors("AllowAll")]
    [ApiController]
    [Route("api/message")]
    public class MessageSenderController : ControllerBase
    {
        private readonly ILogger<MessageSenderController> _logger;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public MessageSenderController(ILogger<MessageSenderController> logger, IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }


        [Authorize]
        [ProducesResponseType((int) HttpStatusCode.Accepted)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto messageDto)
        {
            var storedUserId = (string) HttpContext.Items["user"];
            messageDto.FromUserId = int.Parse(storedUserId);
            var eventMessage = _mapper.Map<MessageSendEvent>(messageDto);
            await _publishEndpoint.Publish(eventMessage);
            return Accepted();
        }
    }
}