using EshopWebAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EshopWebAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
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

            base.OnModelCreating(modelBuilder);

        }
    }
}
