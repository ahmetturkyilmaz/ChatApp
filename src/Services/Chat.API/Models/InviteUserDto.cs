using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Models
{
    public class InviteUserDto
    {
        public int UserId { get; set; }

        public InviteUserDto(int userId, int roomId)
        {
            UserId = userId;
        }
    }
}
