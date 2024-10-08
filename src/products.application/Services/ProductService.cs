using AutoMapper;
using ErrorOr;
using products.application.Services.Interface;
using products.crosscutting.Validation;
using products.crosscutting.ViewModel;
using products.domain.Entities;
using products.domain.Repository;

namespace products.application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public ErrorOr<ProductViewModel> Add(AddProductViewModel vm)
        {
            List<Error> validationErrors = new List<Error>();
            var error = new ProductValidator().Validate(vm);
            if (!error.IsValid)
            {
                validationErrors = error.Errors.Select(_ => _.ErrorMessage).ToErrorOr().Errors;
                return Error.Custom(422, "422", error.ToString());
            }

            var entity = _mapper.Map<Product>(vm);
            _productRepository.Add(entity);
            return _mapper.Map<ProductViewModel>(entity);
        }
        public Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return Task.FromResult(_mapper.Map<IEnumerable<ProductViewModel>>(_productRepository.GetProducts()));
        }

        public ProductViewModel GetById(string id)
        {
            return _mapper.Map<ProductViewModel>(_productRepository.GetById(id));
        }

        public async Task<bool> Remove(string id)
        {
            return await _productRepository.RemoveProductAsync(id);
        }

        public Task<ProductViewModel> Update(ProductViewModel vm)
        {
            //validate if a product exists to updates
            var entity = _mapper.Map<Product>(vm);
            _productRepository.Update(entity);
            return Task.FromResult(_mapper.Map<ProductViewModel>(entity));
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
