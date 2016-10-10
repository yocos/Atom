using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace µServiceSimple
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)                
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services, IApplicationLifetime applicationLifetime)
        {
            //Register configuration
            services.AddSingleton<IConfiguration>(_ => Configuration); 

            services.AddSingleton<IConfigurationRoot>(_ => Configuration); 

            services.AddSingleton<IApplicationLifetime>(applicationLifetime);
            
            // Add framework services.
            services.AddMvc();
            
            //Add microservice Atom Framework
            services.AddAtom();

            //Add microservice Consul Discovery Server
            services.AddConsul();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApplicationLifetime applicationLifetime, IHostingEnvironment env, ILoggerFactory loggerFactory, IConfiguration configuration)
        {            
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

<<<<<<< HEAD
            app.UseMvc()                 
=======
            app.UseMvc()                
>>>>>>> 8f5cdf63f427872bc96b5f2adb0cad750165606c
               .UseServiceDiscovery(configuration); // Register The Service Discovery Server

            // app.UseMonitoring(configuration)
        }
    }
}
