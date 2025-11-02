using deliverySystem_Sharqiya.Models;

namespace deliverySystem_Sharqiya.Helpers.ServiceContext
{
    public class RequestHeaderContent
    {
        public int UserId { get; set; }
        public UserType UserType { get; set; }
        public string UserMobileNumber { get; set; }
        public string UserEmail { get; set; }
        public string EndPointName { get; set; }
    }
}