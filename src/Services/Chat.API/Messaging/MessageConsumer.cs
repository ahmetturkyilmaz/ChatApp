using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chat.API.Entities;
using Chat.API.Models;
using Chat.API.Services;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Chat.API.Messaging
{
    public class MessageConsumer : IConsumer<MessageSendEvent>
    {
        readonly ILogger<MessageConsumer> _logger;
        private readonly IMapper _mapper;
        private readonly IMessageService _service;

        public MessageConsumer(ILogger<MessageConsumer> logger, IMapper mapper, IMessageService service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        public async Task Consume(ConsumeContext<MessageSendEvent> context)
        {
            var command = _mapper.Map<MessageDto>(context.Message);
            var result = await _service.SaveMessage(command);
            _logger.LogInformation("Message with id " + result.Id + " and content '" + result.Content + "' is saved");
        }
    }
}