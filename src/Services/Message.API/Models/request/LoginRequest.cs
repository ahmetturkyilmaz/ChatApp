using System.ComponentModel.DataAnnotations;

namespace Message.API.Models.request
{
    public class LoginRequest
    {
        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }
    }
}