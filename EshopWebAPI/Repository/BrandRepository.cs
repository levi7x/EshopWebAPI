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

        public bool CreateBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            return Save();
        }

        public Brand GetBrand(int id)
        {
            return _context.Brands.Where(b => b.Id == id).FirstOrDefault();
        }

        public Brand GetBrand(string name)
        {
            return _context.Brands.Where(b => b.BrandName.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public ICollection<Brand> GetBrands()
        {
            return _context.Brands.ToList();
        }

        public bool UpdateBrand(Brand brand)
        {
            _context.Brands.Update(brand);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
