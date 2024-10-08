using FluentValidation;
using products.crosscutting.ViewModel;

namespace products.crosscutting.Validation
{
    public class ProductValidator : AbstractValidator<AddProductViewModel>
    {
        public ProductValidator()
        {
            RuleFor(_ => _.Name).NotEmpty().Length(3, 20);
            RuleFor(_ => _.Price).GreaterThan(0).NotEmpty();
            RuleFor(_ => _.Stock).GreaterThan(0).NotEmpty();
        }
    }
}
