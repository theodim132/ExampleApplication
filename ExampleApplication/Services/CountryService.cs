using AutoMapper;
using ExampleApplication.Models;
using ExampleApplication.Models.Dto;
using ExampleApplication.Utility;
using ExampleApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.Metadata;

namespace ExampleApplication.Services
{
    public class CountryService : ICountryService
    {
        private readonly IHttpService _httpService;
        private readonly ConfigurationBuilder _configurationBuilder;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly ICountryRepository _countryRepo;
        

        public CountryService(AppDbContext appDbContext, IHttpService httpService, IMapper mapper,ICountryRepository countryRepository)
        {
            _context = appDbContext;
            _httpService = httpService;
            _mapper = mapper;
            _countryRepo = countryRepository;
        }

        public async Task<ResponseDto?> DeleteCountries()
        {
            try
            {
                var countriesFromDb = await _context.Countries.ToListAsync();
                if (countriesFromDb is not null)
                {
                    _context.Countries.RemoveRange(countriesFromDb);
                    await _context.SaveChangesAsync();
                    _cache.Remove("Countries");
                    return new ResponseDto { Message = "All countries Deleted", IsSuccess = true };
                }
                return new ResponseDto { Message = "Could not delete countries", IsSuccess = false };

            }
            catch (Exception ex)
            {
                throw ex;
            }



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

        public async Task<List<CountryDto>?> GetAllCountriesFromDbAsync()
        {
            var query = _context.Countries.Include(u => u.Borders);
            var countriesFromDb = _mapper.Map<List<CountryDto>>(await query.ToListAsync());
            _cache.Set("Countries", countriesFromDb);
            return countriesFromDb;
        }

        public void PostCountries(List<CountryDto> countries)
        {
            try
            {

                var countryEntities = countries.Select(dto => new Country
                {
                    Name = dto.Name.Common,
                    Capital = dto.Capital.FirstOrDefault(),
                }).ToList();

                _context.Countries.AddRange(countryEntities);
                _context.SaveChanges();

                for (int i = 0; i < countryEntities.Count; i++)
                {
                    var country = countryEntities[i];
                    var countryDto = countries[i];
                    var borders = countryDto.Borders.Select(border => new Border
                    {
                        Name = border,
                        CountryId = country.Id
                    }).ToList();

                    _context.Borders.AddRange(borders);
                }
                _context.SaveChanges();
                _cache.Set("Countries", countries);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
