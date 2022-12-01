using EshopWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EshopWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Address> Adresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get;set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        //customize tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasKey(pc => new { pc.CategoryId, pc.ProductId });
            modelBuilder.Entity<ProductCategory>().HasOne(c => c.Product).WithMany(c => c.ProductCategories).HasForeignKey(pc => pc.ProductId);
            modelBuilder.Entity<ProductCategory>().HasOne(c => c.Category).WithMany(c => c.ProductCategories).HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<OrderDetails>().HasKey(od => new { od.OrderId, od.ProductId });
            modelBuilder.Entity<OrderDetails>().HasOne(o => o.Order).WithMany(o => o.OrderDetails).HasForeignKey(od => od.OrderId);
            modelBuilder.Entity<OrderDetails>().HasOne(o => o.Product).WithMany(o => o.OrderDetails).HasForeignKey(od => od.ProductId);


            //modelBuilder.Entity<Brand>().HasData(
            //    new Brand
            //    {
            //        Id = 1,
            //        BrandName = "Schecter",
            //        BrandDescription = "This is brand called schecter"
            //    },
            //    new Brand
            //    {
            //        Id = 2,
            //        BrandName = "Ephiphone",
            //        BrandDescription = "This is brand called epiphone"
            //    },
            //    new Brand
            //    {
            //        Id = 3,
            //        BrandName = "Cort",
            //        BrandDescription = "This is brand called cort"
            //    }
            //    );


        }
    }
}
