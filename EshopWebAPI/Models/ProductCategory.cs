namespace EshopWebAPI.Models
{
    public class ProductCategory
    {
        public Category Category { get; set; }
        public Product Product { get; set; }
        public int CategoryId { get;set; }
        public int ProductId { get;set; }
    }
}
