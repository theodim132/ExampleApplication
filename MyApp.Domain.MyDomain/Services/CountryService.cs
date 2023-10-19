using Microsoft.Extensions.Logging;
using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Factory.Abstractions;
using MyApp.Domain.MyDomain.Handler.Abstractions;
using MyApp.Domain.MyDomain.Handlers.Abstractions;
using MyApp.Domain.MyDomain.Providers.Country.Abstractions;
using MyApp.Domain.MyDomain.Services.Abstractions;
using Viva;

namespace MyApp.Domain.MyDomain.Services
{
    public class CountryService : ICountryService
    {
        private readonly Lazy<ICountryHandler>  getAllCountriesChain;
        private readonly ICountryDbProvider countryDbProvider;
        private readonly ILogger<CountryService> _logger;

        public CountryService(ICountryDbProvider countryDbProvider,
            ICountryApiHandler countryApiHandler,
            ICountryCacheHandler countryCacheHandler,
            ICountryDbHandler countryDbHandler,
            ICountryHandlerFactory handlerFactory,
            ILogger<CountryService> logger)
        {
            _logger = logger;
            this.countryDbProvider = countryDbProvider;
            getAllCountriesChain = new Lazy<ICountryHandler>(() => handlerFactory.CreateChain(countryCacheHandler, countryDbHandler, countryApiHandler));
        }

        public async Task<IResult<List<CountryContract>>> GetAllCountriesAsync() =>
            await getAllCountriesChain.Value.Handle();
        

        public async Task<IResult<CountryContract>> GetCountryByIdAsync(int id)
        {
            try 
            {
                var result = await countryDbProvider.GetCountryByIdAsync(id);
                if (!result.Success)
                {
                    return Result<CountryContract>.CreateFailed(ResultCode.NotFound, result.ErrorText);
                }

                if (result.Data is null) 
                {
                    return Result<CountryContract>.CreateFailed(ResultCode.NotFound, result.ErrorText);
                }
                return result!;
            }
            catch(Exception ex) 
            {
                return Result<CountryContract>.CreateFailed(ResultCode.InternalServerError, ex.Message);
            }
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
