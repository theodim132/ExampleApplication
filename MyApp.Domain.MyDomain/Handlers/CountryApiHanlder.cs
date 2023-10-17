using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Handler.Abstractions;
using MyApp.Domain.MyDomain.Handlers.Abstractions;
using MyApp.Domain.MyDomain.Providers.Country.Abstractions;
using Viva;

namespace MyApp.Domain.MyDomain.Handler
{
    public class CountryApiHanlder : Handler<object> , ICountriesFromApi
    {
        private readonly ICountryApiProvider apiProvider;
        private readonly ICountryDbProvider countryDbProvider;

        public CountryApiHanlder(ICountryApiProvider apiProvider, ICountryDbProvider countryDbProvider)
        {
            this.apiProvider = apiProvider;
            this.countryDbProvider = countryDbProvider;
        }
        public override async Task<IResult<List<CountryContract>>> Handle(object request)
        {
            var result = await apiProvider.GetCountriesAsync();
            if (result.Success)
            {
                await countryDbProvider.PostCountries(result.Data);
                return result;
            }

            return await base.Handle(request);
        }
    }
}
