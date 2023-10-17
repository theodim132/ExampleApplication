using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Services.Abstractions;

namespace Example.App.Utility.Components
{
    public static class MiddleWareComponent
    {
        public static WebApplication UseDevelopmentConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            return app;
        }

        public static WebApplication UseStandardMiddleware(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseAuthorization();
            return app;
        }

        //public static WebApplication UseEndpointsConfiguration(this WebApplication app)
        //{
        //    app.MapGet("/api/country/", async (ICountryService countryService) =>
        //    {
        //        var response = await countryService.GetAllCountriesAsync();
        //        return !response.Success ? new List<CountryContract>() : response.Data;
        //    }).WithName("GetAllCountries").Produces<List<CountryContract>>(200);

        //    app.MapGet("/api/country/GetCountryById/{id}", async (ICountryService countryService,int id) =>
        //    {
        //        var response = await countryService.GetCountryByIdAsync(id);
        //        return !response.Success ? new CountryContract() : response.Data;
        //    });

        //    return app;
        //}
    }
}
