using ExampleApplication.Data;
using ExampleApplication.Models;
using ExampleApplication.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace ExampleApplication.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ResponseDto _responseDto;

        public CountryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _responseDto = new ResponseDto();
        }

        public async Task Delete(int? id)
        {
            try
            {
                IQueryable<Country?> queryableCountryFromDb = _appDbContext.Countries.Where(c => c.Id == id);
                var country = await queryableCountryFromDb.FirstOrDefaultAsync();
                if (country != null)
                {
                    _appDbContext.Remove(country);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<ResponseDto> DeleteAllAsync()
        {

            try
            {
                IQueryable<Country> queryableCountries = _appDbContext.Countries;
                var countries = await queryableCountries.ToListAsync();
                if (countries.Any())
                {
                    _appDbContext.RemoveRange(countries);
                    await _appDbContext.SaveChangesAsync();
                    _responseDto.IsSuccess = true;
                }
                else
                {
                    _responseDto.Message = "No countries to delete.";
                }
            }
            catch (Exception ex)
            {
                _responseDto.Message = $"An error occurred: {ex.Message}";
            }
            return _responseDto;
        }

        public async Task<List<Country>> GetAll()
        {
            try
            {
                IQueryable<Country> queryableCountries = _appDbContext.Countries.Include(u => u.Borders);
                var countries = await queryableCountries.ToListAsync();
                return countries;
            }
            catch (Exception ex)
            {

            }
            return null;
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

                await _appDbContext.Countries.AddRangeAsync(countryEntities);
                await _appDbContext.SaveChangesAsync();

                for (int i = 0; i < countryEntities.Count; i++)
                {
                    var country = countryEntities[i];
                    var countryDto = countries[i];
                    var borders = countryDto?.Borders.Select(border => new Border
                    {
                        Name = border,
                        CountryId = country.Id
                    }).ToList();

                    if (borders?.Any() is not null) await _appDbContext.Borders.AddRangeAsync(borders);

                }
                await _appDbContext.SaveChangesAsync();
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Countries Saved";
            }
            catch (Exception ex)
            {
                _responseDto.Message = "Countries didn't saved " + ex.Message;
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }
    }
}
