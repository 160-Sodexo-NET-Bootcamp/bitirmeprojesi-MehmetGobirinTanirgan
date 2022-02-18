using FluentValidation;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.CategoryDtos.Request;

namespace PCS.Validation.Validators.CategoryTypeValidators
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError);

            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
                .MaximumLength(50).WithMessage(ValidationErrorMessages.MaxLengthError);

            RuleFor(x => x.CategoryLevel)
                .NotNull().WithMessage(ValidationErrorMessages.EmptyError);

            RuleFor(x => x.LeftBorder)
                .NotNull().WithMessage(ValidationErrorMessages.EmptyError);

            RuleFor(x => x.RightBorder)
                .NotNull().WithMessage(ValidationErrorMessages.EmptyError);
        }
    }
}
