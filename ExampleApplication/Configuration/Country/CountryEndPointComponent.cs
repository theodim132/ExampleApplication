using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Services.Abstractions;

namespace Example.App.Configuration.Country
{
    public static class CountryEndPointComponent
    {
        public static WebApplication UseCountryEndPoints(this WebApplication app)
        {
            var countryEndPoint = app.MapGroup("/api/country/");

            countryEndPoint.MapGet("/GetAll", HandleGetAll);
            countryEndPoint.MapGet("/{id}", HandleGetById);

            return app;
        }

        private static async Task<List<CountryContract>> HandleGetAll(ICountryService countryService)
        {
            var response = await countryService.GetAllCountriesAsync();
            return !response.Success ? new List<CountryContract>() : response.Data;
        }

        private static async Task<CountryContract> HandleGetById(ICountryService countryService, int id)
        {
            var response = await countryService.GetCountryByIdAsync(id);
            return !response.Success ? new CountryContract() : response.Data;
        }
    }
}
