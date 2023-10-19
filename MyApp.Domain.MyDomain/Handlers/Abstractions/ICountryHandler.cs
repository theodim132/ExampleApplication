using MyApp.DataAccess.Abstractions.CountryApi;
using Viva;

namespace MyApp.Domain.MyDomain.Handler.Abstractions
{
    public interface ICountryHandler
    {
        ICountryHandler SetNext(ICountryHandler handler);
        Task<IResult<List<CountryContract>>> HandleAsync();
    }
}
