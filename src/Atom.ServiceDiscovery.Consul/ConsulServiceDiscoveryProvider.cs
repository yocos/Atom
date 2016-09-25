using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atom.ServiceDiscovery.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Atom.ServiceDiscovery.Consul
{
    public class ConsulServiceDiscoveryProvider: IServiceDiscoveryProvider
    {
        public ConsulServiceDiscoveryProvider()
        {
        }

        public void RegisterService(IConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public void RegisterService(string serviceName, string hostname, int servicePort, string version)
        {
            throw new NotImplementedException();
        }

        public void UnregisterService(IConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public void unRegisterService(string serviceName)
        {
            throw new NotImplementedException();
        }
    }
}
