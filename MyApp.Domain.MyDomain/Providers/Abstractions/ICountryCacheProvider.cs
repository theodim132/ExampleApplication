using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.Abstractions.CountryApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viva;

namespace MyApp.Domain.MyDomain.Providers.Abstractions
{
    public interface ICountryCacheProvider
    {
        IResult<List<CountryContract>?> GetCountries(string key);
        void SetCountries(string key, List<CountryContract>? countries, TimeSpan duration);
    }
}
