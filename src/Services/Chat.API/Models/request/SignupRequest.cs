using System.ComponentModel.DataAnnotations;

namespace Chat.API.Models.request
{
    public class SignupRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}