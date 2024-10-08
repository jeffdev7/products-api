using products.domain.Entities;

namespace products.domain.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> GetProducts();
        Task<bool> RemoveProductAsync(string Id);
    }
}
