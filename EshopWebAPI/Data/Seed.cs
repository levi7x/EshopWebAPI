using EshopWebAPI.Models;
using Microsoft.AspNetCore.Identity;
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

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "simomarian99@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        UserName = "levi7x",
                        Email = adminUserEmail,
                        EmailConfirmed = true,

                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC",
                            PostalCode = "03601"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "133 Main St",
                            City = "Charlotte",
                            State = "NC",
                            PostalCode = "03601"
                            
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
