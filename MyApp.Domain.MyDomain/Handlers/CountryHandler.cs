using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Handler.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viva;

namespace MyApp.Domain.MyDomain.Handler
{
   

    public abstract class Handler : ICountryHandler
    {
        protected ICountryHandler NextHandler { get; private set; }

        public virtual async Task<IResult<List<CountryContract>>> Handle()
        {
            if (NextHandler != null)
                return await NextHandler.Handle();
            return Result<List<CountryContract>>.CreateFailed(ResultCode.NotFound,"");
        }

        public ICountryHandler SetNext(ICountryHandler handler)
        {
            Console.WriteLine($"Setting next for {this.GetType().Name} to {handler.GetType().Name}");
            NextHandler = handler;
            return NextHandler;
        }
    }


}
