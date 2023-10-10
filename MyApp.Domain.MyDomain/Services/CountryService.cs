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
                var cachedCountries = cache.Get<List<CountryContract>>("Countries");
                if (cachedCountries?.Count() > 0)
                {
                    return Result<List<CountryContract>>.CreateSuccessful(cachedCountries);
                }
                // Get Countries From DB
                // Store in Cache
                // return
                var countriesFromDb = await countryRepo.GetCountriesFromDbAsync();
                if (countriesFromDb.Any())
                {
                    cache.SetItem("Countries", countriesFromDb, TimeSpan.FromSeconds(10));
                    return Result<List<CountryContract>>.CreateSuccessful(countriesFromDb);
                }
                // Get Countries from API
                // Store In Db
                // return
                var countriesFromApi = await countryApi.GetCountriesAsync(new List<string>
                {
                    "name",
                    "capital",
                    "borders"
                });
                await countryRepo.PostCountries(countriesFromApi.Data);
                return countriesFromApi;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
