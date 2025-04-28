using ChatApp.Application.Commons.DTOs;
using ChatApp.Application.Commons.Exceptions;
using ChatApp.Application.Commons.Interfaces;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Events;
using CQRS;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Application.Features.Commands.Register
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICookieService _cookieService;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IRegisterCaching _registerCaching;
        private readonly IVerifyCodeGenerator _verifyCodeGenerator;

        public RegisterCommandHandler(
            IApplicationDbContext applicationDbContext,
            ICookieService cookieService,
            IPasswordHasher passwordHasher,
            IEventDispatcher eventDispatcher,
            IRegisterCaching registerCaching,
            IVerifyCodeGenerator verifyCodeGenerator
        )
        {
            _applicationDbContext = applicationDbContext;
            _cookieService = cookieService;
            _passwordHasher = passwordHasher;
            _eventDispatcher = eventDispatcher;
            _registerCaching = registerCaching;
            _verifyCodeGenerator = verifyCodeGenerator;
        }

        public async Task<bool> HandleAsync(
            RegisterCommand command,
            CancellationToken cancellationToken = default
        )
        {
            bool isExistedEmail = await _applicationDbContext.Users.AnyAsync(u =>
                u.Email == command.Email
            );
            if (isExistedEmail)
            {
                throw new DuplicateEmailException("Email already existed");
            }

            string verifyCode = _verifyCodeGenerator.Generate();
            System.Console.WriteLine("Your verification code is: " + verifyCode);
            /* await _eventDispatcher.DispatchAsync( */
            /*     new SendMailEvent */
            /*     { */
            /*         To = command.Email, */
            /*         Subject = "Verification Code", */
            /*         Body = $"Your verification code is {verifyCode} and will be avialable for 5 minutes", */
            /*     }, */
            /*     cancellationToken */
            /* ); */

            byte[] salt = _passwordHasher.GenerateSalt();
            string hashedPwd = _passwordHasher.Hash(command.Password, salt);
            Guid cacheId = Guid.NewGuid();

            await _registerCaching.SetAsync(
                cacheId,
                new RegisterCachingDTO
                {
                    Password = hashedPwd,
                    Salt = salt,
                    Email = command.Email,
                    VerificationCode = verifyCode,
                }
            );

            _cookieService.Append(
                CookieEnum.REGISTER_ID,
                cacheId.ToString(),
                DateTime.UtcNow.AddMinutes(5)
            );
            return true;
        }
    }
}
