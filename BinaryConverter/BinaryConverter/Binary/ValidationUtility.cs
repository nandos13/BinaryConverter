using System;

namespace JPAssets.Binary
{
    internal static class ValidationUtility
    {
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 0.</exception>
        internal static void CheckCount(int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        }

        /// <exception cref="ArgumentNullException">
        /// <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="offset"/> or <paramref name="count"/> is less than 0.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="offset"/> and <paramref name="count"/> do not specify a valid range in <paramref name="array"/>.
        /// </exception>
        internal static void CheckArrayOffsetAndCount<T>(T[] array, string arrayParamName, int offset, int count)
        {
            _ = array ?? throw new ArgumentNullException(arrayParamName);

            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            int length = array.Length;

            if (offset >= length || count > length - offset)
                throw new ArgumentException($"Offset and count do not specify a valid range in the array.", nameof(offset));
        }
    }
}
