namespace deliverySystem_Sharqiya.Dtos.Order
{
    public class CreateOrderInput
    {
        public decimal Tips { get; set; }
        public decimal Cash { get; set; }
        public int DriverId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
