using EshopWebAPI.Models;

namespace EshopWebAPI.Data.Interfaces
{
    public interface IBrandRepository
    {
        public ICollection<Brand> GetBrands();
        public Brand GetBrand(int id);
        public Brand GetBrand(string name);
        public bool CreateBrand(Brand brand);
        public bool UpdateBrand(Brand brand);
        public bool BrandExists(int id);
    }
}
