
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyApp.DataAccess.Abstractions.CountryApi;
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

        public async Task<List<CountryContract>> GetCountriesFromDbAsync()
        {
            var countries = await context.Countries.ToListAsync();
            var countryContracts = await context.Countries
                 .Select(c => new CountryContract
                 {
                     Name = new CountryContract.CountryName
                     {
                         Common = c.CommonName,
                         Official = c.OfficialName,
                         NativeName = c.NativeNameSpaCommon == null && c.NativeNameSpaOfficial == null ? null : new CountryContract.NativeName
                         {
                             Spa = new CountryContract.NativeName.NativeNameSpa
                             {
                                 Common = c.NativeNameSpaCommon,
                                 Official = c.NativeNameSpaOfficial
                             }
                         }
                     },
                     Capital = c.Capital == null ? null : new List<string> { c.Capital },
                     Borders = c.Borders.Select(b => b.Name).ToList()
                 })
                 .ToListAsync();

            return countryContracts;
        }

        public async Task<bool> PostCountries(List<CountryContract> countries)
        {
            var countriesToPost = countries.Select(c => new Country
            {
                CommonName = c.Name.Common,
                OfficialName = c.Name.Official,
                NativeNameSpaCommon = c.Name.NativeName?.Spa?.Common,
                NativeNameSpaOfficial = c.Name.NativeName?.Spa?.Official,
                Capital = c.Capital.FirstOrDefault(),
                Borders = c.Borders.Select(b => new Border { Name = b }).ToList()
            }).ToList();
            await context.Countries.AddRangeAsync(countriesToPost);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
