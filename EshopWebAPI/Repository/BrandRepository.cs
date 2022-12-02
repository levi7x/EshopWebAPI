using EshopWebAPI.Data;
using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;

namespace EshopWebAPI.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool BrandExists(int id)
        {
            return _context.Brands.Any(b => b.Id == id);
        }

        public Brand GetBrand(int id)
        {
            return _context.Brands.Where(b => b.Id == id).FirstOrDefault();
        }

        public Brand GetBrand(string name)
        {
            return _context.Brands.Where(b => b.BrandName == name).FirstOrDefault();
        }

        public ICollection<Brand> GetBrands()
        {
            return _context.Brands.ToList();
        }
    }
}
