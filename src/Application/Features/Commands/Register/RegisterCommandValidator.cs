using ChatApp.Application.Commons.Validators;
using FluentValidation;

namespace ChatApp.Application.Features.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(r => r.Username).UsernameValidator();
            RuleFor(r => r.Email).EmailValidator();
            RuleFor(r => r.Password).PasswordValidator();
        }
    }
}
