using FluentValidation;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.ColorDtos.Request;

namespace PCS.Validation.Validations.ColorTypeValidators
{
    public class ColorCreateDtoValidator : AbstractValidator<ColorCreateDto>
    {
        public ColorCreateDtoValidator()
        {
            RuleFor(x => x.ColorName)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
                .MaximumLength(50).WithMessage(ValidationErrorMessages.MaxLengthError);
        }
    }
}
