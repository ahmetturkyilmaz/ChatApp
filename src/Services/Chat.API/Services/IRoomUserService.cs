using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.API.Models;

namespace Chat.API.Services
{
    public interface IRoomUserService
    {
        public Task PostRoomUser(InviteUserDto inviteUserDto,int roomId);


    }
}