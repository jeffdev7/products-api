using AutoMapper;
using products.application.ViewModel;
using products.domain.Entities;

namespace products.application.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, AddProductViewModel>();
            CreateMap<AddProductViewModel, Product>();
        }
    }
}
