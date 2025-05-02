using ChatApp.Application.Commons.DTOs;

namespace ChatApp.Application.Commons.Interfaces
{
    /* purpose: manage caching for register process */
    public interface IRegisterCaching
    {
        Task<RegisterCachingDTO?> GetBykeyAsync(
            string key,
            CancellationToken cancellationToken = default
        );
        Task SetAsync(
            Guid guid,
            RegisterCachingDTO value,
            CancellationToken cancellationToken = default
        );
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    }
}
