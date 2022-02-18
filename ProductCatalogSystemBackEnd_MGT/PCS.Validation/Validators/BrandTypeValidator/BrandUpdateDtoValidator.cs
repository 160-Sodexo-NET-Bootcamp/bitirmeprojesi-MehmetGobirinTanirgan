using FluentValidation;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Models;

namespace PCS.Validation.Validators.BrandTypeValidator
{
    public class BrandUpdateDtoValidator : AbstractValidator<Brand>
    {
        public BrandUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError);

            RuleFor(x => x.BrandName)
               .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
              .MaximumLength(100).WithMessage(ValidationErrorMessages.MaxLengthError);
        }
    }
}
