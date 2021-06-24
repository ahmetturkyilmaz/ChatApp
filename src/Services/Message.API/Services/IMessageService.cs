﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Message.API.Models;

namespace Message.API.Services
{
    public interface IMessageService
    {
        public Task<IList<MessageDto>> GetAllMessagesByRoomId(int roomId);
        public Task<MessageDto> GetById(int messageId);
        public Task SaveMessage(MessageDto message);
        public Task UpdateMessage(MessageDto message);
        public Task DeleteMessage(int id);
    }
}