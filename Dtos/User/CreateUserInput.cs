using deliverySystem_Sharqiya.Models;

namespace deliverySystem_Sharqiya.Dtos.User
{
    public class CreateUserInput
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Prefix { get; set; }
        public UserType Type { get; set; } = UserType.User;
        public UserStatus Status { get; set; } = UserStatus.Active;
    }
}
