using System;
using System.Runtime.CompilerServices;

namespace JPAssets.Binary
{
    // TODO: Documentation

    /// <summary>
    /// The <see cref="BinaryConverter"/> class contains methods for converting most base data types
    /// to their byte values, as well as convert arbitrary bytes into base data types.
    /// </summary>
    public static class BinaryConverter
    {
        public static void GetBytes(bool value, out byte b0)
        {
            b0 = value ? (byte)1 : (byte)0;
        }

        public static void GetBytes(sbyte value, out byte b0)
        {
            b0 = (byte)value;
        }

        public static void GetBytes(char value, out byte b0, out byte b1)
        {
            BinaryUtility.ExtractBytes<char>(value, out b0, out b1);
        }

        public static void GetBytes(short value, out byte b0, out byte b1)
        {
            BinaryUtility.ExtractBytes<short>(value, out b0, out b1);
        }

        public static void GetBytes(ushort value, out byte b0, out byte b1)
        {
            BinaryUtility.ExtractBytes<ushort>(value, out b0, out b1);
        }

        public static void GetBytes(int value, out byte b0, out byte b1, out byte b2, out byte b3)
        {
            BinaryUtility.ExtractBytes<int>(value, out b0, out b1, out b2, out b3);
        }

        public static void GetBytes(uint value, out byte b0, out byte b1, out byte b2, out byte b3)
        {
            BinaryUtility.ExtractBytes<uint>(value, out b0, out b1, out b2, out b3);
        }

        public static void GetBytes(long value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7)
        {
            BinaryUtility.ExtractBytes<long>(value, out b0, out b1, out b2, out b3, out b4, out b5, out b6, out b7);
        }

        public static void GetBytes(ulong value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7)
        {
            BinaryUtility.ExtractBytes<ulong>(value, out b0, out b1, out b2, out b3, out b4, out b5, out b6, out b7);
        }

        public static void GetBytes(float value, out byte b0, out byte b1, out byte b2, out byte b3)
        {
            BinaryUtility.ExtractBytes<float>(value, out b0, out b1, out b2, out b3);
        }

        public static void GetBytes(double value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7)
        {
            BinaryUtility.ExtractBytes<double>(value, out b0, out b1, out b2, out b3, out b4, out b5, out b6, out b7);
        }

        public static bool ToBoolean(byte b)
        {
            return b != 0;
        }

        public static sbyte ToSByte(byte b)
        {
            return (sbyte)b;
        }

        public static char ToChar(byte b0, byte b1)
        {
            return BinaryUtility.ToData<char>(b0, b1);
        }

        public static short ToInt16(byte b0, byte b1)
        {
            return BinaryUtility.ToData<short>(b0, b1);
        }

        public static ushort ToUInt16(byte b0, byte b1)
        {
            return BinaryUtility.ToData<ushort>(b0, b1);
        }

        public static int ToInt32(byte b0, byte b1, byte b2, byte b3)
        {
            return BinaryUtility.ToData<int>(b0, b1, b2, b3);
        }

        public static uint ToUInt32(byte b0, byte b1, byte b2, byte b3)
        {
            return BinaryUtility.ToData<uint>(b0, b1, b2, b3);
        }

        public static long ToInt64(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            return BinaryUtility.ToData<long>(b0, b1, b2, b3, b4, b5, b6, b7);
        }

        public static ulong ToUInt64(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            return BinaryUtility.ToData<ulong>(b0, b1, b2, b3, b4, b5, b6, b7);
        }

        public static unsafe float ToSingle(byte b0, byte b1, byte b2, byte b3)
        {
            return BinaryUtility.ToData<float>(b0, b1, b2, b3);
        }

        public static unsafe double ToDouble(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            return BinaryUtility.ToData<double>(b0, b1, b2, b3, b4, b5, b6, b7);
        }
    }
}
