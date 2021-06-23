using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Message.API.Models;

namespace Message.API.Repository
{
    interface IRoomRepository
    {
        public Task<IList<RoomDto>> GetAllByUserId(int userId);
        public Task<RoomDto> Get(int id);

        public Task<RoomDto> SaveRoom(RoomDto room);
        public Task Delete(int id);
        public void Update(RoomDto roomDto);
    }
}
