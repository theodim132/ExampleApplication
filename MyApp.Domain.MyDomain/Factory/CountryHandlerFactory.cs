using MyApp.Domain.MyDomain.Factory.Abstractions;
using MyApp.Domain.MyDomain.Handler.Abstractions;
using MyApp.Domain.MyDomain.Handlers.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.MyDomain.Factory
{
    public class CountryHandlerFactory : ICountryHandlerFactory
    {
        public ICountryHandler<object> CreateChain(
             ICountryCacheHandler cacheHandler,
             ICountryDbHandler dbHandler,
             ICountryApiHandler apiHandler)
        {
            Console.WriteLine("Creating chain");
            cacheHandler.SetNext(dbHandler).SetNext(apiHandler);
            return cacheHandler;
        }
    }
}
