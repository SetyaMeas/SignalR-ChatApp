using ChatApp.Application.Commons.Exceptions;
using ChatApp.Application.Commons.Interfaces;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Enums;
using CQRS;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Application.Features.Commands.RegisterVerification
{
    public class RegisterVerificationCmdHandler : ICommandHandler<RegisterVerificationCmd, bool>
    {
        private readonly ICookieService _cookieService;
        private readonly IRegisterCaching _registerCaching;
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IJwtProvider _jwtProvider;

        public RegisterVerificationCmdHandler(
            ICookieService cookieService,
            IRegisterCaching registerCaching,
            IApplicationDbContext applicationDbContext,
            IJwtProvider jwtProvider
        )
        {
            _cookieService = cookieService;
            _registerCaching = registerCaching;
            _applicationDbContext = applicationDbContext;
            _jwtProvider = jwtProvider;
        }

        public async Task<bool> HandleAsync(
            RegisterVerificationCmd command,
            CancellationToken cancellationToken = default
        )
        {
            var cacheId = _cookieService.Get(CookieEnum.REGISTER_ID);
            if (cacheId is null)
            {
                return false;
            }

            var cache = await _registerCaching.GetBykeyAsync(cacheId);
            if (cache is null || command.VerificationCode != cache.VerificationCode)
            {
                return false;
            }

            bool isExistedEmail = await _applicationDbContext.Users.AnyAsync(u =>
                u.Email == cache.Email
            );
            if (isExistedEmail)
            {
                throw new DuplicateEmailException("Email already used");
            }

            User newUser = new User
            {
                Username = cache.Username,
                Email = cache.Email,
                Password = cache.Password,
                Salt = cache.Salt,
                CreatedAt = DateTimeOffset.UtcNow,
            };
            await _applicationDbContext.Users.AddAsync(newUser);
            await _applicationDbContext.SaveChangesAsync();

            await _registerCaching.RemoveAsync(cacheId);
            _cookieService.Delete(CookieEnum.REGISTER_ID);
            return true;
        }
    }
}
