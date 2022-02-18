using FluentValidation;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.UserDtos.Request;

namespace PCS.Validation.Validators.UserTypeValidators
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.EmailAddress)
              .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
              .MaximumLength(100).WithMessage(ValidationErrorMessages.MaxLengthError)
              .EmailAddress().WithMessage(ValidationErrorMessages.EmailError);

            RuleFor(x => x.Password)
              .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
              .Length(8, 20).WithMessage(ValidationErrorMessages.MaxMinLengthError);
        }
    }
}
