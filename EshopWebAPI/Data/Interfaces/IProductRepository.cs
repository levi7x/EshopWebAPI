using EshopWebAPI.Models;

namespace EshopWebAPI.Data.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        public Product GetProduct(int id);
        public Product GetProduct(string productName);
    }
}
