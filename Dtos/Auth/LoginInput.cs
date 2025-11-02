using System.ComponentModel.DataAnnotations;

namespace deliverySystem_Sharqiya.Dtos.Auth
{
    public class LoginInput
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
