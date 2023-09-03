using AutoMapper;
using ExampleApplication.Models;
using ExampleApplication.Models.Dto;
using ExampleApplication.Utility;
using Microservices.Services.HotelRoomAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ExampleApplication.Services
{
    public class CountryService : ICountryService
    {
        private readonly IHttpService _httpService;
        private readonly ConfigurationBuilder _configurationBuilder;
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public CountryService(AppDbContext appDbContext,IHttpService httpService,IMapper mapper)
        {
            _appDbContext = appDbContext;
            _httpService = httpService;
            _mapper = mapper;
        }

        public async Task<ResponseDto?> GetAllCountriesAsync()
        {
            var response = await _httpService.SendAsync(new RequestDto()
            {
                ApiType = "GET",
                Url = Constants.CountryAPI + "independent?fields=name,capital,borders"
            });
            return response;
        }

        public async Task<List<Country>?> GetAllCountriesFromDbAsync()
        {
            var query = _appDbContext.Countries;
            return await query.ToListAsync();
        }

        public void PostCountries(List<CountryDto> countries)
        {
           var list  = _mapper.Map<List<Country>>(countries);
            _appDbContext.AddAsync(list);
            _appDbContext.SaveChangesAsync();
        }
    }
}
