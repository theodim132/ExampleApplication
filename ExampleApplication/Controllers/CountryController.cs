using AutoMapper;
using ExampleApplication.Models;
using ExampleApplication.Models.Dto;
using ExampleApplication.Services;
using Microservices.Services.HotelRoomAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExampleApplication.Controllers
{
    [ApiController]
    [Route("api/country")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly AppDbContext _appDbContext;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public CountryController(ICountryService countryService,AppDbContext appDbContext,IMemoryCache memoryCache,IMapper mapper)
        {
            _countryService = countryService;
            _appDbContext = appDbContext;
            _cache = memoryCache;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllFromAPI")]
        public async Task<IActionResult> GetAllFromAPI()
        {
            if (_cache.TryGetValue("Countries", out List<CountryDto> cachedCountries))
            {
                return new JsonResult(new { message = cachedCountries });
            }

            List<CountryDto?> countriesFromDb = await _countryService.GetAllCountriesFromDbAsync();

            if(countriesFromDb is not null && countriesFromDb.Any()) return new JsonResult(new { message = countriesFromDb });

            List<CountryDto> list = new();
            ResponseDto? response = await _countryService.GetAllCountriesAsync();
            if (response is not null && response.IsSuccess)
            {
                if (response.Result is not null)
                {
                    string jsonResult = response.Result.ToString();
                    list = JsonConvert.DeserializeObject<List<CountryDto>>(jsonResult);
                    if(list is not null) _countryService.PostCountries(list);
                }
            }
            else
            {
                return new JsonResult(new { message = response?.Message });
            }
            return new JsonResult(new { message = list });
        }


        [HttpGet]
        [Route("GetAllFromDb")]
        public async Task<IActionResult> GetAllFromDb() 
        {
            var list = await _countryService.GetAllCountriesFromDbAsync();
            //var k = _mapper.Map<List<CountryDto>?>(list); 
            return new JsonResult(list);
        }
        [HttpDelete]
        [Route("DeleteAllFromDb")]
        public async Task<IActionResult> DeleteAllFromDb()
        {
            var list = await _countryService.DeleteCountries();
            return new JsonResult(list);
        }

    }
}