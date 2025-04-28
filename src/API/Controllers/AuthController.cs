using ChatApp.API.Commons;
using ChatApp.API.Filters;
using ChatApp.Application.Commons.Exceptions;
using ChatApp.Application.Features.Commands.Register;
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

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> Testing(
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
    }
}
