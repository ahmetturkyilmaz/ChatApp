using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Services
{
    public interface IRoomUserService
    {
        public Task PostRoomUser(int roomId, int userId);


    }
}