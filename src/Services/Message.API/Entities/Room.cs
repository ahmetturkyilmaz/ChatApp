using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Message.API.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RoomUser> Users { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
