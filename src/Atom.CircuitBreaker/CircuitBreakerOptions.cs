using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Atom.CircuitBreaker
{
    public class CircuitBreakerOptions
    {
        public bool UseCircuitBreakers { get; set; } = true;
        public bool IgnoreTimeouts { get; set; } = true;
        public int Timeout { get; set; } = 2000;
        public int Bulkhead { get; set; } = 10;

        public Dictionary<string, GroupOptions> Groups { get; } = new Dictionary<string, GroupOptions>();
        public Dictionary<string, CommandOptions> Commands { get; } = new Dictionary<string, CommandOptions>();

    }

    public class GroupOptions
    {
        public long? Timeout { get; set; }
    }

    public class CommandOptions : GroupOptions
    {
        public string Group { get; set; }

        public string Name { get; set; }
    }
}
