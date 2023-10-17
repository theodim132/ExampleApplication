using MyApp.Domain.MyDomain.Handler.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.MyDomain.Handlers.Abstractions
{
    public interface ICountriesFromApi : ICountryHandler<object>
    {
    }
}
