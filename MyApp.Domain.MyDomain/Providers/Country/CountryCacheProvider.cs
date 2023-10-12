using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Providers.Country.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viva;

namespace MyApp.Domain.MyDomain.Providers.Country
{
    public class CountryCacheProvider : ICountryCacheProvider
    {
        private readonly ICacheService cache;

        public CountryCacheProvider(ICacheService cache) =>
            this.cache = cache;

        public IResult<List<CountryContract>?> GetCountries(string key)
        {
            return cache.Get<List<CountryContract>?>(key);
        }

        public void SetCountries(string key, List<CountryContract>? countries, TimeSpan duration)
        {
            cache.SetItem(key, countries, duration);
        }
    }
}
