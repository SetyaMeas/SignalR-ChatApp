using ChatApp.Application.Commons.DTOs;
using ChatApp.Application.Commons.Interfaces;

namespace ChatApp.Infrastucture.Cache
{
    public class RegisterCaching : IRegisterCaching
    {
        private readonly ICachingRepository _cachingRepo;

        public RegisterCaching(ICachingRepository caching)
        {
            _cachingRepo = caching;
        }

        public async Task<RegisterCachingDTO?> GetBykeyAsync(
            Guid guid,
            CancellationToken cancellationToken = default
        )
        {
            string key = $"register-{guid}";
            var value = await _cachingRepo.GetByKeyAsync<RegisterCachingDTO>(
                key,
                cancellationToken
            );
            return value;
        }

        public async Task RemoveAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            string key = $"register-{guid}";
            await _cachingRepo.RemoveAsync(key, cancellationToken);
        }

        public async Task SetAsync(
            Guid guid,
            RegisterCachingDTO value,
            CancellationToken cancellationToken = default
        )
        {
            string key = $"register-{guid}";
            await _cachingRepo.SetAsync<RegisterCachingDTO>(
                key,
                value,
                TimeSpan.FromMinutes(5),
                cancellationToken
            );
        }
    }
}
