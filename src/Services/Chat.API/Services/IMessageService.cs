using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.API.Models;
using Chat.API.Models.response;

namespace Chat.API.Services
{
    public interface IMessageService
    {
        public Task<List<MessageDto>> GetAllMessagesByRoomId(int roomId);
        public Task<MessageDto> GetById(int messageId);
        public Task<MessageDto> SaveMessage(MessageDto message);

        public Task<List<MessageDto>> GetByPagination(int roomId, int now, int next);
        public Task UpdateMessage(MessageDto message);
        public Task DeleteMessage(int id);
    }
}