using products.application.ViewModel;

namespace products.application.Services.Interface
{
    public interface IProductService : IDisposable
    {
        Task<IEnumerable<ProductViewModel>> GetAll();
        ProductViewModel GetById(string id);
        Task<ProductViewModel> Update(ProductViewModel vm);
        Task<ProductViewModel> Add(ProductViewModel vm);
        Task<bool> Remove(string id);
    }
}
