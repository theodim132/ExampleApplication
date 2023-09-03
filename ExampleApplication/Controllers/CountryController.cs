using ExampleApplication.Models;
using ExampleApplication.Models.Dto;
using ExampleApplication.Services;
using Microservices.Services.HotelRoomAPI.Data;
using Microsoft.AspNetCore.Mvc;
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

        public CountryController(ICountryService countryService,AppDbContext appDbContext)
        {
            _countryService = countryService;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [Route("GetAllFromAPI")]
        public async Task<IActionResult> GetAllFromAPI()
        {
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
            return new JsonResult(list);
        }

    }
}