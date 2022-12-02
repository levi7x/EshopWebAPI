using EshopWebAPI.Data.Enum;

namespace EshopWebAPI.Models.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedOrderDate { get; set; }
        public OrderStatus Status { get; set; }
    }
}
