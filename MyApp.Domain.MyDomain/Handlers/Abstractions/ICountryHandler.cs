using MyApp.DataAccess.Abstractions.CountryApi;
using Viva;

namespace MyApp.Domain.MyDomain.Handler.Abstractions
{
    public interface ICountryHandler<TRequest>
    {
        ICountryHandler<TRequest> SetNext(ICountryHandler<TRequest> handler);
        Task<IResult<List<CountryContract>>> Handle(TRequest request);
    }
}
