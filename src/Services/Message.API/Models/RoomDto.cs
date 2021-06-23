using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Message.API.Models
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RoomUserDto> Users { get; set; }

        public ICollection<MessageDto> Messages { get; set; }
    }
}
