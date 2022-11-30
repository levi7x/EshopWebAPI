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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
