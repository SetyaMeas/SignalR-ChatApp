using CQRS;

namespace ChatApp.Application.Features.Commands.RegisterVerification
{
    public class RegisterVerificationCmd : ICommand<bool>
    {
        public string VerificationCode { get; init; } = default!;
    }
}
