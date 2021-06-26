using System.Collections.Generic;
using System.Threading.Tasks;
using Chat.API.Entities;
using Chat.API.Models;

namespace Chat.API.Repository
{
    public interface IRoomRepository
    {
        public  Task<RoomDto> GetByRoomName(int userId, string name);
        public Task<RoomDto> Get(int id);
        public Task<Room> SaveRoom(RoomDto room);
        public Task Delete(int id);
        public Task Update(RoomDto roomDto);
    }
}
