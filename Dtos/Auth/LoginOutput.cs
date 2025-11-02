using deliverySystem_Sharqiya.Models;

namespace deliverySystem_Sharqiya.Dtos.Auth
{
    public class LoginOutput
    {
        public string Token { get; set; }
        public List<string> Permissions { get; set; }
        public UserType UserType { get; set; }
    }
}
