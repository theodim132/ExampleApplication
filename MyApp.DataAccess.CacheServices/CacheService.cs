
using Microsoft.Extensions.Caching.Memory;
using MyApp.DataAccess.Abstractions.CacheService;

namespace MyApp.DataAccess.CacheServices
{
    public class CacheService : ICacheService
    {

        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache) => _memoryCache = memoryCache;

        public void Delete<T>(string key) =>
            _memoryCache.Remove(key);

        public T? Get<T>(string key)
        {
            if (_memoryCache.TryGetValue(key, out T cachedItem))
            {
                return cachedItem;
            }
            return default;
        }

        public void SetItem<T>(string key, T item, TimeSpan? expiration = null)
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = expiration ?? TimeSpan.MaxValue
            };

            _memoryCache.Set(key, item, cacheOptions);
        }
    }
}
