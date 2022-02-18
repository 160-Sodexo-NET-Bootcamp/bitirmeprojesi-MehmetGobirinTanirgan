using FluentValidation;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.UserDtos.Request;
using System.Text.RegularExpressions;

namespace PCS.Validation.Validators.UserTypeValidators
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.Firstname)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
                .MaximumLength(50).WithMessage(ValidationErrorMessages.MaxLengthError);

            RuleFor(x => x.Lastname)
               .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
               .MaximumLength(50).WithMessage(ValidationErrorMessages.MaxLengthError);

            RuleFor(x => x.EmailAddress)
               .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
               .MaximumLength(100).WithMessage(ValidationErrorMessages.MaxLengthError)
               .EmailAddress().WithMessage(ValidationErrorMessages.EmailError);

            RuleFor(x => x.PhoneNumber)
               .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
               .Matches(new Regex(@"^((\d{3})(\d{3})(\d{2})(\d{2}))$")).WithMessage(ValidationErrorMessages.PropertyNotValidError);

            RuleFor(x => x.Password)
              .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
              .Length(8, 20).WithMessage(ValidationErrorMessages.MaxMinLengthError);

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage(ValidationErrorMessages.EmptyError)
                .MaximumLength(30).WithMessage(ValidationErrorMessages.MaxLengthError);
        }
    }
}
