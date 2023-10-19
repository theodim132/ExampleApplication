using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Handler.Abstractions;
using MyApp.Domain.MyDomain.Handlers.Abstractions;
using MyApp.Domain.MyDomain.Providers.Country.Abstractions;
using Viva;

namespace MyApp.Domain.MyDomain.Handler
{
    public class CountryCacheHandler : Handler , ICountryCacheHandler
    {
        private readonly ICountryCacheProvider cacheProvider;

        public CountryCacheHandler(ICountryCacheProvider cacheProvider) =>
            this.cacheProvider = cacheProvider;

        public override async Task<IResult<List<CountryContract>>> Handle()
        {
            var result = cacheProvider.GetCountries();
            if (result.Success)
                return result;

            return await base.Handle();
        }
    }
}
