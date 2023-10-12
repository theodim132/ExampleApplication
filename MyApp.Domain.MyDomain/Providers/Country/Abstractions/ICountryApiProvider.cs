using MyApp.DataAccess.Abstractions.CountryApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viva;

namespace MyApp.Domain.MyDomain.Providers.Country.Abstractions
{
    public interface ICountryApiProvider
    {
        Task<IResult<List<CountryContract>>> GetCountriesAsync();
    }
}
