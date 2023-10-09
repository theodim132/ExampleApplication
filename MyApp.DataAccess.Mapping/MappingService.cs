using System.Linq.Expressions;

namespace MyApp.DataAccess.Mapping
{
    public class MappingService : IMappingService
    {
        private readonly IEnumerable<IMappingProvider> mappingProviders;
        private readonly IMappingErrorObserver? mappingErrorHandler;

        public MappingService(
            IEnumerable<IMappingProvider> mappingProviders,
            IMappingErrorObserver? mappingErrorHandler = null)
        {
            this.mappingProviders = mappingProviders;
            this.mappingErrorHandler = mappingErrorHandler;
        }

        private (IMapping, IMappingProvider) GetMapping(Type sourceType, Type destinationType)
        {
            foreach (var p in mappingProviders)
            {
                var mapping = p.GetMapping(sourceType, destinationType);
                if (mapping != null) return (mapping, p);
            }

            var mappingException = new MappingNotFoundException(sourceType, destinationType);
            mappingErrorHandler?.OnError(mappingException);
            throw mappingException;
        }

        private object Map(Type sourceType, Type destinationType, object source)
        {
            var (mapping, provider) = GetMapping(sourceType, destinationType);
            try
            {
                return mapping.Map(source);
            }
            catch (Exception ex)
            {
                var invocationException = new MappingInvocationException(mapping, provider, ex);
                mappingErrorHandler?.OnError(invocationException);
                throw invocationException;
            }
        }

        private object Map(Type sourceType, Type destinationType, object source, object destination)
        {
            var (mapping, provider) = GetMapping(sourceType, destinationType);
            try
            {
                return mapping.Map(source, destination);
            }
            catch (Exception ex)
            {
                var invocationException = new MappingInvocationException(mapping, provider, ex);
                mappingErrorHandler?.OnError(invocationException);
                throw invocationException;
            }
        }

        public TDestination Map<TDestination>(object source)
            => (TDestination)Map(source.GetType(), typeof(TDestination), source);

        public TDestination Map<TSource, TDestination>(TSource source)
            => (TDestination)Map(typeof(TSource), typeof(TDestination), source!);

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
            => (TDestination)Map(typeof(TSource), typeof(TDestination), source!, destination!);

        public object Map(object source, Type sourceType, Type destinationType)
            => Map(sourceType, destinationType, source);

        public object Map(object source, object destination, Type sourceType, Type destinationType)
            => Map(sourceType, destinationType, source, destination);

        public IQueryable<TDestination> ProjectTo<TSource, TDestination>(IQueryable<TSource> source)
        {
            var (mapping, provider) = GetMapping(typeof(TSource), typeof(TDestination));
            try
            {
                var l = (Expression<Func<TSource, TDestination>>)mapping.GetSelectExpression();
                return source.Select(l);
            }
            catch (Exception ex)
            {
                var invocationException = new MappingInvocationException(mapping, provider, ex);
                mappingErrorHandler?.OnError(invocationException);
                throw invocationException;
            }
        }
    }
}
