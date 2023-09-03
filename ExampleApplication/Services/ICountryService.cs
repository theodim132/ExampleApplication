using ExampleApplication.Models;
using ExampleApplication.Models.Dto;

namespace ExampleApplication.Services
{
    public interface ICountryService
    {
        Task<ResponseDto?> GetAllCountriesAsync();
        Task<List<CountryDto>?> GetAllCountriesFromDbAsync();
        void PostCountries(List<CountryDto> countries);
        Task<ResponseDto?> DeleteCountries();
    }
}
