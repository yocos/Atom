using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Atom.ServiceDiscovery;
using Atom.ServiceDiscovery.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Atom.Common;

namespace Microsoft.AspNetCore.Builder
{
    public static class ServiceDiscoveryExtensions
    {
        public static IApplicationBuilder UseServiceDiscovery(this IApplicationBuilder app, IConfiguration configuration)
        {
            var ServiceDiscovery = app.ApplicationServices.GetRequiredService<IServiceDiscoveryProvider>();
            var appLifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();

            //Register service
            ServiceDiscovery.RegisterService(configuration);

            //Unregister during application is stopping
            appLifetime.ApplicationStopping.Register(() => ServiceDiscovery.UnregisterService(configuration));

            return app;
        }

        public static IApplicationBuilder UseServiceDiscovery(this IApplicationBuilder app, string serviceName, int servicePort, string version)
        {
            var ServiceDiscovery = app.ApplicationServices.GetRequiredService<IServiceDiscoveryProvider>();
            var appLifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();
            
             //Register service
            ServiceDiscovery.RegisterService(serviceName, NetworkUtils.GetHostName(), servicePort, version);

             //Unregister during application is stopping
            appLifetime.ApplicationStopping.Register(() => ServiceDiscovery.UnregisterService(serviceName));

            return app;
        }
    }
}
