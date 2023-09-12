using ExampleApplication.Models;
using ExampleApplication.Models.Dto;

namespace ExampleApplication.Services
{
    public interface ICountryService
    {
        Task<ResponseDto> GetAllCountriesAsync();
        Task<ResponseDto> GetAllCountriesFromDbAsync();
        Task<ResponseDto> PostCountries(List<CountryDto> countries);
        Task<ResponseDto> DeleteCountriesAsync();
    }
}
