using MyApp.Domain.MyDomain.Factory.Abstractions;
using MyApp.Domain.MyDomain.Handler.Abstractions;
using MyApp.Domain.MyDomain.Handlers.Abstractions;

namespace MyApp.Domain.MyDomain.Factory
{
    public class CountryHandlerFactory : ICountryHandlerFactory
    {
        private readonly IEnumerable<ICountryHandler> handlers;

        public CountryHandlerFactory(IEnumerable<ICountryHandler> handlers)
        {
            this.handlers = handlers;
        }

        public ICountryHandler CreateChain()
        {
            Console.WriteLine("Creating chain");
            return handlers.Aggregate(default(ICountryHandler), (a, s) => a is null ? s : a.SetNext(s));
        }
    }
}
