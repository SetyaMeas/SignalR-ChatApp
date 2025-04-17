using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace ChatApp.Infrastucture.Cache
{
    public class CachingRepository : ICachingRepository
    {
        private readonly IDistributedCache _distributedCached;

        public CachingRepository(IDistributedCache distributedCache)
        {
            _distributedCached = distributedCache;
        }

        public async Task<T?> GetByKeyAsync<T>(
            string key,
            CancellationToken cancellationToken = default
        )
        {
            string? value = await _distributedCached.GetStringAsync(key, cancellationToken);
            if (string.IsNullOrEmpty(value))
            {
                return default(T?); // return null for nullable type
            }

            T? result = JsonConvert.DeserializeObject<T>(value);
            return result;
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken)
        {
            await _distributedCached.RemoveAsync(key, cancellationToken);
        }

        public async Task SetAsync<T>(
            string key,
            T value,
            TimeSpan expiration,
            CancellationToken cancellationToken = default
        )
        {
            var option = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration,
            };
            await _distributedCached.SetStringAsync(
                key,
                JsonConvert.SerializeObject(value),
                option,
                cancellationToken
            );
        }
    }
}
