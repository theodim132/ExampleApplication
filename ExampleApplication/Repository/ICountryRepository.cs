using ExampleApplication.Models;
using ExampleApplication.Models.Dto;

namespace ExampleApplication.Repository
{
    public interface ICountryRepository
    {
        Task Delete(int? id);
        Task<ResponseDto> DeleteAllAsync();
        IQueryable<Country> GetAll();
        Task PostCountries(List<CountryDto?> countries);
    }
}
