using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyApp.Constants.MyDomain;
using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Providers.Country.Abstractions;
using MyApp.Domain.MyDomain.Services.Abstractions;
using Viva;

namespace MyApp.Domain.MyDomain.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryApiProvider countryApiProvider;
        private readonly ICountryDbProvider countryDbProvider;
        private readonly ICountryCacheProvider cacheProvider;
        private readonly ILogger logger;

        public CountryService(ICountryApiProvider countryApiProvider, ICountryDbProvider countryDbProvider, ICountryCacheProvider cacheProvider, ILogger<CountryService> logger)
        {
            this.countryApiProvider = countryApiProvider;
            this.cacheProvider = cacheProvider;
            this.logger = logger;
            this.countryDbProvider = countryDbProvider;
        }
        public async Task<IResult<List<CountryContract>>> GetAllCountriesAsync()
        {
            try
            {
                logger.LogInformation("CountryService.GetAllCountriesAsync called",
                    DateTime.UtcNow.ToLongTimeString());

                var countriesFromCache = cacheProvider.GetCountries(CacheKeys.Countries);
                if (countriesFromCache.Success && countriesFromCache.Data is not null)
                    return Result<List<CountryContract>>.CreateSuccessful(countriesFromCache.Data);

                var countriesFromDb = await countryDbProvider.GetCountriesAsync();
                if (!countriesFromDb.IsNullOrEmpty())
                {
                    cacheProvider.SetCountries(CacheKeys.Countries, countriesFromDb, TimeSpan.FromSeconds(10));
                    return Result<List<CountryContract>>.CreateSuccessful(countriesFromDb);
                }

                var countriesFromApi = await countryApiProvider.GetCountriesAsync();
                if (!countriesFromApi.Success)
                {
                   
                    return Result<List<CountryContract>>.CreateFailed(ResultCode.NotFound, countriesFromApi.ErrorText);
                }

                return Result<List<CountryContract>>.CreateSuccessful(countriesFromApi.Data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in GetAllCountriesAsync");
                return Result<List<CountryContract>>.CreateFailed(ResultCode.InternalServerError, ex.Message);
            }
        }
    }
}
