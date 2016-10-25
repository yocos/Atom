using Atom.Common;
using System.Threading;
using System;

namespace Atom.CircuitBreaker
{
    public class CircuitBreaker : ICircuitBreaker
    {

        /* track whether this circuit is open/closed at any given point in time (default to false==closed) */
        private AtomicBoolean _CircuitOpen = new AtomicBoolean(false);
        /* when the circuit was marked open or was last allowed to try a 'singleTest' */
        private AtomicLong _CircuitOpenedOrLastTestedTime = new AtomicLong();

        private CommandOptions _CommandOptions;
        private ICommandMetrics _CommandMetrics;

        public CircuitBreaker(ICommandKey key, ICommandGroupKey group, CommandOptions options, ICommandMetrics metrics)
        {
            _CommandOptions = Check.NotNull(options, nameof(options));
            _CommandMetrics = Check.NotNull(metrics, nameof(metrics));
        }

        public void MarkSuccess()
        {
            if (_CircuitOpen.Value)
            {
                if (_CircuitOpen.CompareAndSet(true, false))
                {
                    //win the thread race to reset metrics
                    //Unsubscribe from the current stream to reset the health counts stream.  This only affects the health counts view,
                    //and all other metric consumers are unaffected by the reset
                    _CommandMetrics.ResetStream();
                }
            }
        }

        public bool AllowRequest()
        {
            if (_CommandOptions.CircuitBreakerForceOpen)
            {
                // properties have asked us to force the circuit open so we will allow NO requests
                return false;
            }
            if (_CommandOptions.CircuitBreakerForceClosed)
            {
                // we still want to allow isOpen() to perform it's calculations so we simulate normal behavior
                IsOpen();
                // properties have asked us to ignore errors so we will ignore the results of isOpen and just allow all traffic through
                return true;
            }
            return !IsOpen() || allowSingleTest();

        }
        public bool allowSingleTest()
        {
            long timeCircuitOpenedOrWasLastTested = _CircuitOpenedOrLastTestedTime.Value;
            var currentTime = System.DateTime.Now.Ticks;

            // 1) if the circuit is open
            // 2) and it's been longer than 'sleepWindow' since we opened the circuit
            if (_CircuitOpen.Value && currentTime - timeCircuitOpenedOrWasLastTested > _CommandOptions.CircuitBreakerSleepWindowInMilliseconds)
            {
                // We push the 'circuitOpenedTime' ahead by 'sleepWindow' since we have allowed one request to try.
                // If it succeeds the circuit will be closed, otherwise another singleTest will be allowed at the end of the 'sleepWindow'.
                if (_CircuitOpenedOrLastTestedTime.CompareAndSet(timeCircuitOpenedOrWasLastTested, System.DateTime.Now.Ticks))
                {
                    // if this returns true that means we set the time so we'll return true to allow the singleTest
                    // if it returned false it means another thread raced us and allowed the singleTest before swe did
                    return true;
                }
            }
            return false;
        }
        public bool IsOpen()
        {

            if (_CircuitOpen.Value)
            {
                // if we're open we immediately return true and don't bother attempting to 'close' ourself as that is left to allowSingleTest and a subsequent successful test to close
                return true;
            }

            // we're closed, so let's see if errors have made us so we should trip the circuit open
            HealthCounts health = _CommandMetrics.HealthCounts;

            // check if we are past the statisticalWindowVolumeThreshold
            if (health.TotalRequests < _CommandOptions.CircuitBreakerRequestVolumeThreshold)
            {
                // we are not past the minimum volume threshold for the statisticalWindow so we'll return false immediately and not calculate anything
                return false;
            }

            if (health.ErrorPercentage < _CommandOptions.CircuitBreakerErrorThresholdPercentage)
            {
                return false;
            }
            else
            {
                // our failure rate is too high, trip the circuit
                if (_CircuitOpen.CompareAndSet(false, true))
                {
                    // if the previousValue was false then we want to set the currentTime
                    _CircuitOpenedOrLastTestedTime.SetValue(System.DateTime.Now.Ticks);
                    return true;
                }
                else
                {
                    // How could previousValue be true? If another thread was going through this code at the same time a race-condition could have
                    // caused another thread to set it to true already even though we were in the process of doing the same
                    // In this case, we know the circuit is open, so let the other thread set the currentTime and report back that the circuit is open
                    return true;
                }
            }

        }
    }
}