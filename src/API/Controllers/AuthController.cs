using ChatApp.API.Commons;
using ChatApp.API.Filters;
using ChatApp.Application.Commons.Exceptions;
using ChatApp.Application.Features.Commands.Login;
using ChatApp.Application.Features.Commands.Register;
using ChatApp.Application.Features.Commands.RegisterVerification;
using CQRS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public AuthController(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher
        )
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> Register(
            [FromBody] RegisterCommand cmd,
            CancellationToken ct
        )
        {
            try
            {
                var res = await _commandDispatcher.DispatchAsync(cmd, ct);
                return Ok();
            }
            catch (DuplicateEmailException ex)
            {
                ApiErrorResponse response = new(HttpContext)
                {
                    Title = "Duplicate Email",
                    Status = StatusCodes.Status409Conflict,
                    Type = "about:blank",
                    Detail = ex.Message,
                };
                return Conflict(response);
            }
        }

        [HttpPost("register-verification")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> RegisterVerification(
            [FromBody] RegisterVerificationCmd cmd
        )
        {
            try
            {
                bool res = await _commandDispatcher.DispatchAsync(cmd);
                if (!res)
                {
                    return Unauthorized(
                        new ApiErrorResponse(HttpContext)
                        {
                            Type = "about:blank",
                            Title = "Unauthorized",
                            Status = StatusCodes.Status401Unauthorized,
                            Detail = "Verification Code doesn't match",
                        }
                    );
                }
                return Ok();
            }
            catch (DuplicateEmailException ex)
            {
                return Conflict(
                    new ApiErrorResponse(HttpContext)
                    {
                        Type = "about:blank",
                        Title = "Duplicate Email",
                        Status = StatusCodes.Status409Conflict,
                        Detail = ex.Message,
                    }
                );
            }
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> Login([FromBody] LoginCmd cmd, CancellationToken ct)
        {
            // TODO: test this before pushing
            bool res = await _commandDispatcher.DispatchAsync(cmd, ct);
            if (!res)
            {
                return Unauthorized(
                    new ApiErrorResponse(HttpContext)
                    {
                        Type = "about:blank",
                        Title = "Unauthorized",
                        Status = StatusCodes.Status401Unauthorized,
                        Detail = "Invalid email or password",
                    }
                );
            }
            return Ok();
        }
    }
}
