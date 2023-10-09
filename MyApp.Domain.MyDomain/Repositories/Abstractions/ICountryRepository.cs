

using MyApp.DataAccess.Abstractions.MyDomain.Entities;
using MyApp.Domain.MyDomain.Dto;

namespace MyApp.Domain.MyDomain.Repositories.Abstractions
{
    public interface ICountryRepository
    {
        Task Delete(int? id);
        Task<ResponseDto> DeleteAllAsync();
        Task<List<Model.Country>> GetAll();
        Task<ResponseDto> PostCountries(List<CountryDto> countries);
    }
}
