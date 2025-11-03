using deliverySystem_Sharqiya.Models;

namespace deliverySystem_Sharqiya.Dtos.Order
{
    public class GetOrdersOutput
    {
        public int Id { get; set; }
        public decimal Tips { get; set; }
        public decimal Cash { get; set; }
        public OrderStatus Status { get; set; }
        public string DriverName { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
