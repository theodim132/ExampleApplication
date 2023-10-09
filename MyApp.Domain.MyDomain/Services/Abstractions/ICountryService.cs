

using MyApp.DataAccess.Abstractions.Dto;

namespace MyApp.Domain.MyDomain.Services.Abstractions
{
    public interface ICountryService
    {
        Task<ResponseDto> GetAllCountriesAsync();
        Task<ResponseDto> GetAllCountriesFromDbAsync();
        Task<ResponseDto> PostCountries(List<MyApp.Domain.MyDomain.Dto.CountryDto> countries);
        Task<ResponseDto> DeleteCountriesAsync();
    }
}
