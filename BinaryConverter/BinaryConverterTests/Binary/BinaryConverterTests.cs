using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JPAssets.Binary.Tests
{
    // TODO: It may be more useful to test the methods in the underlying BinaryUtility class,
    // given the BinaryConverter class is now basically just a wrapper for it.
    // If so, maybe still worth having basic tests just to make sure the out byte params
    // are being assigned in the expected order, etc.

    /// <summary>
    /// A collection of tests for the <see cref="BinaryConverter"/> class.
    /// </summary>
    [TestClass()]
    public class BinaryConverterTests
    {
        private static string ByteToString(byte b) => b.ToString();

        private static string GenerateTestDataLogMessage<T>(byte[] bytes, T expected)
        {
            var byteStrings = bytes.Select(new Func<byte, string>(ByteToString));
            return $"Testing byte values ({string.Join(", ", byteStrings)}) against expected {typeof(T).FullName} value {expected.ToString()}.";
        }

        private static void PreprocessTestData<T>(byte[] bytes, T expected)
        {
            // Convert from big-endian to little-endian bytes.
            if (System.BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            Console.WriteLine(GenerateTestDataLogMessage(bytes, expected));
        }

        private static void PreprocessTestData<T>(byte b, T expected)
        {
            Console.WriteLine(GenerateTestDataLogMessage(new byte[] { b }, expected));
        }

        [TestMethod("GetBytes(bool, out ...)")]
        public void GetBytesFromBoolean()
        {
            BinaryConverter.GetBytes(false, out byte falseByte);
            Assert.AreEqual<byte>(0, falseByte);

            BinaryConverter.GetBytes(true, out byte trueByte);
            Assert.AreNotEqual<byte>(0, trueByte);
        }

        [DynamicData(nameof(BinaryTestData.GetBooleanPairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("ToBoolean")]
        public void BytesToBoolean(byte b, bool value)
        {
            PreprocessTestData(b, value);

            Assert.AreEqual<bool>(value, BinaryConverter.ToBoolean(b));
        }

        [DynamicData(nameof(BinaryTestData.GetSBytePairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("GetBytes(sbyte, out ...)")]
        public void GetBytesFromSByte(byte b, sbyte value)
        {
            PreprocessTestData(b, value);

            BinaryConverter.GetBytes(value, out byte b0);
            Assert.AreEqual<byte>(b, b0);
        }

        [DynamicData(nameof(BinaryTestData.GetSBytePairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("ToSByte")]
        public void BytesToSByte(byte b, sbyte value)
        {
            PreprocessTestData(b, value);

            Assert.AreEqual<sbyte>(value, BinaryConverter.ToSByte(b));
        }

        [DynamicData(nameof(BinaryTestData.GetCharPairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("GetBytes(char, out ...)")]
        public void GetBytesFromChar(byte[] bytes, char value)
        {
            PreprocessTestData(bytes, value);

            BinaryConverter.GetBytes(value, out byte b0, out byte b1);
            var resultingBytes = new[] { b0, b1 };

            CollectionAssert.AreEqual(bytes, resultingBytes);
        }

        [DynamicData(nameof(BinaryTestData.GetCharPairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("ToChar")]
        public void BytesToChar(byte[] bytes, char value)
        {
            PreprocessTestData(bytes, value);

            Assert.AreEqual<char>(value, BinaryConverter.ToChar(bytes[0], bytes[1]));
        }

        [DynamicData(nameof(BinaryTestData.GetInt16Pairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("GetBytes(short, out ...)")]
        public void GetBytesFromInt16(byte[] bytes, short value)
        {
            PreprocessTestData(bytes, value);

            BinaryConverter.GetBytes(value, out byte b0, out byte b1);
            var resultingBytes = new[] { b0, b1 };

            CollectionAssert.AreEqual(bytes, resultingBytes);
        }

        [DynamicData(nameof(BinaryTestData.GetInt16Pairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("ToInt16")]
        public void BytesToInt16(byte[] bytes, short value)
        {
            PreprocessTestData(bytes, value);

            Assert.AreEqual<short>(value, BinaryConverter.ToInt16(bytes[0], bytes[1]));
        }

        [DynamicData(nameof(BinaryTestData.GetUInt16Pairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("GetBytes(ushort, out ...)")]
        public void GetBytesFromUInt16(byte[] bytes, ushort value)
        {
            PreprocessTestData(bytes, value);

            BinaryConverter.GetBytes(value, out byte b0, out byte b1);
            var resultingBytes = new[] { b0, b1 };

            CollectionAssert.AreEqual(bytes, resultingBytes);
        }

        [DynamicData(nameof(BinaryTestData.GetUInt16Pairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("ToUInt16")]
        public void BytesToUInt16(byte[] bytes, ushort value)
        {
            PreprocessTestData(bytes, value);

            Assert.AreEqual<ushort>(value, BinaryConverter.ToUInt16(bytes[0], bytes[1]));
        }

        [DynamicData(nameof(BinaryTestData.GetInt32Pairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("GetBytes(int, out ...)")]
        public void GetBytesFromInt32(byte[] bytes, int value)
        {
            PreprocessTestData(bytes, value);

            BinaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3);
            var resultingBytes = new[] { b0, b1, b2, b3 };

            CollectionAssert.AreEqual(bytes, resultingBytes);
        }

        [DynamicData(nameof(BinaryTestData.GetInt32Pairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("ToInt32")]
        public void BytesToInt32(byte[] bytes, int value)
        {
            PreprocessTestData(bytes, value);

            Assert.AreEqual<int>(value, BinaryConverter.ToInt32(bytes[0], bytes[1], bytes[2], bytes[3]));
        }

        [DynamicData(nameof(BinaryTestData.GetUInt32Pairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("GetBytes(uint, out ...)")]
        public void GetBytesFromUInt32(byte[] bytes, uint value)
        {
            PreprocessTestData(bytes, value);

            BinaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3);
            var resultingBytes = new[] { b0, b1, b2, b3 };

            CollectionAssert.AreEqual(bytes, resultingBytes);
        }

        [DynamicData(nameof(BinaryTestData.GetUInt32Pairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("ToUInt32")]
        public void BytesToUInt32(byte[] bytes, uint value)
        {
            PreprocessTestData(bytes, value);

            Assert.AreEqual<uint>(value, BinaryConverter.ToUInt32(bytes[0], bytes[1], bytes[2], bytes[3]));
        }

        [DynamicData(nameof(BinaryTestData.GetInt64Pairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("GetBytes(long, out ...)")]
        public void GetBytesFromInt64(byte[] bytes, long value)
        {
            PreprocessTestData(bytes, value);

            BinaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7);
            var resultingBytes = new[] { b0, b1, b2, b3, b4, b5, b6, b7 };

            CollectionAssert.AreEqual(bytes, resultingBytes);
        }

        [DynamicData(nameof(BinaryTestData.GetInt64Pairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("ToInt64")]
        public void BytesToInt64(byte[] bytes, long value)
        {
            PreprocessTestData(bytes, value);

            Assert.AreEqual<long>(value, BinaryConverter.ToInt64(bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6], bytes[7]));
        }

        [DynamicData(nameof(BinaryTestData.GetUInt64Pairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("GetBytes(ulong, out ...)")]
        public void GetBytesFromUInt64(byte[] bytes, ulong value)
        {
            PreprocessTestData(bytes, value);

            BinaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7);
            var resultingBytes = new[] { b0, b1, b2, b3, b4, b5, b6, b7 };

            CollectionAssert.AreEqual(bytes, resultingBytes);
        }

        [DynamicData(nameof(BinaryTestData.GetUInt64Pairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("ToUInt64")]
        public void BytesToUInt64(byte[] bytes, ulong value)
        {
            PreprocessTestData(bytes, value);

            Assert.AreEqual<ulong>(value, BinaryConverter.ToUInt64(bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6], bytes[7]));
        }

        [DynamicData(nameof(BinaryTestData.GetSinglePairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("GetBytes(float, out ...)")]
        public void GetBytesFromSingle(byte[] bytes, float value)
        {
            PreprocessTestData(bytes, value);

            BinaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3);
            var resultingBytes = new[] { b0, b1, b2, b3 };

            CollectionAssert.AreEqual(bytes, resultingBytes);
        }

        [DynamicData(nameof(BinaryTestData.GetSinglePairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("ToSingle")]
        public void BytesToSingle(byte[] bytes, float value)
        {
            PreprocessTestData(bytes, value);

            Assert.AreEqual<float>(value, BinaryConverter.ToSingle(bytes[0], bytes[1], bytes[2], bytes[3]));
        }

        [DynamicData(nameof(BinaryTestData.GetDoublePairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("GetBytes(double, out ...)")]
        public void GetBytesFromDouble(byte[] bytes, double value)
        {
            PreprocessTestData(bytes, value);

            BinaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7);
            var resultingBytes = new[] { b0, b1, b2, b3, b4, b5, b6, b7 };

            CollectionAssert.AreEqual(bytes, resultingBytes);
        }

        [DynamicData(nameof(BinaryTestData.GetDoublePairs), typeof(BinaryTestData), DynamicDataSourceType.Method)]
        [TestMethod("ToDouble")]
        public void BytesToDouble(byte[] bytes, double value)
        {
            PreprocessTestData(bytes, value);

            Assert.AreEqual<double>(value, BinaryConverter.ToDouble(bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6], bytes[7]));
        }
    }
}
