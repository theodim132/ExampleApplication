//using Microsoft.Extensions.Logging;
//using Moq;
//using MyApp.Constants.MyDomain;
//using MyApp.DataAccess.Abstractions.CacheService;
//using MyApp.DataAccess.Abstractions.CountryApi;
//using MyApp.Domain.MyDomain.Providers.Country.Abstractions;
//using MyApp.Domain.MyDomain.Repositories.Abstractions;
//using MyApp.Domain.MyDomain.Services;
//using Viva;

//namespace MyApp.Tests.Base
//{
//    public class CountryServiceTest
//    {
//        private readonly Mock<ICountryCacheProvider> mockCacheService;
//        private readonly Mock<ILogger<CountryService>> mockLogger;
//        private readonly Mock<ICountryDbProvider> mockCountryRepo;
//        private readonly Mock<ICountryApiProvider> mockCountryApi;

//        public CountryServiceTest()
//        {
//            mockCacheService = new Mock<ICountryCacheProvider>();
//            mockLogger = new Mock<ILogger<CountryService>>();
//            mockCountryRepo = new Mock<ICountryDbProvider>();
//            mockCountryApi = new Mock<ICountryApiProvider>();
//        }
//        [Fact]
//        public async Task GetAllCountries_ReturnsCountriesFromCache_WhenCacheIsNotEmpty()
//        {
//            var countries = CreateSampleCountries();
//            var resultToPass = new Result<List<CountryContract>>();
//            resultToPass.Data = countries;

//            mockCacheService
//                .Setup(c => 
//                    c.GetCountries())
//                .Returns(resultToPass);

//            var service = CreateCountryService();

//            var result = await service.GetAllCountriesAsync();

//            Assert.NotNull(result);
//            Assert.True(result.Success);
//            Assert.Equal(countries, result.Data);
//        }

//        [Fact]
//        public async Task GetAllCountries_SkipCache_GetCountriesFromDbThenCache()
//        {

//            var countriesFromDb = CreateSampleCountries();

//            mockCacheService
//                .Setup(m =>
//                    m.GetCountries())
//                .Returns((IResult<List<CountryContract>>)new Result<List<CountryContract>>());

//            mockCountryRepo
//                .Setup(m => m.GetCountriesAsync())
//                .ReturnsAsync(countriesFromDb);

//            var service = CreateCountryService();

//            var result = await service.GetAllCountriesAsync();

//            Assert.NotNull(result);
//            Assert.Equal(countriesFromDb, result.Data);
//            //mockCacheService
//            //    .Verify(m => 
//            //            m.SetCountries(
//            //                countriesFromDb, 
//            //                It.IsAny<TimeSpan>()), 
//            //                Times.Once);

//            mockCountryRepo
//                .Verify(m => 
//                        m.GetCountriesAsync(), Times.Once);
//        }
//        //private CountryService CreateCountryService()
//        //{
//        //    return new CountryService(
//        //        mockCountryApi.Object,
//        //        mockCountryRepo.Object,
//        //        mockCacheService.Object,
//        //        mockLogger.Object
//        //    );
//        //}

//        private List<CountryContract> CreateSampleCountries()
//        {
//            return new List<CountryContract>
//            {
//                new CountryContract
//                {
//                    Name = new CountryContract.CountryName
//                    {
//                        Common = "United States",
//                        Official = "United States of America",
//                        NativeName = new CountryContract.NativeName
//                        {
//                            Spa = new CountryContract.NativeName.NativeNameSpa
//                            {
//                                Common = "Estados Unidos",
//                                Official = "Estados Unidos de América"
//                            }
//                        }
//                    },
//                    Borders = new List<string> { "Canada", "Mexico" },
//                    Capital = new List<string> { "Washington, D.C." }
//                },
//                new CountryContract
//                {
//                    Name = new CountryContract.CountryName
//                    {
//                        Common = "Canada",
//                        Official = "Canada",
//                        NativeName = new CountryContract.NativeName
//                        {
//                            Spa = new CountryContract.NativeName.NativeNameSpa
//                            {
//                                Common = "Canadá",
//                                Official = "Canadá"
//                            }
//                        }
//                    },
//                    Borders = new List<string> { "United States" },
//                    Capital = new List<string> { "Ottawa" }
//                }
//            };
//        }
//    }
//}