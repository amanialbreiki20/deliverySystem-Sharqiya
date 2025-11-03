using deliverySystem_Sharqiya.Models;

namespace deliverySystem_Sharqiya.Dtos.Order
{
    public class UpdateOrderInput
    {
        public decimal Tips { get; set; }
        public decimal Cash { get; set; }
        public int DriverId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Date { get; set; }
    }
}
