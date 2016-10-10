namespace Microsoft.Extensions.DependencyInjection
{
    public static class AtomDependencyInjectionExtensions
    {
        public static IServiceCollection AddAtom(this IServiceCollection services)
        {
            //Register all framework services
            return services;
        }
    }
}
