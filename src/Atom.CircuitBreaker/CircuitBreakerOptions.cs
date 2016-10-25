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
        public Dictionary<string, GroupOptions> Groups { get; } = new Dictionary<string, GroupOptions>();
        public Dictionary<string, CommandOptions> Commands { get; } = new Dictionary<string, CommandOptions>();

    }

    public class GroupOptions
    {
        public bool CircuitBreakerForceClosed { get; set; } = false;
        public bool CircuitBreakerForceOpen { get; set; } = false;
        public int CircuitBreakerSleepWindowInMilliseconds { get; set; } = 200;
        public long CircuitBreakerRequestVolumeThreshold { get; set; }
        public int CircuitBreakerErrorThresholdPercentage { get; set; }

        public long? Timeout { get; set; }
    }

    public class CommandOptions : GroupOptions
    {
        public string Group { get; set; }

        public string Name { get; set; }
    }
}
