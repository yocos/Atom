using Atom.CircuitBreaker;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Atom.CircuitBreaker.Commands;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AtomCircuitBreakerExtensions
    {
        public static IServiceCollection AddCircuitBreaker(this IServiceCollection services, IConfiguration configuration)
        {
            //Load Circuit Breaker configuration
            services.Configure<CircuitBreakerOptions>(configuration.GetSection("circuitBreaker"));
            services.AddSingleton<IOptions<CircuitBreakerOptions>, OptionsWrapper<CircuitBreakerOptions>>();

            //Register the circuitBreaker Context 
            //==> TODO register after ending the Mjolnir rewriting
            // services.AddSingleton<ICommandContext, CommandContext>();
            // services.AddTransient<ICircuitBreakerInvoker, CircuitBreakerInvoker>();

            //Register all framework services
            return services;
        }
    }
}
