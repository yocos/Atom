using System;
using System.Threading.Tasks;

namespace Atom.CircuitBreaker.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommand<TResult>
    {
        /// <summary>
        /// Invoke the Command synchronously. See <see cref="Command#Invoke()"/>.
        /// </summary>
        TResult Invoke();

        /// <summary>
        /// Invoke the Command asynchronously. See <see cref="Command#InvokeAsync()"/>.
        /// </summary>
        Task<TResult> InvokeAsync();
    }
}