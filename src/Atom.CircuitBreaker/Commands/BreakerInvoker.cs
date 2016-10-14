using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Atom.CircuitBreaker.Breaker;
using Atom.CircuitBreaker.Commands;

namespace Atom.CircuitBreker.Commands
{
    /// <summary>
    /// Executes a command on a circuit breaker.
    /// </summary>
    internal interface IBreakerInvoker
    {
        Task<TResult> ExecuteWithBreakerAsync<TResult>(AsyncCommand<TResult> command, CancellationToken ct);
        TResult ExecuteWithBreaker<TResult>(SyncCommand<TResult> command, CancellationToken ct);
    }

    internal class BreakerInvoker : IBreakerInvoker
    {
        private readonly ICommandContext _context;

        public BreakerInvoker(ICommandContext context)
        {
            if (context == null)
                throw new ArgumentException(nameof(context));

            _context = context;
        }

        public async Task<TResult> ExecuteWithBreakerAsync<TResult>(AsyncCommand<TResult> command, CancellationToken ct)
        {
            var breaker = _context.GetCircuitBreaker(command.BreakerKey);

            if (!breaker.IsAllowing())
            {
                _context.MetricEvents.RejectedByBreaker(breaker.Name, command.Name);
                throw new CircuitBreakerRejectedException();
            }

            TResult result;

            var success = true;
            var breakerStopwatch = Stopwatch.StartNew();
            var executionStopwatch = Stopwatch.StartNew();
            try
            {
                // Await here so we can catch the Exception and track the state.
                result = await command.ExecuteAsync(ct).ConfigureAwait(false);
                executionStopwatch.Stop();

                breaker.MarkSuccess(breakerStopwatch.ElapsedMilliseconds);
                breaker.Metrics.MarkCommandSuccess();
            }
            catch (Exception e)
            {
                executionStopwatch.Stop();
                success = false;

                if (_context.IsExceptionIgnored(e.GetType()))
                {
                    success = true;
                    breaker.MarkSuccess(breakerStopwatch.ElapsedMilliseconds);
                    breaker.Metrics.MarkCommandSuccess();
                }
                else
                {
                    breaker.Metrics.MarkCommandFailure();
                }

                throw;
            }
            finally
            {
                command.ExecutionTimeMillis = executionStopwatch.Elapsed.TotalMilliseconds;

                if (success)
                {
                    _context.MetricEvents.BreakerSuccessCount(breaker.Name, command.Name);
                }
                else
                {
                    _context.MetricEvents.BreakerFailureCount(breaker.Name, command.Name);
                }
            }

            return result;
        }

        public TResult ExecuteWithBreaker<TResult>(SyncCommand<TResult> command, CancellationToken ct)
        {
            var breaker = _context.GetCircuitBreaker(command.BreakerKey);

            if (!breaker.IsAllowing())
            {
                _context.MetricEvents.RejectedByBreaker(breaker.Name, command.Name);
                throw new CircuitBreakerRejectedException();
            }

            TResult result;

            var success = true;
            var breakerStopwatch = Stopwatch.StartNew();
            var executionStopwatch = Stopwatch.StartNew();
            try
            {
                result = command.Execute(ct);
                executionStopwatch.Stop();

                breaker.MarkSuccess(breakerStopwatch.ElapsedMilliseconds);
                breaker.Metrics.MarkCommandSuccess();
            }
            catch (Exception e)
            {
                executionStopwatch.Stop();
                success = false;

                if (_context.IsExceptionIgnored(e.GetType()))
                {
                    success = true;
                    breaker.MarkSuccess(breakerStopwatch.ElapsedMilliseconds);
                    breaker.Metrics.MarkCommandSuccess();
                }
                else
                {
                    breaker.Metrics.MarkCommandFailure();
                }

                throw;
            }
            finally
            {
                command.ExecutionTimeMillis = executionStopwatch.Elapsed.TotalMilliseconds;

                if (success)
                {
                    _context.MetricEvents.BreakerSuccessCount(breaker.Name, command.Name);
                }
                else
                {
                    _context.MetricEvents.BreakerFailureCount(breaker.Name, command.Name);
                }
            }

            return result;
        }
    }
}
