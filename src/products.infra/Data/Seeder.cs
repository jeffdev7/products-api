using Microsoft.Extensions.DependencyInjection;
using products.domain.Entities;

namespace products.infra.Data
{
    public static class Seeder
    {
        public static void RunSeeder(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                context.Database.EnsureCreated();

                if (!context.Products.Any())
                {
                    var products = new List<Product>
                    {
                        Product.Create("Keyboard", 30, 10),
                        Product.Create("Laptop", 1200.50M, 30),
                        Product.Create("Tablet", 350.75M, 100),
                        Product.Create("Headphones", 89.99M, 20),
                        Product.Create("Monitor", 249.50M, 80),
                        Product.Create("Mouse", 29.99M, 500),
                    };

                    context.Products.AddRange(products);
                    context.SaveChanges();
                }
            }
        }
    }

}
