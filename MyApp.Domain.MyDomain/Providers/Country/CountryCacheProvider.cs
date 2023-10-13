using MyApp.Constants.MyDomain;
using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Providers.Country.Abstractions;
using Viva;

namespace MyApp.Domain.MyDomain.Providers.Country
{
    public class CountryCacheProvider : ICountryCacheProvider
    {
        private readonly ICacheService cache;

        public CountryCacheProvider(ICacheService cache) =>
            this.cache = cache;

        public IResult<List<CountryContract>?> GetCountries()
        {
            return cache.Get<List<CountryContract>?>(CacheKeys.Countries);
        }

        public void SetCountries( List<CountryContract>? countries)
        {
            cache.SetItem(CacheKeys.Countries, countries, TimeSpan.FromSeconds(10));
        }
    }
}
