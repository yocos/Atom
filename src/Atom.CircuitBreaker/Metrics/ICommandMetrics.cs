namespace Atom.CircuitBreaker
{
    public interface ICommandMetrics
    {
        void ResetStream();

        HealthCounts HealthCounts { get; }
    }



    public class HealthCounts
    {
        public int TotalRequests { get; internal set; }
        public int ErrorPercentage { get; }
    }
}