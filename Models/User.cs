namespace deliverySystem_Sharqiya.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Prefix { get; set; }
        public UserType Type { get; set; }
        public UserStatus Status { get; set; }
        public string ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordSentAt { get; set; }
        public string EmailConfimationToken { get; set; }
        public DateTime? EmailConfimationTokenSentAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastLoginAt { get; set; }
    }

    public enum UserType
    {
        Admin = 1,
        User = 2,
    }

    public enum UserStatus
    {
        Active = 1,
        Inactive = 2,
        Deleted = 3
    }


}
