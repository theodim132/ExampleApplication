﻿using Refit;

namespace MyApp.DataAccess.HttpServices
{
    public interface ICountryApi
    {
        [Get("/independent?status=true")]
        Task<HttpResponseMessage> GetCountries([Query(",", "fields")] List<string> fields);
    }
}
