using Moq;
using MyApp.Constants.MyDomain;
using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.DataAccess.HttpServices;
using MyApp.Domain.MyDomain.Model;
using MyApp.Domain.MyDomain.Repositories.Abstractions;
using MyApp.Domain.MyDomain.Services;

namespace MyApp.Tests.Base
{
    public class CountryServiceTest
    {
        [Fact]
        public async Task GetAllCountriesAsync_ReturnsCountriesFromCache_WhenCacheIsNotEmpty()
        {
            var mockCacheService = new Mock<ICacheService>();
            var mockCountryRepo = new Mock<ICountryRepository>();
            var mockCountryApi = new Mock<ICountryApiService>();

            var countries = new List<CountryContract>
            {
                new CountryContract
                {
                    Name = new CountryContract.CountryName
                    {
                        Common = "United States",
                        Official = "United States of America",
                        NativeName = new CountryContract.NativeName
                        {
                            Spa = new CountryContract.NativeName.NativeNameSpa
                            {
                                Common = "Estados Unidos",
                                Official = "Estados Unidos de América"
                            }
                        }
                    },
                    Borders = new List<string> { "Canada", "Mexico" },
                    Capital = new List<string> { "Washington, D.C." }
                },
                new CountryContract
                {
                    Name = new CountryContract.CountryName
                    {
                        Common = "Canada",
                        Official = "Canada",
                        NativeName = new CountryContract.NativeName
                        {
                            Spa = new CountryContract.NativeName.NativeNameSpa
                            {
                                Common = "Canadá",
                                Official = "Canadá"
                            }
                        }
                    },
                    Borders = new List<string> { "United States" },
                    Capital = new List<string> { "Ottawa" }
                }
            };
            mockCacheService.Setup(c => 
                c.Get<List<CountryContract>>(CacheKeys.Countries)
                )
                .Returns(countries);

            var service = new CountryService(
                mockCountryApi.Object, 
                mockCountryRepo.Object, 
                mockCacheService.Object);

            var result = await service.GetAllCountriesAsync();

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(countries, result.Data);
        }

        [Fact]
        public async Task GetAllCountries_SkipCache_GetCountriesFromDbThenCache()
        {
            var mockCacheService = new Mock<ICacheService>();
            var mockCountryRepo = new Mock<ICountryRepository>();
            var mockCountryApi = new Mock<ICountryApiService>();

            var countriesFromDb = new List<CountryContract>
            {
                new CountryContract
                {
                    Name = new CountryContract.CountryName
                    {
                        Common = "United States",
                        Official = "United States of America",
                        NativeName = new CountryContract.NativeName
                        {
                            Spa = new CountryContract.NativeName.NativeNameSpa
                            {
                                Common = "Estados Unidos",
                                Official = "Estados Unidos de América"
                            }
                        }
                    },
                    Borders = new List<string> { "Canada", "Mexico" },
                    Capital = new List<string> { "Washington, D.C." }
                },
                new CountryContract
                {
                    Name = new CountryContract.CountryName
                    {
                        Common = "Canada",
                        Official = "Canada",
                        NativeName = new CountryContract.NativeName
                        {
                            Spa = new CountryContract.NativeName.NativeNameSpa
                            {
                                Common = "Canadá",
                                Official = "Canadá"
                            }
                        }
                    },
                    Borders = new List<string> { "United States" },
                    Capital = new List<string> { "Ottawa" }
                }
            };

            mockCacheService.Setup(m => m.Get<List<CountryContract>>(CacheKeys.Countries)).Returns((List<CountryContract>)null);
            mockCountryRepo.Setup(m => m.GetCountriesFromDbAsync()).ReturnsAsync(countriesFromDb);

            var service = new CountryService(
                 mockCountryApi.Object,
                 mockCountryRepo.Object,
                 mockCacheService.Object);

            var result = await service.GetAllCountriesAsync();

            Assert.NotNull(result);
            Assert.Equal(countriesFromDb, result.Data);
            mockCacheService.Verify(m => m.SetItem(CacheKeys.Countries, countriesFromDb, It.IsAny<TimeSpan>()), Times.Once);
            mockCountryRepo.Verify(m => m.GetCountriesFromDbAsync(), Times.Once);
        }

    }
}