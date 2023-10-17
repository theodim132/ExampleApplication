

using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.DataAccess.Abstractions.Dto;
using MyApp.DataAccess.Abstractions.MyDomain.Entities;
using Viva;

namespace MyApp.Domain.MyDomain.Services.Abstractions
{
    public interface ICountryService
    {
        Task<IResult<List<CountryContract>>> GetAllCountriesAsync();
        Task<IResult<CountryContract>> GetCountryByIdAsync(int id);


    }
}
