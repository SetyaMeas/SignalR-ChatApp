using ChatApp.Application.Commons.DTOs;

namespace ChatApp.Application.Commons.Interfaces
{
    /* purpose: manage caching for register process */
    public interface IRegisterCaching
    {
        Task<RegisterCachingDTO?> GetBykeyAsync(
            Guid guid,
            CancellationToken cancellationToken = default
        );
        Task SetAsync(
            Guid guid,
            RegisterCachingDTO value,
            CancellationToken cancellationToken = default
        );
        Task RemoveAsync(Guid guid, CancellationToken cancellationToken = default);
    }
}
