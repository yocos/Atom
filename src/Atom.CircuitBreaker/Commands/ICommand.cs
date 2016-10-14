using System.Threading.Tasks;

namespace Atom.CircuitBreaker.Commands
{
    /// <see cref="Command"/>
    /// <typeparam name="TResult">The type of the result returned by the Command's execution.</typeparam>
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