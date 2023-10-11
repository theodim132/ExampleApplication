
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.DataAccess.Abstractions.MyDomain.Entities;
using MyApp.DataAccess.Databases.MyDomain;
using MyApp.Domain.MyDomain.Dto;
using MyApp.Domain.MyDomain.Mappers;
using MyApp.Domain.MyDomain.Repositories.Abstractions;
using Viva;

namespace MyApp.Domain.MyDomain.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly MyDomainDbContext context;

        public CountryRepository(MyDomainDbContext appDbContext)
        {
            context = appDbContext;
        }
        public async Task<List<CountryContract>> GetCountriesFromDbAsync()
        {
            var countries = await context.Countries
                .Include(c=>c.Borders)
                .ToListAsync();
            return countries.ToCountryContracts();
        }
        public async Task PostCountries(List<CountryContract> countries)
        {
            var countriesToPost = countries.ToCountryEntities();
            await context.Countries.AddRangeAsync(countriesToPost);
            await context.SaveChangesAsync();
        }
    }
}
