namespace ChatApp.Infrastucture.Cache
{
    public interface ICachingRepository
    {
        Task<T?> GetByKeyAsync<T>(string key, CancellationToken cancellationToken = default);
        Task SetAsync<T>(
            string key,
            T value,
            TimeSpan expiration,
            CancellationToken cancellationToken = default
        );
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    }
}
