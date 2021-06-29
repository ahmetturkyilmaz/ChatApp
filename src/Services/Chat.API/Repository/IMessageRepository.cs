using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.API.Models;
using Chat.API.Models.response;

namespace Chat.API.Repository
{
    public interface IMessageRepository
    {
        public Task<List<MessageDto>> GetAllByRoomId(int roomId);
        public Task<MessageDto> Get(int id);
        public Task<List<MessageDto>> GetByPagination(int roomId, int now, int next);
        public Task<MessageDto> SaveMessage(MessageDto message);
        public Task Delete(int id);
        public Task Update(MessageDto messageDto);
    }
}