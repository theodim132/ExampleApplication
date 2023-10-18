using Azure.Core;
using MyApp.Domain.MyDomain.Handler.Abstractions;
using MyApp.Domain.MyDomain.Handlers.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.MyDomain.Factory.Abstractions
{
    public interface ICountryHandlerFactory
    {
        ICountryHandler<object> CreateChain(
             ICountryCacheHandler cacheHandler,
             ICountryDbHandler dbHandler,
             ICountryApiHandler apiHandler);
    }
}
