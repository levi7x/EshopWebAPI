using EshopWebAPI.Models;

namespace EshopWebAPI.Data.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Product> GetProductByCategory(int categoryId);
        bool CategoryExists(int categoryId);
        public bool CreateCategory(Category category);
        public bool UpdateCategory(Category category);
    }
}
