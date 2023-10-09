using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.DataAccess.Abstractions.MyDomain.Entities;
using MyApp.Domain.MyDomain.Dto;

namespace MyApp.Domain.MyDomain.Repositories.Abstractions
{
    public interface ICountryRepository
    {
        Task<List<CountryContract>> GetCountriesFromDbAsync();
        Task<bool> PostCountries(List<CountryContract> countries);
    }
}
