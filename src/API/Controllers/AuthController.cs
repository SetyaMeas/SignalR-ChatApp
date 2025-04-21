using ChatApp.API.Filters;
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
        private readonly ICommandDispatcher commandDispatcher;

        public AuthController(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> Testing([FromBody] RegisterCommand cmd)
        {
            /* var me = await commandDispatcher.DispatchAsync(cmd); */
            return Ok(cmd);
        }
    }
}
