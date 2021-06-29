using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class MessageSendEvent
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FromUserId { get; set; }
        public int ToRoomId { get; set; }

    }
}
