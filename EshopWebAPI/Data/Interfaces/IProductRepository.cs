using EshopWebAPI.Models;

namespace EshopWebAPI.Data.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
    }
}
