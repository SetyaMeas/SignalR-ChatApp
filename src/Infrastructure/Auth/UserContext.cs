using System.Security.Claims;
using ChatApp.Application.Commons.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Infrastucture.Auth
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int Id => extractUserId();

        private int extractUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(
                ClaimTypes.NameIdentifier
            );
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("Empty NameIdentifier detected");
            }

            if (!int.TryParse(userId, out int id))
            {
                throw new UnauthorizedAccessException("Invalid NameIdentifier detected");
            }
            return id;
        }
    }
}
