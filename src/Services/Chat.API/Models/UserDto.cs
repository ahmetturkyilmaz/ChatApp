using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Models
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }

        public UserDto(string email, string name, string surname, string password)
        {
            Email = email;
            Name = name;
            Surname = surname;
            Password = password;
        }
    }
}
