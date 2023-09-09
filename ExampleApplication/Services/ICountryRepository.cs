using ExampleApplication.Models;
using ExampleApplication.Models.Dto;

namespace ExampleApplication.Services
{
    public interface ICountryRepository
    {
        Task Delete(int? id);
        Task DeleteAll();
        IQueryable<Country>  GetAll();
        Task PostCountries(List<CountryDto?> countries);
    }
}
