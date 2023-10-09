using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.DataAccess.Abstractions.Dto;
using MyApp.Domain.MyDomain.Services.Abstractions;
using Viva;

namespace ExampleApplication.Controllers
{
    [ApiController]
    [Route("api/country")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        [Route("GetAllFromAPI")]
        public async Task<List<CountryContract>> GetAllFromAPI()
        {
            var response = await _countryService.GetAllCountriesAsync();
            if (!response.Success)
            {
                return new List<CountryContract>();
            }
            return response.Data;
        }
    }
}