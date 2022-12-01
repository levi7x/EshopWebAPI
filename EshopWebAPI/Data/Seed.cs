using EshopWebAPI.Models;
using System.Diagnostics.Metrics;

namespace EshopWebAPI.Data
{
    public class Seed
    {
        private readonly ApplicationDbContext dataContext;
        public Seed(ApplicationDbContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Products.Any())
            {
                var productCategory = new List<ProductCategory>()
                {
                    new ProductCategory()
                    {
                        Product = new Product()
                        {
                            ProductName = "Syn gates S",
                            Description = "This is syn gates model s",
                            Price = 1400,
                            Stock = 5,
                            ProductImageUrl = "",

                            ProductCategories = new List<ProductCategory>()
                            {
                                new ProductCategory() {Category = new Category() {CategoryName = "Electric guitars", CategoryDescription = "Electric guitars"}}
                            },
                            Brand = new Brand()
                            {
                                BrandName = "Schecter",
                                BrandDescription = "This is brand called schecter"
                            }
                        }
                    },
                    new ProductCategory()
                    {
                        Product = new Product()
                        {
                            ProductName = "Epiphone special II",
                            Description = "This is epiphone guitar",
                            Price = 250,
                            Stock = 10,
                            ProductImageUrl = "",

                            ProductCategories = new List<ProductCategory>()
                            {
                                new ProductCategory() {Category = new Category() {CategoryName = "Les paul", CategoryDescription = "Electric guitar les paul type"}}
                            },
                            Brand = new Brand()
                            {
                                BrandName = "Epiphone",
                                BrandDescription = "This is brand called epiphone"
                            }
                        }
                    }
                };
                dataContext.ProductCategories.AddRange(productCategory);
                dataContext.SaveChanges();
            }
        }
    }
}
