using System.Collections.Generic;
using System;
using Atom.CircuitBreaker.Key;
using Atom.CircuitBreaker.External;
using Atom.CircuitBreaker.Breaker;
using Atom.CircuitBreaker.ThreadPool;
using Microsoft.Extensions.Options;

namespace Atom.CircuitBreaker.Commands
{
    public interface ICommandContext
    {
        /// <summary>
        /// Circuit Breaker Options
        /// </summary>
        /// <returns>The current Options</returns>
        IOptionsMonitor<CircuitBreakerOptions> Options { get; }

        IMetricEvents MetricEvents { get; set; }
        void IgnoreExceptions(HashSet<Type> types);
        bool IsExceptionIgnored(Type type);
        ICircuitBreaker GetCircuitBreaker(GroupKey key);
        IIsolationThreadPool GetThreadPool(GroupKey key);
        //        IBulkheadSemaphore GetBulkhead(GroupKey key);
        IIsolationSemaphore GetFallbackSemaphore(GroupKey key);
    }
}