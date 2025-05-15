using ChatApp.Application.Commons.Interfaces;
using ChatApp.Domain.Enums;
using CQRS;

namespace ChatApp.Application.Features.Logout
{
    public class LogoutCmdHandler : ICommandHandler<LogoutCmd, bool>
    {
        private readonly ICookieService _cookieService;

        public LogoutCmdHandler(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        public async Task<bool> HandleAsync(
            LogoutCmd command,
            CancellationToken cancellationToken = default
        )
        {
            _cookieService.Delete(CookieEnum.ACCESS_TOKEN);
            return true;
        }
    }
}
