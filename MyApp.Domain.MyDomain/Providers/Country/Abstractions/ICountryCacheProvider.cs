using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.Abstractions.CountryApi;
using Viva;

namespace MyApp.Domain.MyDomain.Providers.Country.Abstractions
{
    public interface ICountryCacheProvider
    {
        IResult<List<CountryContract>?> GetCountries();
        void SetCountries(List<CountryContract>? countries);
    }
}
