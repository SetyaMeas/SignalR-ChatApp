using System.Security.Claims;

namespace ChatApp.Application.Commons.DTOs
{
    public class TokenCredential
    {
        public required int Id { get; init; }
        public required string Email { get; init; }
        /* public DateTime ExpiredAt { get; } = DateTime.UtcNow.AddMinutes(3); */

        public ClaimsIdentity ToClaims()
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, Id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Email, Email));
            return claims;
        }
    }
}
