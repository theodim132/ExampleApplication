using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.DataAccess.Abstractions.MyDomain.Entities;
using MyApp.DataAccess.Databases.MyDomain;
using MyApp.Domain.MyDomain.Repositories;
using MyApp.Domain.MyDomain.Repositories.Abstractions;

namespace MyApp.Tests.Base
{
    public class CountryRepositoryTests
    {
        [Fact]
        public async Task Test_GetCountriesFromDbAsync()
        {
            var options = new DbContextOptionsBuilder<MyDomainDbContext>()
                .UseInMemoryDatabase(databaseName: "Example_Db")
                .Options;
            var countries = new List<Country>
            {
                new Country
                {
                    Id = 1,
                    CommonName = "United States",
                    OfficialName = "United States of America",
                    NativeNameSpaCommon = "Estados Unidos",
                    NativeNameSpaOfficial = "Estados Unidos de América",
                    Capital = "Washington, D.C.",
                    Borders = new List<Border>
                    {
                        new Border { },
                    }
                },
                new Country
                {
                    Id = 2,
                    CommonName = "Canada",
                    OfficialName = "Canada",
                    NativeNameSpaCommon = "Canadá",
                    NativeNameSpaOfficial = "Canadá",
                    Capital = "Ottawa",
                    Borders = new List<Border>
                    {
                        new Border {  },
                    }
                },
            };

            using (var context = new MyDomainDbContext(options))
            {
               await context.Countries.AddRangeAsync(countries);
               await  context.SaveChangesAsync();
            }

            // Act
            List<CountryContract> result;
            using (var context = new MyDomainDbContext(options))
            {
                var repo = new CountryRepository(context);
                result = await repo.GetCountriesFromDbAsync();
            }

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task Test_PostCountries()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MyDomainDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_PostCountries")
                .Options;

            var countriesToPost = new List<CountryContract>
            {
                new CountryContract { /* set properties */ },
            };

            using (var context = new MyDomainDbContext(options))
            {
                var repo = new CountryRepository(context);
                await repo.PostCountries(countriesToPost);
            }

            using (var context = new MyDomainDbContext(options))
            {
                Assert.Equal(countriesToPost.Count, context.Countries.Count());
            }
        }
    }
}
