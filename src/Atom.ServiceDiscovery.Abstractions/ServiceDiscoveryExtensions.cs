using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Atom.ServiceDiscovery;
using Atom.ServiceDiscovery.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Atom.Common;

namespace Microsoft.AspNetCore.Builder
{
    public static class ServiceDiscoveryExtensions
    {
        public static IApplicationBuilder UseServiceDiscovery(this IApplicationBuilder app, IConfiguration configuration)
        {
            var ServiceDiscovery = app.ApplicationServices.GetRequiredService<IServiceDiscoveryProvider>();

            ServiceDiscovery.RegisterService(configuration);

            return app;
        }

        public static IApplicationBuilder UseServiceDiscovery(this IApplicationBuilder app, string serviceName, int servicePort, string version)
        {
            var ServiceDiscovery = app.ApplicationServices.GetRequiredService<IServiceDiscoveryProvider>();
            
            
            ServiceDiscovery.RegisterService(serviceName, NetworkUtils.GetHostName(), servicePort, version);

            return app;
        }
    }
}
