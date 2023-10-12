using MyApp.DataAccess.Abstractions.CountryApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.MyDomain.Providers.Country.Abstractions
{
    public interface ICountryDbProvider
    {
        Task<List<CountryContract>?> GetCountriesAsync();
        Task PostCountries(List<CountryContract> countries);
    }
}
