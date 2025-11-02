namespace deliverySystem_Sharqiya.Models
{
    public class DriverToVehicle
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
