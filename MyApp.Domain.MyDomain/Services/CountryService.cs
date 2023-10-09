using AutoMapper;
using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.DataAccess.Abstractions.Dto;
using MyApp.DataAccess.Abstractions.MyDomain.Entities;
using MyApp.Domain.MyDomain.Repositories.Abstractions;
using MyApp.Domain.MyDomain.Services.Abstractions;
using System.Collections.Generic;
using Viva;

namespace MyApp.Domain.MyDomain.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryApiService countryApi;
        private readonly ICountryRepository _countryRepo;
        private readonly ICacheService _cacheService;

        public CountryService(ICountryApiService httpService, ICountryRepository countryRepository, ICacheService cacheService)
        {
            countryApi = httpService;
            _countryRepo = countryRepository;
            _cacheService = cacheService;
        }
        public async Task<IResult<List<CountryContract>>> GetAllCountriesAsync()
        {
            try
            {
                // Get Countries from cache
                var cachedCountries = _cacheService.Get<List<CountryContract>>("Countries");
                if (cachedCountries?.Count() > 0)
                {
                    return Result<List<CountryContract>>.CreateSuccessful(cachedCountries);
                }
                // Get Countries From DB
                // Store in Cache
                // return
                var countriesFromDb = await _countryRepo.GetCountriesFromDbAsync();
                if (countriesFromDb.Any())
                {
                    _cacheService.SetItem("Countries", countriesFromDb, TimeSpan.FromSeconds(10));
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
                await _countryRepo.PostCountries(countriesFromApi.Data);
                return countriesFromApi;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
