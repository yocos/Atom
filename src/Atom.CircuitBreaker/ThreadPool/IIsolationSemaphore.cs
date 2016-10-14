namespace Atom.CircuitBreaker.ThreadPool
{
    internal interface IIsolationSemaphore
    {
        bool TryEnter();
        void Release();
    }
}
