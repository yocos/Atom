using Microsoft.Extensions.Configuration;

namespace Atom.ServiceDiscovery.Abstractions
{
    public interface IServiceDiscoveryProvider
    {
        void RegisterService(IConfiguration configuration);        

        void RegisterService(string serviceName, string hostname, int servicePort, string version);
        
        void UnregisterService(IConfiguration configuration);
        
        void unRegisterService(string serviceName);        
    }
}
