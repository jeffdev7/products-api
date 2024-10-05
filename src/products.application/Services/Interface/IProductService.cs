using products.application.ViewModel;

namespace products.application.Services.Interface
{
    public interface IProductService : IDisposable
    {
        Task<IEnumerable<ProductViewModel>> GetAll();
        ProductViewModel GetById(string id);
        Task<ProductViewModel> Update(ProductViewModel vm);
        ProductViewModel Add(AddProductViewModel vm);
        Task<bool> Remove(string id);
    }
}
