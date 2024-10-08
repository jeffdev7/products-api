using ErrorOr;
using products.crosscutting.ViewModel;

namespace products.application.Services.Interface
{
    public interface IProductService : IDisposable
    {
        Task<IEnumerable<ProductViewModel>> GetAll();
        ProductViewModel GetById(string id);
        Task<ProductViewModel> Update(ProductViewModel vm);
        ErrorOr<ProductViewModel> Add(AddProductViewModel vm);
        Task<bool> Remove(string id);
    }
}
