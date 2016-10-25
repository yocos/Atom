namespace Atom.CircuitBreaker
{
    public interface ICircuitBreaker
    {

        /// <summary>
        ///Invoked on successful executions from {@link Command} as part of feedback mechanism when in a half-open state.
        /// </summary>
        void MarkSuccess();

        /// <summary>
        /// Every {@link HystrixCommand} requests asks this if it is allowed to proceed or not.
        /// <p>
        /// This takes into account the half-open logic which allows some requests through when determining if it should be closed again.
        /// </summary>
        /// <returns>boolean whether a request should be permitted</returns>
        bool AllowRequest();

        /// <summary>
        /// Whether the circuit is currently open (tripped).
        /// </summary>         
        /// <returns>boolean state of circuit breaker</returns>
        bool IsOpen();

    }
}