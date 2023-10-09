using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.Abstractions.Dto;
using MyApp.DataAccess.Databases.MyDomain;
using MyApp.Domain.MyDomain.Services.Abstractions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExampleApplication.Controllers
{
    [ApiController]
    [Route("api/country")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ICacheService _cache;
        private readonly ResponseDto _response;

        public CountryController(ICountryService countryService, ICacheService memoryCache,IMapper mapper)
        {
            _countryService = countryService;
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
                var cachedCountries = _cache.Get<List<MyApp.Domain.MyDomain.Dto.CountryDto>>("Countries");
                if (cachedCountries?.Count() > 0)
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
                    if (responseFromDb.Result is List<MyApp.Domain.MyDomain.Dto.CountryDto> countriesFromDb)
                    {
                        _response.Result = countriesFromDb;
                        _response.Message = "Countries from db";
                        _response.IsSuccess = true;

                        _cache.SetItem("Countries", countriesFromDb, TimeSpan.FromSeconds(10));
                        return _response;
                    }
                    else
                    {
                        _response.IsSuccess = false;
                        _response.Message = "Unexpected type received";
                        return _response;
                    }
                }

                List<MyApp.Domain.MyDomain.Dto.CountryDto> countries = new();
                //get countries from the API
                ResponseDto? response = await _countryService.GetAllCountriesAsync();
                if (response is not null && response.IsSuccess)
                {
                    if (response.Result is not null)
                    {
                        string jsonResult = response.Result.ToString();
                        if (jsonResult is not null)
                        {
                            countries = JsonConvert.DeserializeObject<List<MyApp.Domain.MyDomain.Dto.CountryDto>>(jsonResult);
                            _response.Result = countries;
                            _response.Message = "Results from CountriesApi";
                            _response.IsSuccess = true;
                        }
                        
                       if (countries is not null) await _countryService.PostCountries(countries);
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