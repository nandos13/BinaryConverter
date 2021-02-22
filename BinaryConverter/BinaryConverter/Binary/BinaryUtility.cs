using System;

namespace JPAssets.Binary
{
    public static class BinaryUtility
    {
        internal static void CheckEndianness(Endianness value, string paramName)
        {
            if (value != Endianness.Little && value != Endianness.Big)
                throw new ArgumentOutOfRangeException(paramName);
        }

        private static void SwapBytes(ref byte b0, ref byte b1)
        {
            byte temp = b0;
            b0 = b1;
            b1 = temp;
        }

        /// <summary>
        /// Reverses the endianness of abritrary binary data.
        /// </summary>
        /// <param name="ptr">A pointer to the first byte of the binary value.</param>
        /// <param name="count">The number of bytes to process.</param>
        private static unsafe void ReverseEndiannessInternal(byte* ptr, int count)
        {
            int halfCount = count / 2;
            int upperBound = count - 1;

            for (int i = 0; i < halfCount; i++)
            {
                SwapBytes(ref ptr[i], ref ptr[upperBound - i]);
            }
        }

        /// <inheritdoc cref="BinaryUtility.ReverseEndiannessInternal(byte*, int)"/>
        /// <inheritdoc cref="ValidationUtility.CheckCount(int)"/>
        public static unsafe void ReverseEndianness(byte* ptr, int count)
        {
            ValidationUtility.CheckCount(count);
            ReverseEndiannessInternal(ptr, count);
        }

        /// <param name="bytes">An array of bytes for which to reverse the endianness.</param>
        /// <param name="offset">Offset of the start index.</param>
        /// <inheritdoc cref="BinaryUtility.ReverseEndiannessInternal(byte*, int)"/>
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

        /// <inheritdoc cref="BinaryUtility.ReverseEndianness(byte[], int, int)"/>
        public static unsafe void ReverseEndianness(byte[] bytes)
        {
            _ = bytes ?? throw new ArgumentNullException(nameof(bytes));

            Array.Reverse(bytes);
        }

        /// <param name="b0">This byte is swapped with <paramref name="b1"/>.</param>
        /// <param name="b1">This byte is swapped with <paramref name="b0"/>.</param>
        /// <inheritdoc cref="ReverseEndiannessInternal(byte*, int)"/>
        public static void ReverseEndianness(ref byte b0, ref byte b1)
        {
            SwapBytes(ref b0, ref b1);
        }

        /// <param name="b0">This byte is swapped with <paramref name="b3"/>.</param>
        /// <param name="b1">This byte is swapped with <paramref name="b2"/>.</param>
        /// <param name="b2">This byte is swapped with <paramref name="b1"/>.</param>
        /// <param name="b3">This byte is swapped with <paramref name="b0"/>.</param>
        /// <inheritdoc cref="ReverseEndianness(ref byte, ref byte)"/>
        public static void ReverseEndianness(ref byte b0, ref byte b1, ref byte b2, ref byte b3)
        {
            SwapBytes(ref b0, ref b3);
            SwapBytes(ref b1, ref b2);
        }

        /// <param name="b0">This byte is swapped with <paramref name="b7"/>.</param>
        /// <param name="b1">This byte is swapped with <paramref name="b6"/>.</param>
        /// <param name="b2">This byte is swapped with <paramref name="b5"/>.</param>
        /// <param name="b3">This byte is swapped with <paramref name="b4"/>.</param>
        /// <param name="b4">This byte is swapped with <paramref name="b3"/>.</param>
        /// <param name="b5">This byte is swapped with <paramref name="b2"/>.</param>
        /// <param name="b6">This byte is swapped with <paramref name="b1"/>.</param>
        /// <param name="b7">This byte is swapped with <paramref name="b0"/>.</param>
        /// <inheritdoc cref="ReverseEndianness(ref byte, ref byte)"/>
        public static void ReverseEndianness(ref byte b0, ref byte b1, ref byte b2, ref byte b3, ref byte b4, ref byte b5, ref byte b6, ref byte b7)
        {
            SwapBytes(ref b0, ref b7);
            SwapBytes(ref b1, ref b6);
            SwapBytes(ref b2, ref b5);
            SwapBytes(ref b3, ref b4);
        }

        /// <inheritdoc cref="Binary16.Reverse"/>
        public static Binary16 ReverseEndianness(Binary16 bin)
        {
            return bin.Reverse();
        }

        /// <inheritdoc cref="Binary32.Reverse"/>
        public static Binary32 ReverseEndianness(Binary32 bin)
        {
            return bin.Reverse();
        }

        /// <inheritdoc cref="Binary64.Reverse"/>
        public static Binary64 ReverseEndianness(Binary64 bin)
        {
            return bin.Reverse();
        }
    }
}
