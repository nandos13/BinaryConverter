using System;

namespace JPAssets.Binary
{
    public static class BinaryUtility
    {
        private static unsafe void CheckSize<T>(int expected) where T : unmanaged
        {
            if (sizeof(T) != expected)
                throw new ArgumentException($"Expected generic argument of size {expected.ToString()}.", nameof(T));
        }

        internal static unsafe void ExtractBytes<T>(T value, out byte b0, out byte b1)
            where T : unmanaged
        {
            CheckSize<T>(2);

            var bytes = (byte*)&value;

            b0 = bytes[0];
            b1 = bytes[1];
        }

        internal static unsafe void ExtractBytes<T>(T value, out byte b0, out byte b1, out byte b2, out byte b3)
            where T : unmanaged
        {
            CheckSize<T>(4);

            var bytes = (byte*)&value;

            b0 = bytes[0];
            b1 = bytes[1];
            b2 = bytes[2];
            b3 = bytes[3];
        }

        internal static unsafe void ExtractBytes<T>(T value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7)
            where T : unmanaged
        {
            CheckSize<T>(8);

            var bytes = (byte*)&value;

            b0 = bytes[0];
            b1 = bytes[1];
            b2 = bytes[2];
            b3 = bytes[3];
            b4 = bytes[4];
            b5 = bytes[5];
            b6 = bytes[6];
            b7 = bytes[7];
        }

        internal static unsafe T ToData<T>(byte b0, byte b1)
            where T : unmanaged
        {
            CheckSize<T>(2);

            var bytes = stackalloc byte[2] { b0, b1 };
            return *(T*)bytes;
        }

        internal static unsafe T ToData<T>(byte b0, byte b1, byte b2, byte b3)
            where T : unmanaged
        {
            CheckSize<T>(4);

            var bytes = stackalloc byte[4] { b0, b1, b2, b3 };
            return *(T*)bytes;
        }

        internal static unsafe T ToData<T>(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
            where T : unmanaged
        {
            CheckSize<T>(8);

            var bytes = stackalloc byte[8] { b0, b1, b2, b3, b4, b5, b6, b7 };
            return *(T*)bytes;
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
            Array.Reverse(bytes ?? throw new ArgumentNullException(nameof(bytes)));
        }

        /// <summary>
        /// Returns the given value with reversed endianness.
        /// </summary>
        /// <typeparam name="T">The type of unmanaged data to operate on.</typeparam>
        /// <param name="value">The value to reverse.</param>
        public static unsafe T ReverseEndianness<T>(T value) where T : unmanaged
        {
            ReverseEndiannessInternal((byte*)&value, sizeof(T));
            return value;
        }
    }
}
