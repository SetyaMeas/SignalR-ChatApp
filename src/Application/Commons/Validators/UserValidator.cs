using FluentValidation;

namespace ChatApp.Application.Commons.Validators
{
    public static class UserValidator
    {
        public static IRuleBuilderOptions<T, string> UsernameValidator<T>(
            this IRuleBuilder<T, string> rule
        )
        {
            return rule.NotNull()
                .WithMessage("Username cannot be null")
                .NotEmpty()
                .WithMessage("Username cannot be empty")
                .MinimumLength(6)
                .WithMessage("Username must be at least 6 characters")
                .MaximumLength(20)
                .WithMessage("Password must be at most 20 characters");
        }

        public static IRuleBuilderOptions<T, string> PasswordValidator<T>(
            this IRuleBuilder<T, string> rule
        )
        {
            return rule.NotNull()
                .WithMessage("Password cannot be null")
                .NotEmpty()
                .WithMessage("Password cannot be empty")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters")
                .MaximumLength(20)
                .WithMessage("Password must be at most 20 characters");
        }

        public static IRuleBuilderOptions<T, string> EmailValidator<T>(
            this IRuleBuilder<T, string> rule
        )
        {
            const string regex = @"^[A-Za-z0-9]+@[A-Za-z0-9]+(\.com)$";
            return rule.NotNull()
                .WithMessage("Email cannot be null")
                .NotEmpty()
                .WithMessage("Email cannot be empty")
                .MaximumLength(320)
                .WithMessage("Email cannot be more than 320 characters")
                .Matches(regex)
                .WithMessage("Invalid email provided");
        }
    }
}
