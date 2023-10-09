namespace MyApp.DataAccess.Abstractions.CacheService
{
    public interface ICacheService
    {
        T? Get<T>(string key);
        void SetItem<T>(string key, T item, TimeSpan? expiration = null);
        void Delete<T>(string key);
    }
}
