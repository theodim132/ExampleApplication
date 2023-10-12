using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Providers.Abstractions;
using MyApp.Domain.MyDomain.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.MyDomain.Providers
{
    public class CountryDbProvider : ICountryDbProvider
    {
        private readonly ICountryRepository _countryRepo;

        public CountryDbProvider(ICountryRepository countryRepo)
        {
            _countryRepo = countryRepo;
        }

        public async Task<List<CountryContract>?> GetCountriesAsync()
        {
            return await _countryRepo.GetCountriesFromDbAsync();
        }

        public async Task PostCountries(List<CountryContract> countries)
        {
            await _countryRepo.PostCountries(countries);
        }
    }
}
