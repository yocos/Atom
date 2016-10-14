using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Atom.CircuitBreaker.Commands
{
    /// <summary>
    /// An asynchronous command.
    /// 
    /// <seealso cref="SyncCommand{TResult}"/>
    /// 
    /// If you need both synchronous and asynchronous versions of a command, it's recommended that
    /// you implement both separately, rather than implementing one and using "conversion" code
    /// like Task.FromResult() or .Result/.Wait() (which are dangerous and can deadlock).
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned by this command's execution.</typeparam>
    public abstract class AsyncCommand<TResult> : BaseCommand
    {
        /// <summary>
        /// Constructs the command.
        /// 
        /// The group is used as part of the command's <see cref="Name">Name</see>.
        /// If the group contains dots, they'll be converted to dashes.
        /// 
        /// The provided <code>isolationKey</code> will be used as both the
        /// breaker and bulkhead keys.
        /// 
        /// Command timeouts can be configured at runtime. See the Mjolnir wiki at
        /// https://github.com/hudl/Mjolnir/wiki for configuration information.
        /// If not configured, the provided <code>defaultTimeout</code> will be used.
        /// 
        /// </summary>
        /// <param name="group">Logical grouping for the command, usually the owning team. Avoid using dots.</param>
        /// <param name="isolationKey">Breaker and bulkhead key to use.</param>
        /// <param name="defaultTimeout">Timeout to enforce if not otherwise provided.</param>
        protected AsyncCommand(string group, string isolationKey, TimeSpan defaultTimeout, CacheCommand cacheCommand, IConfiguration configuration)
            : base(group, isolationKey, isolationKey, cacheCommand, configuration, defaultTimeout)
        { }

        /// <summary>
        /// Constructs the command.
        /// 
        /// The group is used as part of the command's <see cref="Name">Name</see>.
        /// If the group contains dots, they'll be converted to dashes.
        /// 
        /// Command timeouts can be configured at runtime. See the Mjolnir wiki at
        /// https://github.com/hudl/Mjolnir/wiki for configuration information.
        /// If not configured, the provided <code>defaultTimeout</code> will be used.
        /// 
        /// </summary>
        /// <param name="group">Logical grouping for the command, usually the owning team. Avoid using dots.</param>
        /// <param name="breakerKey">Breaker to use for this command.</param>
        /// <param name="bulkheadKey">Bulkhead to use for this command.</param>
        /// <param name="defaultTimeout">Timeout to enforce if not otherwise provided.</param>
        protected AsyncCommand(string group, string breakerKey, string bulkheadKey, TimeSpan defaultTimeout, CacheCommand cacheCommand, IConfiguration configuration)
            : base(group, breakerKey, bulkheadKey, cacheCommand, configuration, defaultTimeout)
        { }

        /// <summary>
        /// The operation that should be performed when this command is invoked.
        /// 
        /// If this method throws an Exception, the command's execution will be tracked as a
        /// failure with its circuit breaker. Otherwise, it will be considered successful.
        /// </summary>
        /// <param name="cancellationToken">
        ///     Token used to cancel and detect cancellation of the Command. The token may be
        ///     provided by the Invoke call, configuration, or the default timeout given to the
        ///     command's constructor.
        /// </param>
        /// <returns>A Task providing the command's result.</returns>
        public abstract Task<TResult> ExecuteAsync(CancellationToken cancellationToken);
    }
}