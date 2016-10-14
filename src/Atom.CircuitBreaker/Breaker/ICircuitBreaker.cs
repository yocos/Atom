using Atom.CircuitBreaker.Metrics;

namespace Atom.CircuitBreaker.Breaker
{
    internal interface ICircuitBreaker
    {
        bool IsAllowing();
        void MarkSuccess(long elapsedMillis);

        ICommandMetrics Metrics { get; }
        string Name { get; }
    }
}