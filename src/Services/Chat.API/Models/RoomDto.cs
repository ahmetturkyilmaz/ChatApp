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
        [Required] public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastMessageAt { get; set; }

        public RoomDto()
        {
        }

        public RoomDto(int id, string name, DateTime createdAt, DateTime lastMessageAt)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
            LastMessageAt = lastMessageAt;
        }
    }
}