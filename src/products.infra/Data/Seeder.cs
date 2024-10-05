using Microsoft.Extensions.DependencyInjection;
using products.domain.Entities;

namespace products.infra.Data
{
    public class Seeder
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
                        Product.Create("Smartphone", 699.99M, 50),
                        Product.Create("Laptop", 1200.50M, 30),
                        Product.Create("Tablet", 350.75M, 100),
                        Product.Create("Headphones", 89.99M, 20),
                        Product.Create("Smartwatch", 199.99M, 1),
                        Product.Create("Bluetooth Speaker", 59m , 43),
                        Product.Create("Monitor", 249.50M, 80),
                        Product.Create("Keyboard", 49.99M, 400),
                        Product.Create("Mouse", 29.99M, 500),
                        Product.Create("External Hard Drive", 9, 87)

                    };

                    context.Products.AddRange(products);
                    context.SaveChanges();
                }
            }
        }
    }

}
