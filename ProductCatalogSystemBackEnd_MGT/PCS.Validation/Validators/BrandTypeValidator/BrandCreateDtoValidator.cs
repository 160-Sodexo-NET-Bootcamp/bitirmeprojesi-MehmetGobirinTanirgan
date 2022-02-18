using FluentValidation;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Models;

namespace PCS.Validation.Validators.BrandTypeValidator
{
    public class BrandCreateDtoValidator : AbstractValidator<Brand>
    {
        public BrandCreateDtoValidator()
        {
            RuleFor(x => x.BrandName)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
               .MaximumLength(100).WithMessage(ValidationErrorMessages.MaxLengthError);
        }
    }
}
