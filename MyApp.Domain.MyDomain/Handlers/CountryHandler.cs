using MyApp.DataAccess.Abstractions.CountryApi;
using MyApp.Domain.MyDomain.Handler.Abstractions;
using Viva;

namespace MyApp.Domain.MyDomain.Handler
{


    public abstract class Handler : ICountryHandler
    {
        protected ICountryHandler? NextHandler { get; private set; }

        public virtual Task<IResult<List<CountryContract>>> HandleAsync()
        {
            if (NextHandler != null)
                return NextHandler.HandleAsync();

            return Task.FromResult(Result<List<CountryContract>>.CreateFailed(ResultCode.NotFound, "No handlers left."));
        }

        public ICountryHandler SetNext(ICountryHandler handler)
        {
            Console.WriteLine($"Setting next for {this.GetType().Name} to {handler.GetType().Name}");
            NextHandler = handler;
            return NextHandler;
        }
    }


}
