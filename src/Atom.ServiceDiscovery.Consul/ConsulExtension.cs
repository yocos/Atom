using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atom.ServiceDiscovery.Abstractions;
using Atom.ServiceDiscovery.Consul;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AtomExtensions
    {
        public static IServiceCollection AddConsul(this IServiceCollection services)
        {
            //Register consul Provider
            services.AddSingleton<IServiceDiscoveryProvider, ConsulServiceDiscoveryProvider>();

            return services;
        }
    }
}
