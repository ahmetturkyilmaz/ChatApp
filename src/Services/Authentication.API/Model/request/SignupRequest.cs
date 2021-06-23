using System.ComponentModel.DataAnnotations;

namespace auth_service_dotnet.Models.Users
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