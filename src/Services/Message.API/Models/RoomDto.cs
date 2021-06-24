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
        public DateTime CreatedAt { get; set; }
        public ICollection<int> UsersIds { get; set; }
        public ICollection<MessageDto> Messages { get; set; }
    }
}