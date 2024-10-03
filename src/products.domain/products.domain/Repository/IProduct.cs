using products.domain.Entities;

namespace products.domain.Repository
{
    public interface IProduct : IRepository<Product>
    {
        IQueryable<Product> GetProducts();
    }
}
