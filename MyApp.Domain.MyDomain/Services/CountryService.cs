using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyApp.Constants.MyDomain;
using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Providers.Abstractions;
using MyApp.Domain.MyDomain.Repositories.Abstractions;
using MyApp.Domain.MyDomain.Services.Abstractions;
using System.Diagnostics.Tracing;
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
                    return Result<List<CountryContract>>.CreateSuccessful(countriesFromDb);

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

        //private IResult<List<CountryContract>?> GetCountriesFromCacheAsync(string key)
        //{
        //    return cache.Get<List<CountryContract>?>(key);
        //}

        //private async Task<List<CountryContract>> GetCountriesFromDbAsync()
        //{
        //    var countriesFromDb = await countryRepo.GetCountriesFromDbAsync();
        //    if (countriesFromDb is not null && countriesFromDb.Any())
        //    {
        //        cache.SetItem(CacheKeys.Countries, countriesFromDb, TimeSpan.FromSeconds(10));
        //        return countriesFromDb;
        //    }

        //    return new List<CountryContract>();
        //}

        //private async Task<IResult<List<CountryContract>>> GetCountriesFromApiAsync()
        //{
        //    var countriesFromApi = await countryApi.GetCountriesAsync(ApiFields.Default);
        //    if (!countriesFromApi.Success)
        //    {
        //        return Result<List<CountryContract>>.CreateFailed(ResultCode.NotFound, "Could not get countries");
        //    }
        //    await countryRepo.PostCountries(countriesFromApi.Data);

        //    return countriesFromApi;
        //}
    }
}
