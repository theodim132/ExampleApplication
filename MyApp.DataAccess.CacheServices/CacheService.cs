
using Microsoft.Extensions.Caching.Memory;
using MyApp.DataAccess.Abstractions.CacheService;
using Viva;

namespace MyApp.DataAccess.CacheServices
{
    public class CacheService : ICacheService
    {

        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache) => _memoryCache = memoryCache;

        public void Delete<T>(string key) =>
            _memoryCache.Remove(key);

        public IResult<T?> Get<T>(string key)
        {
            try
            {
                if (_memoryCache.TryGetValue(key, out T? cachedItem))
                {
                    return Result<T?>.CreateSuccessful(cachedItem);
                }
                return Result<T?>.CreateFailed(ResultCode.NotFound,"Not found in cache");
            }
            catch (Exception ex) 
            {
                return Result<T?>.CreateFailed(ResultCode.InternalServerError,"Get Cache Error");
            }
        }

        public void SetItem<T>(string key, T? item, TimeSpan? expiration = null)
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = expiration ?? TimeSpan.MaxValue
            };

            _memoryCache.Set(key, item, cacheOptions);
        }
    }
}
