using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.API.Models;

namespace Chat.API.Repository
{
    public interface IRoomRepository
    {
        public Task<IList<RoomDto>> GetAllByUserId(int userId);
        public  Task<RoomDto> GetByRoomName(int userId, string name);

        public Task<RoomDto> Get(int id);
        public Task<RoomDto> SaveRoom(RoomDto room);
        public Task Delete(int id);
        public Task Update(RoomDto roomDto);
    }
}
