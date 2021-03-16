using System;

namespace JPAssets.Binary
{
    internal static class BinaryConversionUtility
    {
        private static unsafe void CheckDataSize<T>(int expected) where T : unmanaged
        {
            if (sizeof(T) != expected)
                throw new ArgumentException($"Expected generic argument of size {expected.ToString()}.", nameof(T));
        }

        /// <summary>
        /// Extract the bytes that make up the binary data of the given value. Bytes are returned
        /// in memory-order, where <paramref name="b0"/> is the lowest memory address, etc.
        /// </summary>
        internal static unsafe void ExtractBytes<T>(T value, out byte b0, out byte b1)
            where T : unmanaged
        {
            CheckDataSize<T>(2);

            var bytes = (byte*)&value;

            b0 = bytes[0];
            b1 = bytes[1];
        }

        /// <inheritdoc cref="ExtractBytes{T}(T, out byte, out byte)"/>
        internal static unsafe void ExtractBytes<T>(T value, out byte b0, out byte b1, out byte b2, out byte b3)
            where T : unmanaged
        {
            CheckDataSize<T>(4);

            var bytes = (byte*)&value;

            b0 = bytes[0];
            b1 = bytes[1];
            b2 = bytes[2];
            b3 = bytes[3];
        }

        /// <inheritdoc cref="ExtractBytes{T}(T, out byte, out byte)"/>
        internal static unsafe void ExtractBytes<T>(T value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7)
            where T : unmanaged
        {
            CheckDataSize<T>(8);

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

        /// <returns>A <typeparamref name="T"/> representation of the binary data.</returns>
        internal static unsafe T ToData<T>(byte* ptr)
            where T : unmanaged
        {
            return *(T*)ptr;
        }

        /// <inheritdoc cref="ToData{T}(byte*)"/>
        internal static unsafe T ToData<T>(byte b0, byte b1)
            where T : unmanaged
        {
            CheckDataSize<T>(2);

            var bytes = stackalloc byte[2] { b0, b1 };
            return ToData<T>(bytes);
        }

        /// <inheritdoc cref="ToData{T}(byte*)"/>
        internal static unsafe T ToData<T>(byte b0, byte b1, byte b2, byte b3)
            where T : unmanaged
        {
            CheckDataSize<T>(4);

            var bytes = stackalloc byte[4] { b0, b1, b2, b3 };
            return ToData<T>(bytes);
        }

        /// <inheritdoc cref="ToData{T}(byte*)"/>
        internal static unsafe T ToData<T>(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
            where T : unmanaged
        {
            CheckDataSize<T>(8);

            var bytes = stackalloc byte[8] { b0, b1, b2, b3, b4, b5, b6, b7 };
            return ToData<T>(bytes);
        }
    }
}
