using System.ComponentModel.DataAnnotations;

namespace deliverySystem_Sharqiya.Dtos.Auth
{
    public class ResetPasswordInput
    {
        [Required]
        public string Email { get; set; }
    }
}
