using System.ComponentModel.DataAnnotations;

namespace EshopWebAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
