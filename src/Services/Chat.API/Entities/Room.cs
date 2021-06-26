using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Entities
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Message> Messages { get; set; }

        public ICollection<RoomUser> RoomUsers { get; set; }
    }
}
