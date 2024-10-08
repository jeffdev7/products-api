using FluentValidation;
using products.crosscutting.ViewModel;

namespace products.crosscutting.Validation
{
    public class UpdateProductValidator : AbstractValidator<ProductViewModel>
    {
        public UpdateProductValidator()
        {
            RuleFor(_ => _.Id).NotEmpty().NotNull();
            RuleFor(_ => _.Name).NotEmpty().Length(3, 25);
            RuleFor(_ => _.Price).GreaterThan(0);
            RuleFor(_ => _.Stock).GreaterThanOrEqualTo(0);
        }
    }
}
