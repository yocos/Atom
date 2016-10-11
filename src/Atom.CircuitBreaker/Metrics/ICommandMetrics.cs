namespace Atom.CircuitBreaker.Metrics
{
    internal interface ICommandMetrics
    {
        MetricsSnapshot GetSnapshot();
        void Reset();

        void MarkCommandSuccess(/*TimeSpan duration*/); // TODO rob.hruska 11/8/2013 - Durations?
        void MarkCommandFailure(/*TimeSpan duration*/);
               
    }
}
