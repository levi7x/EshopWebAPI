using System.ComponentModel.DataAnnotations;

namespace EshopWebAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public bool IsConfirmedOrder { get; set; } = false;
        public DateTime CreatedOrderDate { get; set; }
        public bool IsCanceled { get; set; }
        public string OrderStatus { get; set; }
        public User User { get; set; }
    }
}
