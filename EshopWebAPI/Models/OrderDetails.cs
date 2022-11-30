using System.ComponentModel.DataAnnotations;

namespace EshopWebAPI.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        public int Pieces { get; set; }
        public Order Order { get; set; }
        public ICollection<Product> Products { get; set;}
    }
}
