using ExampleApplication.Models;
using ExampleApplication.Models.Dto;

namespace ExampleApplication.Repository
{
    public interface ICountryRepository
    {
        Task Delete(int? id);
        Task<ResponseDto> DeleteAllAsync();
        Task<List<Country>> GetAll();
        Task<ResponseDto> PostCountries(List<CountryDto> countries);
    }
}
