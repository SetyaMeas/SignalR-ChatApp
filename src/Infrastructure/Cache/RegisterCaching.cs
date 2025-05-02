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
            string key,
            CancellationToken cancellationToken = default
        )
        {
            var value = await _cachingRepo.GetByKeyAsync<RegisterCachingDTO>(
                $"register-{key}",
                cancellationToken
            );
            return value;
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _cachingRepo.RemoveAsync($"register-{key}", cancellationToken);
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
