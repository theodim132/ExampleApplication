using Viva;

namespace MyApp.DataAccess.Abstractions.CountryApi
{
    public interface ICountryApiService
    {
        Task<IResult<List<CountryContract>>> GetCountriesAsync(List<string> fields);
    }
}
