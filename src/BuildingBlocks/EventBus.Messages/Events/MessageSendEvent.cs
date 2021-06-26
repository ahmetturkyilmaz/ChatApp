using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class MessageSendEvent : IntegrationBaseEvent
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FromUserId { get; set; }
        public int ToRoomId { get; set; }
    }
}