using Example.App.Services;
using Example.App.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MyApp.Domain.MyDomain.Model;
using Viva;

namespace Example.App.Configuration
{
    public static class RequestObjEndPointComponent
    {
        public static WebApplication UserRequestObjEndPoints(this WebApplication app)
        {
            app.MapPost("api/requestObj/FindSecondLargest", HanldeFindSecondLargest);
            return app;
        }

        private static async Task<IResult<int>> HanldeFindSecondLargest(IRequestObjService service,[FromBody] RequestObj request) =>
             await service.FindSecondLargestAsync(request.RequestArrayObj);
    }
}
