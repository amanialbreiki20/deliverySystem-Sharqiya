namespace deliverySystem_Sharqiya.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public VehicleType Type { get; set; }
        public string PlateNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }

    public enum VehicleType
    {
        Car = 1,
        Bike
    }
}
