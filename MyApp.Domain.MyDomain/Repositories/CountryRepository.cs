
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyApp.DataAccess.Abstractions.MyDomain.Entities;
using MyApp.DataAccess.Databases.MyDomain;
using MyApp.Domain.MyDomain.Dto;
using MyApp.Domain.MyDomain.Repositories.Abstractions;

namespace MyApp.Domain.MyDomain.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly MyDomainDbContext context;
        private readonly ResponseDto _responseDto;
        private readonly IMapper _mapper;

        public CountryRepository(MyDomainDbContext appDbContext, IMapper mapper)
        {
            context = appDbContext;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }

        public async Task Delete(int? id)
        {
            IQueryable<Model.Country?> queryableCountryFromDb =
                context
                .Set<Country>()
                .Where(c => c.Id == id)
                .ProjectTo<Model.Country>(_mapper.ConfigurationProvider);

            var country = await queryableCountryFromDb.FirstOrDefaultAsync();
            if (country != null)
            {
                context.Remove(country);
            }
        }

        public async Task<ResponseDto> DeleteAllAsync()
        {
            IQueryable<Country> queryableCountries = context.Set<Country>();

            var countries = await queryableCountries.ToListAsync();
            if (countries.Any())
            {
                context.Countries.RemoveRange(countries);
                await context.SaveChangesAsync();
                _responseDto.IsSuccess = true;
            }
            else
            {
                _responseDto.Message = "No countries to delete.";
            }
            return _responseDto;
        }

        public async Task<List<Model.Country>> GetAll()
        {
            IQueryable<Model.Country> queryableCountries = context
                .Set<Country>()
                .Include(u => u.Borders)
                .ProjectTo<Model.Country>(_mapper.ConfigurationProvider);

            var countries = await queryableCountries.ToListAsync();
            return countries;
        }

        public async Task<ResponseDto> PostCountries(List<CountryDto> countries)
        {
            try
            {
                var countryEntities = countries.Select(dto => new Country
                {
                    Name = dto.Name.Common,
                    Capital = dto.Capital.FirstOrDefault(),
                }).ToList();

                await context.Countries.AddRangeAsync(countryEntities);
                await context.SaveChangesAsync();


                for (int i = 0; i < countryEntities.Count; i++)
                {
                    var country = countryEntities[i];
                    var countryDto = countries[i];
                    var borders = countryDto?.Borders.Select(border => new Border
                    {
                        Name = border,
                        CountryId = country.Id
                    }).ToList();

                    if (borders?.Any() is not null) await context.Set<Border>().AddRangeAsync(borders);

                }
                await context.SaveChangesAsync();
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Countries Saved";
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}
