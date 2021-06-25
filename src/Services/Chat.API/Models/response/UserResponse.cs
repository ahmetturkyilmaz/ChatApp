using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Models.response
{
    public class UserResponse
    {
        public long Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }

        public UserResponse(long id, string email, string name, string surname)
        {
            Id = id;
            Email = email;
            Name = name;
            Surname = surname;
        }
    }
}
