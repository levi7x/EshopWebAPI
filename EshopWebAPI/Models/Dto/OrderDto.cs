using EshopWebAPI.Data.Enum;

namespace EshopWebAPI.Models.Dto
{
    public class OrderDto
    {
        public DateTime CreatedOrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsOrderActive { get; set; }
    }
}
