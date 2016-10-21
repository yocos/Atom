namespace Atom.CircuitBreaker.ThreadPool
{
    public interface IIsolationSemaphore
    {
        bool TryEnter();
        void Release();
    }
}
