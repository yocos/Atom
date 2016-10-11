namespace Atom.CircuitBreaker.Metrics
{
    internal interface ILongCounter
    {
        void Increment();
        long Get();
    }
}