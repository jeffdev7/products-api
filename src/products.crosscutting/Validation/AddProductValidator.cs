using FluentValidation;
using products.crosscutting.ViewModel;

namespace products.crosscutting.Validation
{
    public class AddProductValidator : AbstractValidator<AddProductViewModel>
    {
        public AddProductValidator()
        {
            RuleFor(_ => _.Name).NotEmpty().Length(3, 25);
            RuleFor(_ => _.Price).GreaterThan(0);
            RuleFor(_ => _.Stock).GreaterThan(0);
        }
    }
}
