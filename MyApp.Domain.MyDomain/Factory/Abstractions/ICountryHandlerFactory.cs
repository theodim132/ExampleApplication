using MyApp.Domain.MyDomain.Handler.Abstractions;

namespace MyApp.Domain.MyDomain.Factory.Abstractions
{
    public interface ICountryHandlerFactory
    {
        ICountryHandler CreateChain();
    }
}
