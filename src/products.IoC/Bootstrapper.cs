using Microsoft.Extensions.DependencyInjection;
using products.application.Services;
using products.application.Services.Interface;
using products.domain.Repository;
using products.infra.Repositories;

namespace products.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection service)
        {
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<IProductService, ProductService>();

        }
    }
}
