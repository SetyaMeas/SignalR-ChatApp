using ChatApp.Domain.Enums;

namespace ChatApp.Application.Commons.Interfaces
{
    /* purpose: manage http-only cookie for a whole application */
    public interface ICookieService
    {
        void Append(CookieEnum key, string value, DateTime expiredAt);
        void Delete(CookieEnum key);
    }
}
