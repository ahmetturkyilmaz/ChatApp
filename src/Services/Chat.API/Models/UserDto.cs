using System.Collections.Generic;

namespace Chat.API.Models
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<RoomDto> Rooms { get; set; }

        public UserDto()
        {
        }

        public UserDto(string email, string name, string surname, string password)
        {
            Email = email;
            Name = name;
            Surname = surname;
            PasswordHash = password;
        }
    }
}