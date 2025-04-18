using ChatApp.Application.Commons.Interfaces;
using CQRS;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Application.Features.Commands.Register
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public RegisterCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> HandleAsync(
            RegisterCommand command,
            CancellationToken cancellationToken = default
        )
        {
            int totalUser = await _applicationDbContext.Users.CountAsync();
            System.Console.WriteLine(totalUser);
            return true;
        }
    }
}
