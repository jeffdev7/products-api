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
        private bool _disposed;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public ErrorOr<ProductViewModel> Add(AddProductViewModel vm)
        {
            List<Error> validationErrors = new List<Error>();
            var error = new AddProductValidator().Validate(vm);

            if (!error.IsValid)
            {
                validationErrors = error.Errors
                    .Select(e => Error.Validation(e.PropertyName, e.ErrorMessage))
                    .ToList();
                return ErrorOr<ProductViewModel>.From(validationErrors);
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
        public Task<ErrorOr<ProductViewModel>> Update(ProductViewModel vm)
        {
            List<Error> validationErrors = new List<Error>();
            var error = new UpdateProductValidator().Validate(vm);

            if (!error.IsValid)
            {
                validationErrors = error.Errors
                           .Select(e => Error.Validation(e.PropertyName, e.ErrorMessage))
                           .ToList();
                return Task.FromResult<ErrorOr<ProductViewModel>>(validationErrors);
            }

            var entity = _mapper.Map<Product>(vm);
            _productRepository.Update(entity);

            return Task.FromResult<ErrorOr<ProductViewModel>>(_mapper.Map<ProductViewModel>(entity));
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_productRepository is IDisposable disposableRepository)
                    {
                        disposableRepository.Dispose();
                    }
                    if (_mapper is IDisposable disposableMapper)
                    {
                        disposableMapper.Dispose();
                    }
                }
                _disposed = true;
            }
        }

    }
}
