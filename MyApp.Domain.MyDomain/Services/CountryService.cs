using MyApp.Constants.MyDomain;
using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Repositories.Abstractions;
using MyApp.Domain.MyDomain.Services.Abstractions;
using Viva;

namespace MyApp.Domain.MyDomain.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryApiService countryApi;
        private readonly ICountryRepository countryRepo;
        private readonly ICacheService cache;

        public CountryService(ICountryApiService httpService, ICountryRepository countryRepository, ICacheService cacheService)
        {
            countryApi = httpService;
            countryRepo = countryRepository;
            cache = cacheService;
        }
        public async Task<IResult<List<CountryContract>>> GetAllCountriesAsync()
        {
            try
            {
                // Get Countries from cache
                //return
                var countriesFromCache = GetCountriesFromCacheAsync(CacheKeys.Countries);
                if (countriesFromCache.Success && countriesFromCache.Data is not null)
                    return Result<List<CountryContract>>.CreateSuccessful(countriesFromCache.Data);

                // Get Countries From DB
                // Store in Cache
                // return
                var countriesFromDb = await GetCountriesFromDbAsync();
                if (countriesFromDb is not null)
                    return Result<List<CountryContract>>.CreateSuccessful(countriesFromDb);

                // Get Countries from API
                // Store In Db
                // return
                var countriesFromApi = await GetCountriesFromApiAsync();
                if (countriesFromApi.Success)
                    return Result<List<CountryContract>>.CreateSuccessful(countriesFromApi.Data);

                return Result<List<CountryContract>>.CreateFailed(ResultCode.NotFound, "Countries not found from api");
            }
            catch (Exception ex)
            {
                return Result<List<CountryContract>>.CreateFailed(ResultCode.InternalServerError, "Error");
            }
        }

        private IResult<List<CountryContract>?> GetCountriesFromCacheAsync(string key)
        {
            return cache.Get<List<CountryContract>?>(key);
        }

        private async Task<List<CountryContract>?> GetCountriesFromDbAsync()
        {
            var countriesFromDb = await countryRepo.GetCountriesFromDbAsync();
            if (countriesFromDb.Any())
            {
                cache.SetItem(CacheKeys.Countries, countriesFromDb, TimeSpan.FromSeconds(10));
                return countriesFromDb;
            }
            return null;
        }

        private async Task<IResult<List<CountryContract>>> GetCountriesFromApiAsync()
        {
            var countriesFromApi = await countryApi.GetCountriesAsync(ApiFields.Default);
            //check if the repo stored the data????
            await countryRepo.PostCountries(countriesFromApi.Data);
            return countriesFromApi;
        }
    }
}
