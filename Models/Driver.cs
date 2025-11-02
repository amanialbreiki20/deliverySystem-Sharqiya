namespace deliverySystem_Sharqiya.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ResidentId { get; set; }
        public string Talabatid { get; set; }
        public string PersonalNumber { get; set; }
        public DriverStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Order> Orders { get; set; }
        public List<DailyDistanceTracker> DailyDistanceTrackers { get; set; }
    }

    public enum DriverStatus
    {
        Active = 1,
        InActive
    }
}
