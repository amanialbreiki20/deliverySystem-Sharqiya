using deliverySystem_Sharqiya.Models;

namespace deliverySystem_Sharqiya.Dtos.User
{
    public class UpdateUserInput
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? MobileNumber { get; set; }
        public string? Prefix { get; set; }
        public UserType? Type { get; set; }
        public UserStatus? Status { get; set; }
    }
}
