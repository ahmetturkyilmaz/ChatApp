using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.API.Models;
using Chat.API.Models.response;


namespace Chat.API.Services
{
    public interface IRoomService
    {
        public Task<RoomDto> GetById(int roomId);
        public Task<RoomResponse> SaveRoom(int userId, RoomDto room);
        public Task UpdateRoom(RoomDto room);
        public Task DeleteRoom(int id);
    }
}