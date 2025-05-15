using ChatApp.Application.Commons.DTOs;
using ChatApp.Application.Commons.Interfaces;
using ChatApp.Domain.Enums;
using CQRS;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Application.Features.Commands.Login
{
    public class LoginCmdHandler : ICommandHandler<LoginCmd, bool>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICookieService _cookieService;
        private readonly IApplicationDbContext _appDbContext;

        public LoginCmdHandler(
            IJwtProvider jwtProvider,
            IPasswordHasher passwordHasher,
            ICookieService cookieService,
            IApplicationDbContext applicationDbContext
        )
        {
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
            _cookieService = cookieService;
            _appDbContext = applicationDbContext;
        }

        public async Task<bool> HandleAsync(
            LoginCmd command,
            CancellationToken cancellationToken = default
        )
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(
                u => u.Email == command.Email,
                cancellationToken
            );
            if (user is null)
            {
                return false;
            }

            if (!_passwordHasher.Compare(command.Password, user.Password, user.Salt))
            {
                return false;
            }

            var credential = new TokenCredential { Email = command.Email, Id = user.Id };
            string token = _jwtProvider.CreateToken(credential);

            _cookieService.Append(
                CookieEnum.ACCESS_TOKEN,
                token,
                credential.ExpiredAt.AddMinutes(-2)
            );
            return true;
        }
    }
}
