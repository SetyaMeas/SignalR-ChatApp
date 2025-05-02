using ChatApp.Application.Commons.Interfaces;
using ChatApp.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Infrastucture.Auth
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Append(CookieEnum key, string value, DateTime expiredAt)
        {
            Delete(key);

            _httpContextAccessor.HttpContext?.Response.Cookies.Append(
                key.ToString(),
                value,
                new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true, // Ensure cookie is sent over HTTPS only
                    Expires = expiredAt,
                }
            );
        }

        public string? Get(CookieEnum key)
        {
            if (_httpContextAccessor.HttpContext is not null)
            {
                _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(
                    key.ToString(),
                    out var result
                );
                return result;
            }
            return null;
        }

        public void Delete(CookieEnum key)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(key.ToString());
        }
    }
}
