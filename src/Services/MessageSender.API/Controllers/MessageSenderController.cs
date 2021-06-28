using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using MessageSender.API.Models;

namespace MessageSender.API.Controllers
{
    [ApiController]
    [Route("message-queue")]
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

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Accepted)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> MessageQueue([FromBody] MessageDto messageDto)
        {
            var eventMessage = _mapper.Map<MessageSendEvent>(messageDto);
            await _publishEndpoint.Publish(eventMessage);
            return Accepted();
        }
    }
}