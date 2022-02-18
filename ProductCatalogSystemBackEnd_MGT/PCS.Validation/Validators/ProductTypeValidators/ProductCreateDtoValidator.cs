using FluentValidation;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.ProductDtos.Request;

namespace PCS.Validation.Validators.ProductTypeValidators
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
                .MaximumLength(100).WithMessage(ValidationErrorMessages.MaxLengthError);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
                .MaximumLength(500).WithMessage(ValidationErrorMessages.MaxLengthError);

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
                .ScalePrecision(2, 8).WithMessage(ValidationErrorMessages.PropertyNotValidError);

            RuleFor(x => x.UsageCondition)
                .NotEmpty().WithMessage(ValidationErrorMessages.PropertyNotValidError);

            RuleFor(x => x.IsOfferable)
                .NotNull().WithMessage(ValidationErrorMessages.EmptyError);

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError);

            RuleFor(x => x.ImageFiles)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError);
        }
    }
}
