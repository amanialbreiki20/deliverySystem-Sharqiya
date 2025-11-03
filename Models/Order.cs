namespace deliverySystem_Sharqiya.Models
{
    public class Order
        {
            public int Id { get; set; }
            public decimal Tips { get; set; }
            public decimal Cash { get; set; }
            public OrderStatus Status { get; set; }

           
            public int DriverId { get; set; }
            public Driver Driver { get; set; }

           
            public int UserId { get; set; }     
            public User User { get; set; }        

            public int CreatedById { get; set; }
            public DateTime Date { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        }

        public enum OrderStatus
        {
            Completed = 1,
            Deleted
        }
}

