using System.Collections.Generic;

namespace JPAssets.Binary.Tests
{
    /// <summary>
    /// This class contains predefined binary/value pairs for each primitive data type
    /// for use in unit tests.
    /// Note: Byte arrays declared within this class are defined in big-endian order
    /// (most significant byte first) for readability.
    /// </summary>
    internal static class BinaryTestData
    {
        internal static IEnumerable<object[]> GetBooleanPairs()
        {
            return new object[][]
            {
                new object[2] { (byte)   0, false },
                new object[2] { (byte)0x01, true },
                new object[2] { (byte)0x7F, true },
                new object[2] { (byte)0x80, true },
                new object[2] { (byte)0xFF, true }
            };
        }

        internal static IEnumerable<object[]> GetSBytePairs()
        {
            return new object[][]
            {
                new object[2]{ (byte)   0, (sbyte)0 },
                new object[2]{ (byte)0x01, (sbyte)1 },
                new object[2]{ (byte)0x7F, (sbyte)127 },
                new object[2]{ (byte)0x80, (sbyte)-128 },
                new object[2]{ (byte)0xFF, (sbyte)-1 }
            };
        }

        internal static IEnumerable<object[]> GetCharPairs()
        {
            return new object[][]
            {
                new object[2] { new byte[2] {    0,    0 }, (char)0 },
                new object[2] { new byte[2] {    0, 0x01 }, (char)1 },
                new object[2] { new byte[2] {    0, 0x80 }, (char)128 },
                new object[2] { new byte[2] {    0, 0xFF }, (char)255 },
                new object[2] { new byte[2] { 0x01,    0 }, (char)256 },
                new object[2] { new byte[2] { 0x04,    0 }, (char)1024 },
                new object[2] { new byte[2] { 0xFF, 0xFF }, (char)65535 }
            };
        }

        internal static IEnumerable<object[]> GetInt16Pairs()
        {
            return new object[][]
            {
                new object[2] { new byte[2] {    0,    0 }, (short)0 },
                new object[2] { new byte[2] {    0, 0x01 }, (short)+1 },
                new object[2] { new byte[2] {    0, 0x80 }, (short)+128 },
                new object[2] { new byte[2] {    0, 0xFF }, (short)+255 },
                new object[2] { new byte[2] { 0x01,    0 }, (short)+256 },
                new object[2] { new byte[2] { 0x04,    0 }, (short)+1024 },
                new object[2] { new byte[2] { 0x7F, 0xFF }, (short)+32767 },
                new object[2] { new byte[2] { 0x80,    0 }, (short)-32768 },
                new object[2] { new byte[2] { 0xFF, 0xFF }, (short)-1 }
            };
        }

        internal static IEnumerable<object[]> GetUInt16Pairs()
        {
            return new object[][]
            {
                new object[2] { new byte[2] {    0,    0 }, (ushort)0 },
                new object[2] { new byte[2] {    0, 0x01 }, (ushort)1 },
                new object[2] { new byte[2] {    0, 0x80 }, (ushort)128 },
                new object[2] { new byte[2] {    0, 0xFF }, (ushort)255 },
                new object[2] { new byte[2] { 0x01,    0 }, (ushort)256 },
                new object[2] { new byte[2] { 0x04,    0 }, (ushort)1024 },
                new object[2] { new byte[2] { 0xFF, 0xFF }, (ushort)65535 }
            };
        }

        internal static IEnumerable<object[]> GetInt32Pairs()
        {
            return new object[][]
            {
                new object[2] { new byte[4] {    0,    0,    0,    0 }, (int)0 },
                new object[2] { new byte[4] {    0,    0,    0, 0x01 }, (int)+1 },
                new object[2] { new byte[4] {    0,    0,    0, 0x80 }, (int)+128 },
                new object[2] { new byte[4] {    0,    0,    0, 0xFF }, (int)+255 },
                new object[2] { new byte[4] {    0,    0, 0x01,    0 }, (int)+256 },
                new object[2] { new byte[4] {    0,    0, 0x04,    0 }, (int)+1024 },
                new object[2] { new byte[4] {    0,    0, 0x7F, 0xFF }, (int)+32767 },
                new object[2] { new byte[4] {    0, 0x80,    0,    0 }, (int)+8388608 },
                new object[2] { new byte[4] { 0x7F, 0xFF, 0xFF, 0xFF }, (int)+2147483647 },
                new object[2] { new byte[4] { 0x80,    0,    0,    0 }, (int)-2147483648 },
                new object[2] { new byte[4] { 0xFF, 0xFF, 0x80,    0 }, (int)-32768 },
                new object[2] { new byte[4] { 0xFF, 0xFF, 0xFF, 0xFF }, (int)-1 }
            };
        }

        internal static IEnumerable<object[]> GetUInt32Pairs()
        {
            return new object[][]
            {
                new object[2] { new byte[4] {    0,    0,    0,    0 }, (uint)0 },
                new object[2] { new byte[4] {    0,    0,    0, 0x01 }, (uint)1 },
                new object[2] { new byte[4] {    0,    0,    0, 0x80 }, (uint)128 },
                new object[2] { new byte[4] {    0,    0,    0, 0xFF }, (uint)255 },
                new object[2] { new byte[4] {    0,    0, 0x01,    0 }, (uint)256 },
                new object[2] { new byte[4] {    0,    0, 0x04,    0 }, (uint)1024 },
                new object[2] { new byte[4] {    0,    0, 0x7F, 0xFF }, (uint)32767 },
                new object[2] { new byte[4] {    0, 0x80,    0,    0 }, (uint)8388608 },
                new object[2] { new byte[4] { 0x7F, 0xFF, 0xFF, 0xFF }, (uint)2147483647 },
                new object[2] { new byte[4] { 0x80,    0,    0,    0 }, (uint)2147483648 },
                new object[2] { new byte[4] { 0xFF, 0xFF, 0xFF, 0xFF }, (uint)4294967295 }
            };
        }

        internal static IEnumerable<object[]> GetInt64Pairs()
        {
            return new object[][]
            {
                new object[2] { new byte[8] {    0,    0,    0,    0,    0,    0,    0,    0 }, (long)0 },
                new object[2] { new byte[8] {    0,    0,    0,    0,    0,    0,    0, 0x01 }, (long)+1 },
                new object[2] { new byte[8] {    0,    0,    0,    0,    0,    0,    0, 0x80 }, (long)+128 },
                new object[2] { new byte[8] {    0,    0,    0,    0,    0,    0,    0, 0xFF }, (long)+255 },
                new object[2] { new byte[8] {    0,    0,    0,    0,    0,    0, 0x01,    0 }, (long)+256 },
                new object[2] { new byte[8] {    0,    0,    0,    0,    0,    0, 0x04,    0 }, (long)+1024 },
                new object[2] { new byte[8] {    0,    0,    0,    0,    0,    0, 0x7F, 0xFF }, (long)+32767 },
                new object[2] { new byte[8] {    0,    0,    0,    0,    0, 0x80,    0,    0 }, (long)+8388608 },
                new object[2] { new byte[8] {    0,    0,    0,    0, 0x7F, 0xFF, 0xFF, 0xFF }, (long)+2147483647 },
                new object[2] { new byte[8] { 0x7F, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, (long)+9223372036854775807 },
                new object[2] { new byte[8] { 0x80,    0,    0,    0,    0,    0,    0,    0 }, (long)-9223372036854775808 },
                new object[2] { new byte[8] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,    0 }, (long)-256 },
                new object[2] { new byte[8] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, (long)-1 }
            };
        }

        internal static IEnumerable<object[]> GetUInt64Pairs()
        {
            return new object[][]
            {
                new object[2] { new byte[8] {    0,    0,    0,    0,    0,    0,    0,    0 }, (ulong)0 },
                new object[2] { new byte[8] {    0,    0,    0,    0,    0,    0,    0, 0x01 }, (ulong)1 },
                new object[2] { new byte[8] {    0,    0,    0,    0,    0,    0,    0, 0x80 }, (ulong)128 },
                new object[2] { new byte[8] {    0,    0,    0,    0,    0,    0,    0, 0xFF }, (ulong)255 },
                new object[2] { new byte[8] {    0,    0,    0,    0,    0,    0, 0x01,    0 }, (ulong)256 },
                new object[2] { new byte[8] {    0,    0,    0,    0,    0,    0, 0x04,    0 }, (ulong)1024 },
                new object[2] { new byte[8] {    0,    0,    0,    0,    0,    0, 0x7F, 0xFF }, (ulong)32767 },
                new object[2] { new byte[8] {    0,    0,    0,    0,    0, 0x80,    0,    0 }, (ulong)8388608 },
                new object[2] { new byte[8] {    0,    0,    0,    0, 0x7F, 0xFF, 0xFF, 0xFF }, (ulong)2147483647 },
                new object[2] { new byte[8] {    0,    0,    0,    0, 0x80,    0,    0,    0 }, (ulong)2147483648 },
                new object[2] { new byte[8] {    0,    0,    0,    0, 0xFF, 0xFF, 0xFF, 0xFF }, (ulong)4294967295 },
                new object[2] { new byte[8] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,    0 }, (ulong)18446744073709551360 },
                new object[2] { new byte[8] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, (ulong)18446744073709551615 }
            };
        }

        internal static IEnumerable<object[]> GetSinglePairs()
        {
            return new object[][]
            {
                new object[2] { new byte[4] {    0,    0,    0,    0 }, (float)0f },
                new object[2] { new byte[4] { 0x3D, 0xCC, 0xCC, 0xCD }, (float)+0.1f },
                new object[2] { new byte[4] { 0xBD, 0xCC, 0xCC, 0xCD }, (float)-0.1f },
                new object[2] { new byte[4] { 0x3C, 0x23, 0xD7, 0x0A }, (float)+0.01f },
                new object[2] { new byte[4] { 0xBC, 0x23, 0xD7, 0x0A }, (float)-0.01f },
                new object[2] { new byte[4] { 0x38, 0xD1, 0xB7, 0x17 }, (float)+0.0001f },
                new object[2] { new byte[4] { 0xB8, 0xD1, 0xB7, 0x17 }, (float)-0.0001f },
                new object[2] { new byte[4] { 0x3F, 0x80,    0,    0 }, (float)+1f },
                new object[2] { new byte[4] { 0xBF, 0x80,    0,    0 }, (float)-1f },
                new object[2] { new byte[4] { 0x42, 0xC8,    0,    0 }, (float)+100f },
                new object[2] { new byte[4] { 0xC2, 0xC8,    0,    0 }, (float)-100f },
                new object[2] { new byte[4] { 0x3F, 0x9E, 0x06, 0x10 }, (float)+1.23456f },
                new object[2] { new byte[4] { 0xBF, 0x9E, 0x06, 0x10 }, (float)-1.23456f },
                new object[2] { new byte[4] { 0x7F, 0x7F, 0xFF, 0xFF }, (float)float.MaxValue },
                new object[2] { new byte[4] { 0xFF, 0x7F, 0xFF, 0xFF }, (float)float.MinValue }
            };
        }

        internal static IEnumerable<object[]> GetDoublePairs()
        {
            return new object[][]
            {
                new object[2] { new byte[8] {    0,    0,    0,    0,    0,    0,    0,    0 }, (double)0d },
                new object[2] { new byte[8] { 0x3F, 0xB9, 0x99, 0x99, 0x99, 0x99, 0x99, 0x9A }, (double)+0.1d },
                new object[2] { new byte[8] { 0xBF, 0xB9, 0x99, 0x99, 0x99, 0x99, 0x99, 0x9A }, (double)-0.1d },
                new object[2] { new byte[8] { 0x3F, 0x84, 0x7A, 0xE1, 0x47, 0xAE, 0x14, 0x7B }, (double)+0.01d },
                new object[2] { new byte[8] { 0xBF, 0x84, 0x7A, 0xE1, 0x47, 0xAE, 0x14, 0x7B }, (double)-0.01d },
                new object[2] { new byte[8] { 0x3F, 0x1A, 0x36, 0xE2, 0xEB, 0x1C, 0x43, 0x2D }, (double)+0.0001d },
                new object[2] { new byte[8] { 0xBF, 0x1A, 0x36, 0xE2, 0xEB, 0x1C, 0x43, 0x2D }, (double)-0.0001d },
                new object[2] { new byte[8] { 0x3F, 0xF0,    0,    0,    0,    0,    0,    0 }, (double)+1d },
                new object[2] { new byte[8] { 0xBF, 0xF0,    0,    0,    0,    0,    0,    0 }, (double)-1d },
                new object[2] { new byte[8] { 0x40, 0x59,    0,    0,    0,    0,    0,    0 }, (double)+100d },
                new object[2] { new byte[8] { 0xC0, 0x59,    0,    0,    0,    0,    0,    0 }, (double)-100d },
                new object[2] { new byte[8] { 0x3F, 0xF3, 0xC0, 0xC1, 0xFC, 0x8F, 0x32, 0x38 }, (double)+1.23456d },
                new object[2] { new byte[8] { 0xBF, 0xF3, 0xC0, 0xC1, 0xFC, 0x8F, 0x32, 0x38 }, (double)-1.23456d },
                new object[2] { new byte[8] { 0x7F, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, (double)double.MaxValue },
                new object[2] { new byte[8] { 0xFF, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, (double)double.MinValue }
            };
        }
    }
}
