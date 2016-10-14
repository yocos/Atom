using System.Collections.Generic;
using System;
using Atom.CircuitBreaker.Key;
using Atom.CircuitBreaker.External;
using Atom.CircuitBreaker.Breaker;
using Atom.CircuitBreaker.ThreadPool;
using Hudl.Mjolnir.Bulkhead;

namespace Atom.CircuitBreaker.Commands
{
    internal interface ICommandContext
    {
        IMetricEvents MetricEvents { get; set; }
        void IgnoreExceptions(HashSet<Type> types);
        bool IsExceptionIgnored(Type type);
        ICircuitBreaker GetCircuitBreaker(GroupKey key);
        IIsolationThreadPool GetThreadPool(GroupKey key);
        IBulkheadSemaphore GetBulkhead(GroupKey key);
        IIsolationSemaphore GetFallbackSemaphore(GroupKey key);
    }
}