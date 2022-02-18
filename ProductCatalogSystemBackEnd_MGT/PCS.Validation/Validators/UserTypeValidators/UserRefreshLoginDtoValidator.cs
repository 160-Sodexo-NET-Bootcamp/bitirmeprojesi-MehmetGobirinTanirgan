using FluentValidation;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.UserDtos.Request;

namespace PCS.Validation.Validators.UserTypeValidators
{
    public class UserRefreshLoginDtoValidator : AbstractValidator<UserRefreshLoginDto>
    {
        public UserRefreshLoginDtoValidator()
        {
            RuleFor(x => x.EmailAddress)
            .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
            .MaximumLength(100).WithMessage(ValidationErrorMessages.MaxLengthError)
            .EmailAddress().WithMessage(ValidationErrorMessages.EmailError);

            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError);
        }
    }
}
