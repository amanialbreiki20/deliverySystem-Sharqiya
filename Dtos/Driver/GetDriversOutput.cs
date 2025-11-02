using deliverySystem_Sharqiya.Models;

namespace deliverySystem_Sharqiya.Dtos.Driver
{
    public class GetDriversOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ResidentId { get; set; }
        public string Talabatid { get; set; }
        public string PersonalNumber { get; set; }
        public DriverStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
