using System.Threading;

namespace Atom.CircuitBreaker.Metrics
{
    internal class InterlockingLongCounter : ILongCounter
    {
        private long _count;

        public void Increment()
        {
            Interlocked.Increment(ref _count);
        }

        public long Get()
        {
            return Interlocked.Read(ref _count);
        }
    }
}