using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.API.Models;

namespace Chat.API.Repository
{
    public interface IMessageRepository
    {
        public Task<IList<MessageDto>> GetAllByRoomId(int roomId);
        public Task<MessageDto> Get(int id);

        public Task<MessageDto> SaveMessage(MessageDto message);

        public Task Delete(int id);
        public Task Update(MessageDto messageDto);

    }
}