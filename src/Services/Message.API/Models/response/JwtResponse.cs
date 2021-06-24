using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Message.API.Models.response
{
    public class JwtResponse
    {

        public string AccessToken { get; set; }
        public string Type = "Bearer";
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public JwtResponse(string accessToken, string email, string name, string surname)
        {
            AccessToken = accessToken;
            Email = email;
            Name = name;
            Surname = surname;
        }
    }
}
