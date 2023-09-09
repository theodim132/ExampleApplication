using ExampleApplication.Data;
using ExampleApplication.Models;
using ExampleApplication.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace ExampleApplication.Services
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CountryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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

        public async Task DeleteAll()
        {
            try
            {
                IQueryable<Country> queryableCountries = _appDbContext.Countries;
                var countries = await queryableCountries.ToListAsync();
                if (countries.Any())
                {
                    _appDbContext.RemoveRange(countries);
                }
            }
            catch (Exception ex)
            {

            }

        }
        public  IQueryable<Country> GetAll()
        {
            try
            {
                IQueryable<Country> queryableCountries = _appDbContext.Countries;
                return queryableCountries;
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }

        public Task PostCountries(List<CountryDto?> countries)
        {
            throw new NotImplementedException();
        }
    }
}
