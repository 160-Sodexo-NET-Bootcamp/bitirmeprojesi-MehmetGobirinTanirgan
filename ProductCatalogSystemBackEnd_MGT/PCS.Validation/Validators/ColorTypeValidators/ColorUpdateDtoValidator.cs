using FluentValidation;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.ColorDtos.Request;

namespace PCS.Validation.Validators.ColorTypeValidators
{
    public class ColorUpdateDtoValidator : AbstractValidator<ColorUpdateDto>
    {
        public ColorUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                  .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
                  .InclusiveBetween(1, int.MaxValue);

            RuleFor(x => x.ColorName)
               .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
               .MaximumLength(50).WithMessage(ValidationErrorMessages.MaxLengthError);
        }
    }
}
