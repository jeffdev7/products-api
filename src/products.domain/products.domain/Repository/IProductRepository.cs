using products.domain.Entities;

namespace products.domain.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> GetProducts();
        Product GetProductById(string Id);
        Task<bool> RemoveProductAsync(string Id);
        void UpdateProduct(Product product);
        int Insert();
    }
}
