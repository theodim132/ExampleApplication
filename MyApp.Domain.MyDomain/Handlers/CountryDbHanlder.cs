﻿using Microsoft.IdentityModel.Tokens;
using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Handler.Abstractions;
using MyApp.Domain.MyDomain.Providers.Country.Abstractions;
using Viva;

namespace MyApp.Domain.MyDomain.Handler
{

    public class CountryDbHanlder : Handler<object> , ICountryDbHanlder
    {
        private readonly ICountryDbProvider dbProvider;
        private readonly ICountryCacheProvider cacheProvider;

        public CountryDbHanlder(ICountryDbProvider dbProvider, ICountryCacheProvider cacheProvider)
        {
            this.dbProvider = dbProvider;
            this.cacheProvider = cacheProvider;
        }

        public override async Task<IResult<List<CountryContract>>> Handle(object request)
        {
            var result = await dbProvider.GetCountriesAsync();
            if (!result.IsNullOrEmpty())
            {
                cacheProvider.SetCountries(result);
                return Result<List<CountryContract>>.CreateSuccessful(result!);
            }

            return await base.Handle(request);
        }

    }
}
