namespace System.Threading
{
    /// <summary>
    /// Provides non-blocking, thread-safe access to a long valueB.
    /// </summary>
    public class AtomicLong
    {
        #region Member Variables

        private long _currentValue;

        #endregion

        #region Constructor

        public AtomicLong(long initialValue)
        {
            _currentValue = initialValue;
        }

        public AtomicLong()
        {
            _currentValue = long.MinValue;
        }

        #endregion       

        #region Public Properties and Methods

        public long Value
        {
            get
            {
                return Interlocked.Add(ref _currentValue, 0);
            }
        }

        /// <summary>
        /// Sets the long value.
        /// </summary>
        /// <param name="newValue"></param>
        /// <returns>The original value.</returns>
        public long SetValue(long newValue)
        {
            return
            Interlocked.Exchange(ref _currentValue, newValue);
        }

        /// <summary>
        /// Compares with expected value and if same, assigns the new value.
        /// </summary>
        /// <param name="expectedValue"></param>
        /// <param name="newValue"></param>
        /// <returns>True if able to compare and set, otherwise false.</returns>
        public bool CompareAndSet(long expectedValue,
            long newValue)
        {
            return Interlocked.CompareExchange(ref _currentValue, newValue, expectedValue) == expectedValue;
        }

        #endregion

    }
}