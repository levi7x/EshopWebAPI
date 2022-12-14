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

        public bool CreateProduct(Product product)
        {
            _context.Products.Add(product);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateProduct(Product product)
        {
            _context.Update(product);
            return Save();
        }

        public bool DeleteProduct(int id)
        {
            var product = GetProduct(id);
            _context.Remove(product);
            return Save();
        }

        
        public bool AddBrandToProduct(Brand brand, Product product)
        {
            product.Brand = brand;

            _context.Update(product);
            return Save();
            
        }

        public bool AddProductToCategory(Product product, Category category)
        {
            var productCategory = new ProductCategory()
            {
                ProductId = product.Id,
                CategoryId = category.Id
            };

            _context.ProductCategories.Add(productCategory);
            return Save();
        }

        public bool RemoveProductFromCategory(Product product, Category category)
        {
            var productCategory = _context.ProductCategories.FirstOrDefault(pc=>pc.ProductId== product.Id && pc.CategoryId == category.Id);
            _context.Remove(productCategory);
            return Save();
        }

        public bool IfProductHasCategory(Product product, Category category)
        {
            var productCategory = _context.ProductCategories.Any(pc => pc.ProductId == product.Id && pc.CategoryId == category.Id);

            if (productCategory) { return true; };

            return false;
        }
    }
}
