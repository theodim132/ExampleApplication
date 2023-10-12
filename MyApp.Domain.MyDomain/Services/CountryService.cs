using Microsoft.Extensions.Logging;
using MyApp.Constants.MyDomain;
using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Repositories.Abstractions;
using MyApp.Domain.MyDomain.Services.Abstractions;
using Viva;
using Viva.Diagnostics;

namespace MyApp.Domain.MyDomain.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryApiService countryApi;
        private readonly ICountryRepository countryRepo;
        private readonly ICacheService cache;
        private readonly ILogger logger;

        public CountryService(ICountryApiService countryApi, ICountryRepository countryRepo, ICacheService cache, ILogger<CountryService> logger)
        {
            this.countryApi = countryApi;
            this.countryRepo = countryRepo;
            this.cache = cache;
            this.logger = logger;
        }

        public async Task<IResult<List<CountryContract>>> GetAllCountriesAsync()
        {
            try
            {
                logger.LogInformation("CountryService.GetAllCountriesAsync called",
                    DateTime.UtcNow.ToLongTimeString());

                var countriesFromCache = GetCountriesFromCacheAsync(CacheKeys.Countries);
                if (countriesFromCache.Success && countriesFromCache.Data is not null)
                    return Result<List<CountryContract>>.CreateSuccessful(countriesFromCache.Data);

                var countriesFromDb = await GetCountriesFromDbAsync();
                if (countriesFromDb.Any())
                    return Result<List<CountryContract>>.CreateSuccessful(countriesFromDb);

                var countriesFromApi = await GetCountriesFromApiAsync();
                if (!countriesFromApi.Success)
                {
                    return Result<List<CountryContract>>.CreateFailed(ResultCode.NotFound, countriesFromApi.ErrorText);
                }

                return Result<List<CountryContract>>.CreateSuccessful(countriesFromApi.Data);
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured in GetAllCountriesAsync", ex);
                return Result<List<CountryContract>>.CreateFailed(ResultCode.InternalServerError, ex.Message);
            }
        }

        private IResult<List<CountryContract>?> GetCountriesFromCacheAsync(string key)
        {
            return cache.Get<List<CountryContract>?>(key);
        }

        private async Task<List<CountryContract>> GetCountriesFromDbAsync()
        {
            var countriesFromDb = await countryRepo.GetCountriesFromDbAsync();
            if (countriesFromDb is not null && countriesFromDb.Any())
            {
                cache.SetItem(CacheKeys.Countries, countriesFromDb, TimeSpan.FromSeconds(10));
                return countriesFromDb;
            }

            return new List<CountryContract>();
        }

        private async Task<IResult<List<CountryContract>>> GetCountriesFromApiAsync()
        {
            var countriesFromApi = await countryApi.GetCountriesAsync(ApiFields.Default);
            if (!countriesFromApi.Success)
            {
                return Result<List<CountryContract>>.CreateFailed(ResultCode.NotFound, "Could not get countries");
            }
            await countryRepo.PostCountries(countriesFromApi.Data);

            return countriesFromApi;
        }
    }
}
