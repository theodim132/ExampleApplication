using MyApp.DataAccess.Abstractions.CountryApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viva;

namespace MyApp.Domain.MyDomain.Handler.Abstractions
{
    public interface ICountryHandler<TRequest>
    {
        ICountryHandler<TRequest> SetNext(ICountryHandler<TRequest> handler);
        Task<IResult<List<CountryContract>>> Handle(TRequest request);
    }
}
