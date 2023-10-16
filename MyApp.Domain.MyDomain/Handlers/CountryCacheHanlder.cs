using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Handler.Abstractions;
using MyApp.Domain.MyDomain.Providers.Country.Abstractions;
using Viva;

namespace MyApp.Domain.MyDomain.Handler
{
    public class CountryCacheHanlder : Handler<object> , ICountryCacheHanlder
    {
        private readonly ICountryCacheProvider cacheProvider;

        public CountryCacheHanlder(ICountryCacheProvider cacheProvider) =>
            this.cacheProvider = cacheProvider;

        public override async Task<IResult<List<CountryContract>>> Handle(object request)
        {
            var result = cacheProvider.GetCountries();
            if (result.Success)
                return result;

            return await base.Handle(request);
        }
    }
}
