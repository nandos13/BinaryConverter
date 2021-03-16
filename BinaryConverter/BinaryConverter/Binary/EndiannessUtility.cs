using System;

namespace JPAssets.Binary
{
    public static class EndiannessUtility
    {
        /// <summary>
        /// Reverses the endianness of abritrary binary data.
        /// </summary>
        /// <param name="ptr">A pointer to the first byte of the binary value.</param>
        /// <param name="count">The number of bytes to process.</param>
        private static unsafe void ReverseEndiannessInternal(byte* ptr, int count)
        {
            int halfCount = count / 2;
            int inverse = count - 1;

            for (int i = 0; i < halfCount; i++, inverse--)
            {
                byte temp = ptr[i];

                ptr[i] = ptr[inverse];
                ptr[inverse] = temp;
            }
        }

        /// <inheritdoc cref="EndiannessUtility.ReverseEndiannessInternal(byte*, int)"/>
        /// <inheritdoc cref="ValidationUtility.CheckCount(int)"/>
        public static unsafe void ReverseEndianness(byte* ptr, int count)
        {
            ValidationUtility.CheckCount(count);
            ReverseEndiannessInternal(ptr, count);
        }

        /// <param name="bytes">An array of bytes for which to reverse the endianness.</param>
        /// <param name="offset">Offset of the start index.</param>
        /// <inheritdoc cref="EndiannessUtility.ReverseEndiannessInternal(byte*, int)"/>
        /// /// <inheritdoc cref="ValidationUtility.CheckArrayOffsetAndCount{T}(T[], string, int, int)"/>
        public static unsafe void ReverseEndianness(byte[] bytes, int offset, int count)
        {
            ValidationUtility.CheckArrayOffsetAndCount(bytes, nameof(bytes), offset, count);

            if (offset == 0 && count == bytes.Length)
            {
                Array.Reverse(bytes);
            }
            else
            {
                fixed (byte* ptr = bytes)
                    ReverseEndiannessInternal(ptr + offset, count);
            }
        }

        /// <inheritdoc cref="EndiannessUtility.ReverseEndianness(byte[], int, int)"/>
        public static unsafe void ReverseEndianness(byte[] bytes)
        {
            Array.Reverse(bytes ?? throw new ArgumentNullException(nameof(bytes)));
        }

        /// <summary>
        /// Returns the given value with reversed endianness.
        /// </summary>
        /// <typeparam name="T">The type of unmanaged data to operate on.</typeparam>
        /// <param name="value">The value to reverse.</param>
        internal static unsafe T ReverseEndianness<T>(T value) where T : unmanaged
        {
            ReverseEndiannessInternal((byte*)&value, sizeof(T));
            return value;
        }
    }
}
