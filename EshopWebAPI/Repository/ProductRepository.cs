using EshopWebAPI.Data;
using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;

namespace EshopWebAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<Product> GetProducts()
        {
            return _context.Products.OrderBy(p => p.Id).ToList();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public Product GetProduct(string productName)
        {
            return _context.Products.Where(p => p.ProductName == productName).FirstOrDefault();
        }
    }
}
