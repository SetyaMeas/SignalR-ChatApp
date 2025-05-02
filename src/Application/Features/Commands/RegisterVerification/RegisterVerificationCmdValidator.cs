using FluentValidation;

namespace ChatApp.Application.Features.Commands.RegisterVerification
{
    public class RegisterVerificationCmdValidator : AbstractValidator<RegisterVerificationCmd>
    {
        public RegisterVerificationCmdValidator()
        {
            RuleFor(u => u.VerificationCode)
                .NotEmpty()
                .WithMessage("Verification code can't be empty");
        }
    }
}
