using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Models
{
    public class RoomDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<MessageDto> Messages { get; set; }
        public ICollection<UserDto> Users { get; set; }

        public RoomDto()
        {
        }
    }
}