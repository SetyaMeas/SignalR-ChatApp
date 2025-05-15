using ChatApp.Application.Commons.Validators;
using CQRS;
using FluentValidation;

namespace ChatApp.Application.Features.Commands.Login
{
    public class LoginCmd : ICommand<bool>
    {
        public string Email { get; init; } = default!;
        public string Password { get; init; } = default!;
    }

    public class LoginCmdValidator : AbstractValidator<LoginCmd>
    {
        public LoginCmdValidator()
        {
            RuleFor(l => l.Email).EmailValidator();
            RuleFor(l => l.Password).PasswordValidator();
        }
    }
}
