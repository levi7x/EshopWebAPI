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
            //if (!dataContext.Products.Any())
            //{
            //    var product = new List<Product>()
            //    {
            //        new Product
            //        {
            //        ProductName = "Syn gates S",
            //        Description = "This is syn gates model s",
            //        Price = 1400,
            //        Stock = 5,
            //        ProductImageUrl = "",
            //        Brand = new Brand()
            //            {
            //                BrandName = "Schecter",
            //                BrandDescription = "This is brand called schecter"
            //            }
            //        },
            //        new Product
            //        {
            //        ProductName = "Les paul standard",
            //        Description = "This is les paul std",
            //        Price = 2500,
            //        Stock = 7,
            //        ProductImageUrl = "",
            //        Brand = new Brand()
            //            {
            //                BrandName = "Les Paul",
            //                BrandDescription = "This is brand called les paul"
            //            }
            //        }
            //    };
            //    dataContext.Products.AddRange(product);
            //    dataContext.SaveChanges();
            //}


            if (!dataContext.ProductCategories.Any())
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
                            Brand = new Brand()
                            {
                                BrandName = "Schecter",
                                BrandDescription = "This is brand called schecter"
                            }
                        },
                        Category = new Category()
                        {
                            CategoryName = "Electric guitars",
                            CategoryDescription = "Electric guitars are using pickups"
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
                            Brand = new Brand()
                            {
                                BrandName = "Epiphone",
                                BrandDescription = "This is brand called epiphone"
                            }
                        },
                        Category = new Category()
                        {
                            CategoryName = "Vintage",
                            CategoryDescription = "Vintage guitars"
                        }
                    },
                    new ProductCategory()
                    {
                        Product = new Product()
                        {
                            ProductName = "Cort x6",
                            Description = "This is cort guitar",
                            Price = 250,
                            Stock = 10,
                            ProductImageUrl = "",
                            Brand = new Brand()
                            {
                                BrandName = "Cort",
                                BrandDescription = "This is brand called cort"
                            }
                        },
                        Category = new Category()
                        {
                            CategoryName = "Heavy",
                            CategoryDescription = "Heavy guitars"
                        }
                    }
                };
                dataContext.ProductCategories.AddRange(productCategory);
                dataContext.SaveChanges();
            }
        }
    }
}
