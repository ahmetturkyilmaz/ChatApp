using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Message.API.Models;

namespace Message.API.Services
{
    public interface IRoomService
    {
        public Task<IList<RoomDto>> GetAllByUserId(int userId);
        public Task<RoomDto> GetById(int roomId);
        public Task SaveRoom(int userId, RoomDto room);
        public Task UpdateRoom(RoomDto room);
        public Task DeleteRoom(int id);
    }
}