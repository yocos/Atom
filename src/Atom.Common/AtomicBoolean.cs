namespace System.Threading
{
    /// <summary>
    /// Provides non-blocking, thread-safe access to a boolean valueB.
    /// </summary>
    public class AtomicBoolean
    {
        #region Member Variables

        private const int VALUE_TRUE = 1;
        private const int VALUE_FALSE = 0;

        private int _currentValue;

        #endregion

        #region Constructor

        public AtomicBoolean(bool initialValue)
        {
            _currentValue = BoolToInt(initialValue);
        }

        #endregion

        #region Private Methods

        private int BoolToInt(bool value)
        {
            return value ? VALUE_TRUE : VALUE_FALSE;
        }

        private bool IntToBool(int value)
        {
            return value == VALUE_TRUE;
        }

        #endregion

        #region Public Properties and Methods

        public bool Value
        {
            get
            {
                return IntToBool(Interlocked.Add(
                ref _currentValue, 0));
            }
        }

        /// <summary>
        /// Sets the boolean value.
        /// </summary>
        /// <param name="newValue"></param>
        /// <returns>The original value.</returns>
        public bool SetValue(bool newValue)
        {
            return IntToBool(
            Interlocked.Exchange(ref _currentValue,
            BoolToInt(newValue)));
        }

        /// <summary>
        /// Compares with expected value and if same, assigns the new value.
        /// </summary>
        /// <param name="expectedValue"></param>
        /// <param name="newValue"></param>
        /// <returns>True if able to compare and set, otherwise false.</returns>
        public bool CompareAndSet(bool expectedValue,
            bool newValue)
        {
            int expectedVal = BoolToInt(expectedValue);
            int newVal = BoolToInt(newValue);
            return Interlocked.CompareExchange(
            ref _currentValue, newVal, expectedVal) == expectedVal;
        }

        #endregion

    }
}