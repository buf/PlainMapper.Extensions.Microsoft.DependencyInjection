namespace PlainMapper.Extensions.Microsoft.DependencyInjection
{
    using System.Linq;
    using System.Reflection;
    using global::Microsoft.Extensions.DependencyInjection;
    using Interfaces;

    public static class ServiceCollectionExtensions
    {
        public static void AddMappings(this IServiceCollection services, params Assembly[] assemblies)
        {
            var mapperType = typeof(IMapping<,>);
            var mappingTypes = assemblies.SelectMany(x => x.GetTypes())
                .Where(x => x.GetInterfaces().Any(i => i.IsGenericType && i.Name == mapperType.Name))
                .Where(x => x.IsClass && !x.IsAbstract)
                .ToDictionary(x => x, x => x.GetInterface(mapperType.Name));

            foreach (var (key, value) in mappingTypes)
            {
                services.Add(new ServiceDescriptor(value, key, ServiceLifetime.Transient));
            }

            services.AddScoped<IMapper, Mapper>();
        }
    }
}
