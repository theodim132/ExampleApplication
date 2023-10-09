using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.DataAccess.Abstractions.Dto;
using Newtonsoft.Json.Linq;
using System.Net;
using Viva;
using Viva.Diagnostics;
using Viva.Enterprise.Extensions.Serialization;
using Viva.Enterprise.Integration.Refit;

namespace MyApp.DataAccess.HttpServices
{

    public class CountryApiService : RefitHttpClientBase<ICountryApi>, ICountryApiService
    {
        public CountryApiService(HttpClient httpClient, IEventLogService eventLogService, ICamelCaseJsonSerializationService serializationService) 
            : base(httpClient, eventLogService, serializationService)
        {
        }

        public Task<IResult<List<CountryContract>>> GetCountriesAsync(List<string> fields)
            => base.SendRequestAsync<List<string>, List<CountryContract>>(fields, client.GetCountries);
    }
}
