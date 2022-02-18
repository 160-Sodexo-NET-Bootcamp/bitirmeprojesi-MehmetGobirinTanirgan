using FluentValidation;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.OfferDtos.Request;

namespace PCS.Validation.Validators.OfferTypeValidators
{
    public class OfferCreateDtoValidator : AbstractValidator<OfferCreateDto>
    {
        public OfferCreateDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError);

            RuleFor(x => x.OfferPercentage)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
                .LessThan(100);
        }
    }
}
