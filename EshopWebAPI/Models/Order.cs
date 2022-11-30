using EshopWebAPI.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace EshopWebAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedOrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderDetails> Details { get; set; }
    }
}
