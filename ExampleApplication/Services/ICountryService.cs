using ExampleApplication.Models;
using ExampleApplication.Models.Dto;

namespace ExampleApplication.Services
{
    public interface ICountryService
    {
        Task<ResponseDto?> GetAllCountriesAsync();
        Task<List<Country>?> GetAllCountriesFromDbAsync();
        void PostCountries(List<CountryDto> countries);
    }
}
