using AutoMapper;
using Microsoft.Extensions.Configuration;
using MyApp.Constants;
using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.Abstractions.Dto;
using MyApp.DataAccess.Abstractions.HttpService;
using MyApp.Domain.MyDomain.Model;
using MyApp.Domain.MyDomain.Repositories.Abstractions;
using MyApp.Domain.MyDomain.Services.Abstractions;

namespace MyApp.Domain.MyDomain.Services
{
    public class CountryService : ICountryService
    {
        private readonly IHttpService _httpService;
        private readonly ConfigurationBuilder _configurationBuilder;
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepo;
        private readonly ResponseDto _responseDto;
        private readonly ICacheService _cacheService;


        public CountryService(IHttpService httpService, IMapper mapper,ICountryRepository countryRepository, ICacheService cacheService)
        {
            _httpService = httpService;
            _mapper = mapper;
            _countryRepo = countryRepository;
            _responseDto = new ResponseDto();
            _cacheService = cacheService;
        }

        public async Task<ResponseDto> DeleteCountriesAsync()
        {
            try
            {
                var result = await _countryRepo.DeleteAllAsync();
                if (result.IsSuccess)
                {
                    _cacheService.Delete<Country>("Countries");
                    return new ResponseDto { Message = "Countries Deleted", IsSuccess = true };
                }
                else
                {
                    return new ResponseDto { Message = result.Message, IsSuccess = false };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { Message = "An error occurred while deleting countries.", IsSuccess = false };
            }
        }


        public async Task<ResponseDto> GetAllCountriesAsync()
        {
            var response = await _httpService.SendAsync(new RequestDto()
            {
                ApiType = "GET",
                Url = Api.CountryAPI + "independent?fields=name,capital,borders"
            });
            return response;
        }

        public async Task<ResponseDto> GetAllCountriesFromDbAsync()
        {
            try
            {
                var query = await _countryRepo.GetAll();
                var countriesFromDb = _mapper.Map<List<MyApp.Domain.MyDomain.Dto.CountryDto>>(query);
                if (query.Any())
                {
                    _responseDto.Result = countriesFromDb;
                    _responseDto.Message = "Countries From db";
                    TimeSpan timeSpan = TimeSpan.FromSeconds(1);
                    _cacheService.SetItem<List<Country>>("Countries", query, timeSpan);
                }
                else
                {
                 _responseDto.IsSuccess = false;
                }
            }
            catch (Exception ex) 
            {
                _responseDto.Message += ex.Message;
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }

        public  async Task<ResponseDto> PostCountries(List<MyApp.Domain.MyDomain.Dto.CountryDto> countries)
        {
            try
            {
                if (countries.Any())
                {
                    var result = await _countryRepo.PostCountries(countries);
                    _responseDto.IsSuccess = result.IsSuccess;
                    _responseDto.Message = result.Message;
                }
                else 
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "No countries to post";
                }
                
            }
            catch (Exception ex) 
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}
