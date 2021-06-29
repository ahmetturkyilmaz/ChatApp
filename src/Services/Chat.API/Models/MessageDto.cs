using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Models
{
    public class MessageDto
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FromUserId { get; set; }
        [Required]
        public int ToRoomId { get; set; }

        public MessageDto()
        {
        }
    }
}
