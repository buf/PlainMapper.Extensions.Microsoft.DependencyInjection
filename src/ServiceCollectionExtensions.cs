namespace PlainMapper.Extensions.Microsoft.DependencyInjection
{
    using System.Reflection;
    using global::Microsoft.Extensions.DependencyInjection;
    using Interfaces;

    public static class ServiceCollectionExtensions
    {
        public static void AddMappings(this IServiceCollection services, params Assembly[] assemblies)
        {
            var mappingTypes = AssemblyExtensions.GetMappings(assemblies);
            foreach (var type in mappingTypes)
            {
                services.Add(new ServiceDescriptor(type.InterfaceType, type.ClassType, ServiceLifetime.Transient));
            }

            services.AddScoped<IMapper, Mapper>();
        }
    }
}
