using ChatApp.Application.Commons.Interfaces;
using ChatApp.Domain.Enums;
using CQRS;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Application.Features.Commands.Register
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICookieService _cookieService;

        public RegisterCommandHandler(
            IApplicationDbContext applicationDbContext,
            ICookieService cookieService
        )
        {
            _applicationDbContext = applicationDbContext;
            _cookieService = cookieService;
        }

        public async Task<bool> HandleAsync(
            RegisterCommand command,
            CancellationToken cancellationToken = default
        )
        {
            _cookieService.Append(
                CookieEnum.ACCESS_TOKEN,
                "this is access token sir",
                DateTime.UtcNow.AddMinutes(2)
            );
            return true;
        }
    }
}
