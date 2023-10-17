using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Providers.Country.Abstractions;
using MyApp.Domain.MyDomain.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viva;

namespace MyApp.Domain.MyDomain.Providers.Country
{
    public class CountryDbProvider : ICountryDbProvider
    {
        private readonly ICountryRepository countryRepo;

        public CountryDbProvider(ICountryRepository countryRepo) =>
            this.countryRepo = countryRepo;

        public async Task<List<CountryContract>?> GetCountriesAsync()
        {
            return await countryRepo.GetCountriesFromDbAsync();
        }

        public async  Task<IResult<CountryContract?>> GetCountryByIdAsync(int id)
        {
            var country = await countryRepo.GetCountryByIdAsync(id);
            if (country is null) 
            {
               return  Result<CountryContract>.CreateFailed(ResultCode.NotFound, "");
            }
            return Result<CountryContract?>.CreateSuccessful(country!);
        }

        public async Task PostCountries(List<CountryContract> countries) =>
            await countryRepo.PostCountries(countries);
    }
}
