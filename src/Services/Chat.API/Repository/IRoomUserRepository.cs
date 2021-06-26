using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Repository
{
    public interface IRoomUserRepository
    {
        public Task PostRoomUser(int userId, int roomId);

    }
}
