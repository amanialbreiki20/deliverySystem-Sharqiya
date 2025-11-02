using System.ComponentModel.DataAnnotations;

namespace deliverySystem_Sharqiya.Dtos.Driver
{
    public class UpdateDriverInput
    {
        [Required]
        public string Name { get; set; }
        public string ResidentId { get; set; }
        public string Talabatid { get; set; }
        public string PersonalNumber { get; set; }
    }
}
