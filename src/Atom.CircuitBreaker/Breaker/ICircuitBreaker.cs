using Atom.CircuitBreaker.Metrics;

namespace Atom.CircuitBreaker.Breaker
{
    public interface ICircuitBreaker
    {
        bool IsAllowing();
        void MarkSuccess(long elapsedMillis);

        ICommandMetrics Metrics { get; }
        string Name { get; }
    }
}