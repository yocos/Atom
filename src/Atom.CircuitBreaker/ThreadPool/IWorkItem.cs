using System;
using System.Threading;

namespace Atom.CircuitBreaker.ThreadPool
{
    public interface IWorkItem<TResult>
    {
        TResult Get(CancellationToken cancellationToken, TimeSpan timeout);
    }
}