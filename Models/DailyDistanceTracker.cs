namespace deliverySystem_Sharqiya.Models
{
    public class DailyDistanceTracker
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public DistanceStatus Unit { get; set; }
        public decimal Start { get; set; }
        public decimal End { get; set; }
        public int CreatedById { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }

    public enum DistanceStatus
    {
        KM = 1,
        Mile
    }
}
