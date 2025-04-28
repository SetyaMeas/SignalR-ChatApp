using ChatApp.Application.Commons.DTOs;

namespace ChatApp.Application.Commons.Interfaces
{
    public interface IJwtProvider
    {
        string CreateToken(TokenCredential tokenCredential);
    }
}
