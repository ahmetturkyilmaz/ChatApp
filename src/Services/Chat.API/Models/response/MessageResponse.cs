using System;
using System.ComponentModel.DataAnnotations;

namespace Chat.API.Models.response
{
    public class MessageResponse
    {

        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public int FromUserId { get; set; }
        public int ToRoomId { get; set; }
        public MessageResponse(int id, string content, DateTime createdAt, int fromUserId, int toRoomId)
        {
            Id = id;
            Content = content;
            CreatedAt = createdAt;
            FromUserId = fromUserId;
            ToRoomId = toRoomId;
        }

    }
}