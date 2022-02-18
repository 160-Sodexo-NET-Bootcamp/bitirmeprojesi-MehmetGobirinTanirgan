using FluentValidation;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.UserDtos.Request;

namespace PCS.Validation.Validators.UserTypeValidators
{
    public class UserBuyProductDtoValidator : AbstractValidator<UserBuyProductDto>
    {
        public UserBuyProductDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError);

            RuleFor(x => x.PaymentTypeId)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError);
        }
    }
}
