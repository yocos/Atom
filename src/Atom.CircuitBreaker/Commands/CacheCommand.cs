using System;
using System.Collections.Concurrent;
using Atom.CircuitBreaker.Key;

namespace Atom.CircuitBreaker.Commands
{

    public class CacheCommand
    {
        /// <summary>
        /// Cache of known command names, keyed by Type and group key. Helps
        /// avoid repeatedly generating the same Name for every distinct command
        /// instance.
        /// </summary>
        internal ConcurrentDictionary<Tuple<Type, GroupKey>, string> GeneratedNameCache = new ConcurrentDictionary<Tuple<Type, GroupKey>, string>();

        /// <summary>
        /// Cache of known command names, keyed by provided name and group key. Helps
        /// avoid repeatedly generating the same Name for every distinct command.
        /// </summary>
        internal ConcurrentDictionary<Tuple<string, GroupKey>, string> ProvidedNameCache = new ConcurrentDictionary<Tuple<string, GroupKey>, string>();

        /// <summary>
        /// Maps command names to IConfigurableValues with command timeouts.
        /// 
        /// This is only internal so that we can look at it during unit tests.
        /// </summary>
        internal ConcurrentDictionary<string, long> TimeoutConfigCache = new ConcurrentDictionary<string, long>();
    }

}
