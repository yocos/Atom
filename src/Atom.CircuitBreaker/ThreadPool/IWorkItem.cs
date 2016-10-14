using System;
using System.Threading;

namespace Atom.CircuitBreaker.ThreadPool
{
    internal interface IWorkItem<TResult>
    {
        TResult Get(CancellationToken cancellationToken, TimeSpan timeout);
    }
}