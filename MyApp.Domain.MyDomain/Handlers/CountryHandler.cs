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
   

    public abstract class Handler<TRequest> : ICountryHandler<TRequest>
    {
        protected ICountryHandler<TRequest> NextHandler { get; private set; }

        public virtual async Task<IResult<List<CountryContract>>> Handle(TRequest request)
        {
            if (NextHandler != null)
                return await NextHandler.Handle(request);
            return Result<List<CountryContract>>.CreateFailed(ResultCode.NotFound,"");
        }

        public ICountryHandler<TRequest> SetNext(ICountryHandler<TRequest> handler)
        {
            Console.WriteLine($"Setting next for {this.GetType().Name} to {handler.GetType().Name}");
            NextHandler = handler;
            return NextHandler;
        }
    }


}
