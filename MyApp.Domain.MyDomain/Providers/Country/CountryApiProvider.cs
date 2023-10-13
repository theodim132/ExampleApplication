using MyApp.Constants.MyDomain;
using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Providers.Country.Abstractions;
using Viva;

namespace MyApp.Domain.MyDomain.Providers.Country
{
    public class CountryApiProvider : ICountryApiProvider
    {
        private readonly ICountryApiService _countryApi;

        public CountryApiProvider(ICountryApiService countryApi) => _countryApi = countryApi;

        public async Task<IResult<List<CountryContract>>> GetCountriesAsync()
        {
            return await _countryApi.GetCountriesAsync(ApiFields.Default);
        }
    }
}
