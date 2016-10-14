using System;
using System.Collections.Concurrent;

namespace Atom.CircuitBreaker.Commands
{
    public class CircuitBreakingSetting
    {
        public bool UseCircuitBreakers { get; set; } = true;

        /// <summary>
        /// If this is set to true then all calls wrapped in a Mjolnir command will ignore the default timeout.
        /// This is likely to be useful when debugging Command decorated methods, however it is not advisable to use in a production environment since it disables 
        /// some of Mjolnir's key features. 
        /// </summary>
        public bool IgnoreCommandTimeouts { get; set; } = false;
    }
}