using System.ComponentModel.DataAnnotations;

namespace EshopWebAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set;}
        public string ProductImageUrl { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public Brand? Brand { get; set; }
    }
}
