using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FromUserId { get; set; }
        public int ToRoomId { get; set; }
        public Room ToRoom { get; set; }
    }
}
