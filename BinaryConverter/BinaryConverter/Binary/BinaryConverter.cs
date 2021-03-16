using System;
using System.Runtime.CompilerServices;

namespace JPAssets.Binary
{
    /// <summary>
    /// The <see cref="BinaryConverter"/> class contains methods for converting most base data types
    /// to their byte values, as well as convert arbitrary bytes into base data types.
    /// </summary>
    public static class BinaryConverter
    {
        /// <summary>
        /// Converts the given <see cref="bool"/> value to a <see cref="byte"/>.
        /// </summary>
        public static void GetBytes(bool value, out byte b0)
        {
            b0 = value ? (byte)1 : (byte)0;
        }

        /// <summary>
        /// Converts the given <see cref="sbyte"/> value to a <see cref="byte"/> with an equal binary value.
        /// </summary>
        public static void GetBytes(sbyte value, out byte b0)
        {
            b0 = (byte)value;
        }

        /// <inheritdoc cref="BinaryConversionUtility.ExtractBytes{T}(T, out byte, out byte)"/>
        public static void GetBytes(char value, out byte b0, out byte b1)
        {
            BinaryConversionUtility.ExtractBytes<char>(value, out b0, out b1);
        }

        /// <inheritdoc cref="BinaryConversionUtility.ExtractBytes{T}(T, out byte, out byte)"/>
        public static void GetBytes(short value, out byte b0, out byte b1)
        {
            BinaryConversionUtility.ExtractBytes<short>(value, out b0, out b1);
        }

        /// <inheritdoc cref="BinaryConversionUtility.ExtractBytes{T}(T, out byte, out byte)"/>
        public static void GetBytes(ushort value, out byte b0, out byte b1)
        {
            BinaryConversionUtility.ExtractBytes<ushort>(value, out b0, out b1);
        }

        /// <inheritdoc cref="BinaryConversionUtility.ExtractBytes{T}(T, out byte, out byte, out byte, out byte)"/>
        public static void GetBytes(int value, out byte b0, out byte b1, out byte b2, out byte b3)
        {
            BinaryConversionUtility.ExtractBytes<int>(value, out b0, out b1, out b2, out b3);
        }

        /// <inheritdoc cref="BinaryConversionUtility.ExtractBytes{T}(T, out byte, out byte, out byte, out byte)"/>
        public static void GetBytes(uint value, out byte b0, out byte b1, out byte b2, out byte b3)
        {
            BinaryConversionUtility.ExtractBytes<uint>(value, out b0, out b1, out b2, out b3);
        }

        /// <inheritdoc cref="BinaryConversionUtility.ExtractBytes{T}(T, out byte, out byte, out byte, out byte, out byte, out byte, out byte, out byte)"/>
        public static void GetBytes(long value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7)
        {
            BinaryConversionUtility.ExtractBytes<long>(value, out b0, out b1, out b2, out b3, out b4, out b5, out b6, out b7);
        }

        /// <inheritdoc cref="BinaryConversionUtility.ExtractBytes{T}(T, out byte, out byte, out byte, out byte, out byte, out byte, out byte, out byte)"/>
        public static void GetBytes(ulong value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7)
        {
            BinaryConversionUtility.ExtractBytes<ulong>(value, out b0, out b1, out b2, out b3, out b4, out b5, out b6, out b7);
        }

        /// <inheritdoc cref="BinaryConversionUtility.ExtractBytes{T}(T, out byte, out byte, out byte, out byte)"/>
        public static void GetBytes(float value, out byte b0, out byte b1, out byte b2, out byte b3)
        {
            BinaryConversionUtility.ExtractBytes<float>(value, out b0, out b1, out b2, out b3);
        }

        /// <inheritdoc cref="BinaryConversionUtility.ExtractBytes{T}(T, out byte, out byte, out byte, out byte, out byte, out byte, out byte, out byte)"/>
        public static void GetBytes(double value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7)
        {
            BinaryConversionUtility.ExtractBytes<double>(value, out b0, out b1, out b2, out b3, out b4, out b5, out b6, out b7);
        }

        /// /// <returns><see langword="false"/> if the given byte is zero; Otherwise <see langword="true"/>.</returns>
        public static bool ToBoolean(byte b)
        {
            return b != 0;
        }

        /// <returns>An <see cref="sbyte"/> value with an equal binary value to the given <see cref="byte"/>.</returns>
        public static sbyte ToSByte(byte b)
        {
            return (sbyte)b;
        }

        /// <returns>A <see cref="char"/> representation of the binary data.</returns>
        public static char ToChar(byte b0, byte b1)
        {
            return BinaryConversionUtility.ToData<char>(b0, b1);
        }

        /// <returns>A <see cref="short"/> representation of the binary data.</returns>
        public static short ToInt16(byte b0, byte b1)
        {
            return BinaryConversionUtility.ToData<short>(b0, b1);
        }

        /// <returns>A <see cref="ushort"/> representation of the binary data.</returns>
        public static ushort ToUInt16(byte b0, byte b1)
        {
            return BinaryConversionUtility.ToData<ushort>(b0, b1);
        }

        /// <returns>A <see cref="int"/> representation of the binary data.</returns>
        public static int ToInt32(byte b0, byte b1, byte b2, byte b3)
        {
            return BinaryConversionUtility.ToData<int>(b0, b1, b2, b3);
        }

        /// <returns>A <see cref="uint"/> representation of the binary data.</returns>
        public static uint ToUInt32(byte b0, byte b1, byte b2, byte b3)
        {
            return BinaryConversionUtility.ToData<uint>(b0, b1, b2, b3);
        }

        /// <returns>A <see cref="long"/> representation of the binary data.</returns>
        public static long ToInt64(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            return BinaryConversionUtility.ToData<long>(b0, b1, b2, b3, b4, b5, b6, b7);
        }

        /// <returns>A <see cref="ulong"/> representation of the binary data.</returns>
        public static ulong ToUInt64(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            return BinaryConversionUtility.ToData<ulong>(b0, b1, b2, b3, b4, b5, b6, b7);
        }

        /// <returns>A <see cref="float"/> representation of the binary data.</returns>
        public static unsafe float ToSingle(byte b0, byte b1, byte b2, byte b3)
        {
            return BinaryConversionUtility.ToData<float>(b0, b1, b2, b3);
        }

        /// <returns>A <see cref="double"/> representation of the binary data.</returns>
        public static unsafe double ToDouble(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            return BinaryConversionUtility.ToData<double>(b0, b1, b2, b3, b4, b5, b6, b7);
        }
    }
}
