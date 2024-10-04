using Microsoft.EntityFrameworkCore;
using products.domain.Entities;

namespace products.infra.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("InMemoryDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(Product.Create("Keyboard", 30, 10));
            modelBuilder.Entity<Product>().HasData(Product.Create("Smartphone", 699.99M, 50));
            modelBuilder.Entity<Product>().HasData(Product.Create("Laptop", 1200.50M, 30));
            modelBuilder.Entity<Product>().HasData(Product.Create("Tablet", 350.75M, 100));
            modelBuilder.Entity<Product>().HasData(Product.Create("Headphones", 89.99M, 200));
            modelBuilder.Entity<Product>().HasData(Product.Create("Smartwatch", 199.99M, 150));
            modelBuilder.Entity<Product>().HasData(Product.Create("Bluetooth Speaker", 59.99M, 300));
            modelBuilder.Entity<Product>().HasData(Product.Create("Monitor", 249.50M, 80));
            modelBuilder.Entity<Product>().HasData(Product.Create("Keyboard", 49.99M, 400));
            modelBuilder.Entity<Product>().HasData(Product.Create("Mouse", 29.99M, 500));
            modelBuilder.Entity<Product>().HasData(Product.Create("External Hard Drive", 99.99M, 120));
        }
    }
}
