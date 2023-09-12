using AutoMapper;
using ExampleApplication.Data;
using ExampleApplication.Models;
using ExampleApplication.Models.Dto;
using ExampleApplication.Services;
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
        private readonly ResponseDto _response;

        public CountryController(ICountryService countryService,AppDbContext appDbContext,IMemoryCache memoryCache,IMapper mapper)
        {
            _countryService = countryService;
            _appDbContext = appDbContext;
            _cache = memoryCache;
             _response = new ResponseDto();
        }

        [HttpGet]
        [Route("GetAllFromAPI")]
        public async Task<ResponseDto> GetAllFromAPI()
        {
            try
            {
                //get cached countries if any
                if (_cache.TryGetValue("Countries", out List<CountryDto> cachedCountries))
                {
                    _response.Result = cachedCountries;
                    _response.Message = "Countries from cached data";
                    _response.IsSuccess = true;
                    return _response;
                }
                //get countries from db if any
                ResponseDto responseFromDb = await _countryService.GetAllCountriesFromDbAsync();
                if (responseFromDb.IsSuccess == true) 
                {
                    _response.Result = responseFromDb.Result;
                    _response.Message = "Countries from db";
                    _response.IsSuccess = true;
                    return _response;
                }

                List<CountryDto> countries = new();
                //get countries from the API
                ResponseDto? response = await _countryService.GetAllCountriesAsync();
                if (response is not null && response.IsSuccess)
                {
                    if (response.Result is not null)
                    {
                        string jsonResult = response.Result.ToString();
                        if (jsonResult is not null)
                        {
                            countries = JsonConvert.DeserializeObject<List<CountryDto>>(jsonResult);
                            _response.Result = countries;
                            _response.Message = "Results from CountriesApi";
                            _response.IsSuccess = true;
                        }
                        
                       if (countries is not null) _countryService.PostCountries(countries);
                    }
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("GetAllFromDb")]
        public async Task<ResponseDto?> GetAllFromDb() 
        {
            try
            {
                var countries = await _countryService.GetAllCountriesFromDbAsync();
                _response.Result = countries;
            }catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete]
        [Route("DeleteAllFromDb")]
        public async Task<ResponseDto?> DeleteAllFromDb()
        {
            try
            {
                var response = await _countryService.DeleteCountriesAsync();
                _response.Message = response.Message;
                _response.IsSuccess = response.IsSuccess;
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

    }
}