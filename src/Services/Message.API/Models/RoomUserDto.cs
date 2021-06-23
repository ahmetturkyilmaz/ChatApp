using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Message.API.Models
{
    public class RoomUserDto
    {
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public ICollection<RoomDto> Rooms { get; set; }
        public ICollection<MessageDto> Messages { get; set; }
    }
}