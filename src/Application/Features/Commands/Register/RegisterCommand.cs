using CQRS;

namespace ChatApp.Application.Features.Commands.Register
{
    public class RegisterCommand : ICommand<bool>
    {
        public string Username { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string Password { get; init; } = default!;
    }
}
