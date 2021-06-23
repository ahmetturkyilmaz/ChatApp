using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Message.API.Models
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public RoomUserDto FromUser { get; set; }
        public int ToRoomId { get; set; }
        public RoomDto ToRoom { get; set; }
    }
}
