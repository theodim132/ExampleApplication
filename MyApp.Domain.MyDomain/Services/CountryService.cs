using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Handler;
using MyApp.Domain.MyDomain.Handler.Abstractions;
using MyApp.Domain.MyDomain.Providers.Country.Abstractions;
using MyApp.Domain.MyDomain.Services.Abstractions;
using Viva;

namespace MyApp.Domain.MyDomain.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryHandler<object> _firstHandler;
        private readonly ICountryCacheHanlder countryCacheHanlder;
        private readonly ICountryApiHanlder countryApiHanlder;
        private readonly ICountryDbHanlder countryDbHanlder;
        private readonly ILogger<CountryService> _logger;

        public CountryService(
            ICountryApiHanlder countryApiHanlder,
            ICountryCacheHanlder countryCacheHanlder,
            ICountryDbHanlder countryDbHanlder,
            ILogger<CountryService> logger)
        {
            _logger = logger;
            this.countryCacheHanlder = countryCacheHanlder;
            this.countryApiHanlder = countryApiHanlder;
            this.countryDbHanlder = countryDbHanlder;

            countryCacheHanlder.SetNext(countryDbHanlder).SetNext(countryApiHanlder);
            _firstHandler = countryCacheHanlder;
        }

        public async Task<IResult<List<CountryContract>>> GetAllCountriesAsync()
        {
            return await _firstHandler.Handle(null);
        }


        //public async Task<IResult<List<CountryContract>>> GetAllCountriesAsync()
        //{
        //    try
        //    {
        //        logger.LogInformation("CountryService.GetAllCountriesAsync called",
        //            DateTime.UtcNow);

        //        var countriesFromCache = cacheProvider.GetCountries();
        //        if (countriesFromCache.Success && countriesFromCache.Data is not null)
        //        {
        //            logger.LogInformation("Returned countries from cache",
        //                    DateTime.UtcNow);
        //            return Result<List<CountryContract>>.CreateSuccessful(countriesFromCache.Data);
        //        }

        //        var countriesFromDb = await countryDbProvider.GetCountriesAsync();
        //        if (!countriesFromDb.IsNullOrEmpty())
        //        {
        //            cacheProvider.SetCountries(countriesFromDb);
        //            logger.LogInformation("Returned countries from db and returned",
        //                   DateTime.UtcNow);
        //            return Result<List<CountryContract>>.CreateSuccessful(countriesFromDb!);
        //        }

        //        var countriesFromApi = await countryApiProvider.GetCountriesAsync();
        //        if (!countriesFromApi.Success)
        //        {
        //            logger.LogInformation("Returned countries from API",
        //                   DateTime.UtcNow);
        //            return Result<List<CountryContract>>.CreateFailed(ResultCode.NotFound, countriesFromApi.ErrorText);
        //        }

        //        return Result<List<CountryContract>>.CreateSuccessful(countriesFromApi.Data);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex, "CountryService.GetAllCountriesAsync : An error occurred");
        //        return Result<List<CountryContract>>.CreateFailed(ResultCode.InternalServerError, ex.Message);
        //    }
        //}
    }
}
