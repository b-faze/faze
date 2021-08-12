using Faze.Examples.Gallery.Visualisations.EightQueensProblem;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSingletons<TInterface>(this IServiceCollection services, Assembly assembly)
        {
            var interfaceType = typeof(TInterface);
            var types = assembly.GetTypes().Where(type => !type.IsInterface && !type.IsAbstract && type.GetInterfaces().Any(i => i == interfaceType)).ToArray();
            foreach (var type in types)
            {
                services.AddSingleton(interfaceType, type);
            }

            return services;
        }
    }
}
