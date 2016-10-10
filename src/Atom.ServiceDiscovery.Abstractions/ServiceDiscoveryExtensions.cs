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
using Microsoft.AspNetCore.Hosting;

namespace Microsoft.AspNetCore.Builder
{
    
    /// <summary>
    /// Extensions methods for IApplicationBuilder
    /// </summary>
    public static class ServiceDiscoveryExtensions    
    {
        /// <summary>
        /// Activate Service Discovery from configuration
        /// </summary>
        /// <param name="app">The Application Builder.</param>
        /// <param name="configuration">The configuration needed to load Service Discovery setting.</param>
        /// <returns>The ApplicationBuilder</returns>
        public static IApplicationBuilder UseServiceDiscovery(this IApplicationBuilder app, IConfiguration configuration)
        {
            var ServiceDiscovery = app.ApplicationServices.GetRequiredService<IServiceDiscoveryProvider>();
            var applicationLifeTime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();

            //Register service
            ServiceDiscovery.RegisterService(configuration);

            applicationLifeTime.ApplicationStopping.Register(() => {ServiceDiscovery.UnregisterService(configuration);});

            return app;
        }

        /// <summary>
        /// Activate Service Discovery with custom configuration
        /// </summary>
        /// <param name="app">The Application Builder.</param>
        /// <param name="serviceName">The service name</param>
        /// <param name="servicePort">The port exposed</param>
        /// <param name="version">The service version</param>
        /// <returns>The ApplicationBuilder</returns>
        public static IApplicationBuilder UseServiceDiscovery(this IApplicationBuilder app, string serviceName, int servicePort, string version)
        {
            var ServiceDiscovery = app.ApplicationServices.GetRequiredService<IServiceDiscoveryProvider>();
<<<<<<< HEAD
            var appLifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();
            
             //Register service
            ServiceDiscovery.RegisterService(serviceName, NetworkUtils.GetHostName(), servicePort, version);

             //Unregister during application is stopping
            appLifetime.ApplicationStopping.Register(() => ServiceDiscovery.UnregisterService(serviceName));

=======
              var applicationLifeTime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();
            
            ServiceDiscovery.RegisterService(serviceName, NetworkUtils.GetHostName(), servicePort, version);

            applicationLifeTime.ApplicationStopping.Register(() => {ServiceDiscovery.UnregisterService(serviceName);});
>>>>>>> 8f5cdf63f427872bc96b5f2adb0cad750165606c
            return app;
        }
    }
}
