using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace JPAssets.Binary.Tests
{
    [TestClass()]
    public class BinaryConverterTests
    {
        private static string GenerateGetBytesLogMessage<T>(T value, byte expected)
        {
            return $"Asserting {typeof(T).FullName} value {value.ToString()} is equal to expected byte value {expected.ToString()}.";
        }

        private static string GenerateGetBytesLogMessage<T>(T value, byte[] expected)
        {
            return $"Asserting {typeof(T).FullName} value {value.ToString()} is equal to expected byte value ({string.Join(", ", expected.Select(b => b.ToString()))}).";
        }

        private static (byte b, bool value)[] GetBooleanPairs()
        {
            return new (byte b, bool value)[]
            {
                (b:    0, value: false),
                (b: 0x01, value: true),
                (b: 0x7F, value: true),
                (b: 0x80, value: true),
                (b: 0xFF, value: true)
            };
        }

        private static (byte b, sbyte value)[] GetSBytePairs()
        {
            return new (byte b, sbyte value)[]
            {
                (b:    0, value: 0),
                (b: 0x01, value: 1),
                (b: 0x7F, value: 127),
                (b: 0x80, value: -128),
                (b: 0xFF, value: -1)
            };
        }

        private static (byte[] bytes, char value)[] GetCharPairs()
        {
            return new (byte[] bytes, char value)[]
            {
                (bytes: new byte[2] {    0,    0 }, value: (char)0),
                (bytes: new byte[2] {    0, 0x01 }, value: (char)1),
                (bytes: new byte[2] {    0, 0x80 }, value: (char)128),
                (bytes: new byte[2] {    0, 0xFF }, value: (char)255),
                (bytes: new byte[2] { 0x01,    0 }, value: (char)256),
                (bytes: new byte[2] { 0x04,    0 }, value: (char)1024),
                (bytes: new byte[2] { 0xFF, 0xFF }, value: (char)65535)
            };
        }

        private static (byte[] bytes, short value)[] GetInt16Pairs()
        {
            return new (byte[] bytes, short value)[]
            {
                (bytes: new byte[2] {    0,    0 }, value: 0),
                (bytes: new byte[2] {    0, 0x01 }, value: +1),
                (bytes: new byte[2] {    0, 0x80 }, value: +128),
                (bytes: new byte[2] {    0, 0xFF }, value: +255),
                (bytes: new byte[2] { 0x01,    0 }, value: +256),
                (bytes: new byte[2] { 0x04,    0 }, value: +1024),
                (bytes: new byte[2] { 0x7F, 0xFF }, value: +32767),
                (bytes: new byte[2] { 0x80,    0 }, value: -32768),
                (bytes: new byte[2] { 0xFF, 0xFF }, value: -1)
            };
        }

        private static (byte[] bytes, ushort value)[] GetUInt16Pairs()
        {
            return new (byte[] bytes, ushort value)[]
            {
                (bytes: new byte[2] {    0,    0 }, value: 0),
                (bytes: new byte[2] {    0, 0x01 }, value: 1),
                (bytes: new byte[2] {    0, 0x80 }, value: 128),
                (bytes: new byte[2] {    0, 0xFF }, value: 255),
                (bytes: new byte[2] { 0x01,    0 }, value: 256),
                (bytes: new byte[2] { 0x04,    0 }, value: 1024),
                (bytes: new byte[2] { 0xFF, 0xFF }, value: 65535)
            };
        }

        private static (byte[] bytes, int value)[] GetInt32Pairs()
        {
            return new (byte[] bytes, int value)[]
            {
                (bytes: new byte[4] {    0,    0,    0,    0 }, value: 0),
                (bytes: new byte[4] {    0,    0,    0, 0x01 }, value: +1),
                (bytes: new byte[4] {    0,    0,    0, 0x80 }, value: +128),
                (bytes: new byte[4] {    0,    0,    0, 0xFF }, value: +255),
                (bytes: new byte[4] {    0,    0, 0x01,    0 }, value: +256),
                (bytes: new byte[4] {    0,    0, 0x04,    0 }, value: +1024),
                (bytes: new byte[4] {    0,    0, 0x7F, 0xFF }, value: +32767),
                (bytes: new byte[4] {    0, 0x80,    0,    0 }, value: +8388608),
                (bytes: new byte[4] { 0x7F, 0xFF, 0xFF, 0xFF }, value: +2147483647),
                (bytes: new byte[4] { 0x80,    0,    0,    0 }, value: -2147483648),
                (bytes: new byte[4] { 0xFF, 0xFF, 0x80,    0 }, value: -32768),
                (bytes: new byte[4] { 0xFF, 0xFF, 0xFF, 0xFF }, value: -1)
            };
        }

        private static (byte[] bytes, uint value)[] GetUInt32Pairs()
        {
            return new (byte[] bytes, uint value)[]
            {
                (bytes: new byte[4] {    0,    0,    0,    0 }, value: 0),
                (bytes: new byte[4] {    0,    0,    0, 0x01 }, value: 1),
                (bytes: new byte[4] {    0,    0,    0, 0x80 }, value: 128),
                (bytes: new byte[4] {    0,    0,    0, 0xFF }, value: 255),
                (bytes: new byte[4] {    0,    0, 0x01,    0 }, value: 256),
                (bytes: new byte[4] {    0,    0, 0x04,    0 }, value: 1024),
                (bytes: new byte[4] {    0,    0, 0x7F, 0xFF }, value: 32767),
                (bytes: new byte[4] {    0, 0x80,    0,    0 }, value: 8388608),
                (bytes: new byte[4] { 0x7F, 0xFF, 0xFF, 0xFF }, value: 2147483647),
                (bytes: new byte[4] { 0x80,    0,    0,    0 }, value: 2147483648),
                (bytes: new byte[4] { 0xFF, 0xFF, 0xFF, 0xFF }, value: 4294967295)
            };
        }

        private static (byte[] bytes, long value)[] GetInt64Pairs()
        {
            return new (byte[] bytes, long value)[]
            {
                (bytes: new byte[8] {    0,    0,    0,    0,    0,    0,    0,    0 }, value: 0),
                (bytes: new byte[8] {    0,    0,    0,    0,    0,    0,    0, 0x01 }, value: +1),
                (bytes: new byte[8] {    0,    0,    0,    0,    0,    0,    0, 0x80 }, value: +128),
                (bytes: new byte[8] {    0,    0,    0,    0,    0,    0,    0, 0xFF }, value: +255),
                (bytes: new byte[8] {    0,    0,    0,    0,    0,    0, 0x01,    0 }, value: +256),
                (bytes: new byte[8] {    0,    0,    0,    0,    0,    0, 0x04,    0 }, value: +1024),
                (bytes: new byte[8] {    0,    0,    0,    0,    0,    0, 0x7F, 0xFF }, value: +32767),
                (bytes: new byte[8] {    0,    0,    0,    0,    0, 0x80,    0,    0 }, value: +8388608),
                (bytes: new byte[8] {    0,    0,    0,    0, 0x7F, 0xFF, 0xFF, 0xFF }, value: +2147483647),
                (bytes: new byte[8] { 0x7F, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, value: +9223372036854775807),
                (bytes: new byte[8] { 0x80,    0,    0,    0,    0,    0,    0,    0 }, value: -9223372036854775808),
                (bytes: new byte[8] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,    0 }, value: -256),
                (bytes: new byte[8] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, value: -1)
            };
        }

        private static (byte[] bytes, ulong value)[] GetUInt64Pairs()
        {
            return new (byte[] bytes, ulong value)[]
            {
                (bytes: new byte[8] {    0,    0,    0,    0,    0,    0,    0,    0 }, value: 0),
                (bytes: new byte[8] {    0,    0,    0,    0,    0,    0,    0, 0x01 }, value: 1),
                (bytes: new byte[8] {    0,    0,    0,    0,    0,    0,    0, 0x80 }, value: 128),
                (bytes: new byte[8] {    0,    0,    0,    0,    0,    0,    0, 0xFF }, value: 255),
                (bytes: new byte[8] {    0,    0,    0,    0,    0,    0, 0x01,    0 }, value: 256),
                (bytes: new byte[8] {    0,    0,    0,    0,    0,    0, 0x04,    0 }, value: 1024),
                (bytes: new byte[8] {    0,    0,    0,    0,    0,    0, 0x7F, 0xFF }, value: 32767),
                (bytes: new byte[8] {    0,    0,    0,    0,    0, 0x80,    0,    0 }, value: 8388608),
                (bytes: new byte[8] {    0,    0,    0,    0, 0x7F, 0xFF, 0xFF, 0xFF }, value: 2147483647),
                (bytes: new byte[8] {    0,    0,    0,    0, 0x80,    0,    0,    0 }, value: 2147483648),
                (bytes: new byte[8] {    0,    0,    0,    0, 0xFF, 0xFF, 0xFF, 0xFF }, value: 4294967295),
                (bytes: new byte[8] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,    0 }, value: 18446744073709551360),
                (bytes: new byte[8] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, value: 18446744073709551615)
            };
        }

        private static (byte[] bytes, float value)[] GetSinglePairs()
        {
            return new (byte[] bytes, float value)[]
            {
                (bytes: new byte[4] {    0,    0,    0,    0 }, value: 0f),
                (bytes: new byte[4] { 0x3D, 0xCC, 0xCC, 0xCD }, value: +0.1f),
                (bytes: new byte[4] { 0xBD, 0xCC, 0xCC, 0xCD }, value: -0.1f),
                (bytes: new byte[4] { 0x3C, 0x23, 0xD7, 0x0A }, value: +0.01f),
                (bytes: new byte[4] { 0xBC, 0x23, 0xD7, 0x0A }, value: -0.01f),
                (bytes: new byte[4] { 0x38, 0xD1, 0xB7, 0x17 }, value: +0.0001f),
                (bytes: new byte[4] { 0xB8, 0xD1, 0xB7, 0x17 }, value: -0.0001f),
                (bytes: new byte[4] { 0x3F, 0x80,    0,    0 }, value: +1f),
                (bytes: new byte[4] { 0xBF, 0x80,    0,    0 }, value: -1f),
                (bytes: new byte[4] { 0x42, 0xC8,    0,    0 }, value: +100f),
                (bytes: new byte[4] { 0xC2, 0xC8,    0,    0 }, value: -100f),
                (bytes: new byte[4] { 0x3F, 0x9E, 0x06, 0x10 }, value: +1.23456f),
                (bytes: new byte[4] { 0xBF, 0x9E, 0x06, 0x10 }, value: -1.23456f),
                (bytes: new byte[4] { 0x7F, 0x7F, 0xFF, 0xFF }, value: float.MaxValue),
                (bytes: new byte[4] { 0xFF, 0x7F, 0xFF, 0xFF }, value: float.MinValue)
            };
        }

        private static (byte[] bytes, double value)[] GetDoublePairs()
        {
            return new (byte[] bytes, double value)[]
            {
                (bytes: new byte[8] {    0,    0,    0,    0,    0,    0,    0,    0 }, value: 0d),
                (bytes: new byte[8] { 0x3F, 0xB9, 0x99, 0x99, 0x99, 0x99, 0x99, 0x9A }, value: +0.1d),
                (bytes: new byte[8] { 0xBF, 0xB9, 0x99, 0x99, 0x99, 0x99, 0x99, 0x9A }, value: -0.1d),
                (bytes: new byte[8] { 0x3F, 0x84, 0x7A, 0xE1, 0x47, 0xAE, 0x14, 0x7B }, value: +0.01d),
                (bytes: new byte[8] { 0xBF, 0x84, 0x7A, 0xE1, 0x47, 0xAE, 0x14, 0x7B }, value: -0.01d),
                (bytes: new byte[8] { 0x3F, 0x1A, 0x36, 0xE2, 0xEB, 0x1C, 0x43, 0x2D }, value: +0.0001d),
                (bytes: new byte[8] { 0xBF, 0x1A, 0x36, 0xE2, 0xEB, 0x1C, 0x43, 0x2D }, value: -0.0001d),
                (bytes: new byte[8] { 0x3F, 0xF0,    0,    0,    0,    0,    0,    0 }, value: +1d),
                (bytes: new byte[8] { 0xBF, 0xF0,    0,    0,    0,    0,    0,    0 }, value: -1d),
                (bytes: new byte[8] { 0x40, 0x59,    0,    0,    0,    0,    0,    0 }, value: +100d),
                (bytes: new byte[8] { 0xC0, 0x59,    0,    0,    0,    0,    0,    0 }, value: -100d),
                (bytes: new byte[8] { 0x3F, 0xF3, 0xC0, 0xC1, 0xFC, 0x8F, 0x32, 0x38 }, value: +1.23456d),
                (bytes: new byte[8] { 0xBF, 0xF3, 0xC0, 0xC1, 0xFC, 0x8F, 0x32, 0x38 }, value: -1.23456d),
                (bytes: new byte[8] { 0x7F, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, value: double.MaxValue),
                (bytes: new byte[8] { 0xFF, 0xEF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, value: double.MinValue)
            };
        }

        [TestMethod()]
        public void GetBytesFromBoolean()
        {
            Assert.AreEqual<byte>(0, BinaryConverter.GetBytes(false));

            Assert.AreNotEqual<byte>(0, BinaryConverter.GetBytes(true));
        }

        [TestMethod()]
        public void BytesToBoolean()
        {
            foreach ((byte b, bool value) in GetBooleanPairs())
            {
                Assert.AreEqual<bool>(value, BinaryConverter.ToBoolean(b));
            }
        }

        [TestMethod()]
        public void GetBytesFromSByte()
        {
            foreach ((byte b, sbyte value) in GetSBytePairs())
            {
                Console.WriteLine(GenerateGetBytesLogMessage(value, b));
                Assert.AreEqual<byte>(b, BinaryConverter.GetBytes(value));
            }
        }

        [TestMethod()]
        public void BytesToSByte()
        {
            foreach ((byte b, sbyte value) in GetSBytePairs())
            {
                Assert.AreEqual<sbyte>(value, BinaryConverter.ToSByte(b));
            }
        }

        [TestMethod()]
        public void GetBytesFromChar()
        {
            foreach ((byte[] bytes, char value) in GetCharPairs())
            {
                Console.WriteLine(GenerateGetBytesLogMessage(value, bytes));

                // Test out-byte method
                {
                    BinaryConverter.GetBytes(value, out byte b0, out byte b1);

                    Assert.AreEqual<byte>(bytes[0], b0);
                    Assert.AreEqual<byte>(bytes[1], b1);
                }

                // Test binary-return method
                {
                    var bin16 = BinaryConverter.GetBytes(value);

                    Assert.AreEqual<byte>(bytes[0], bin16.b0);
                    Assert.AreEqual<byte>(bytes[1], bin16.b1);
                }
            }
        }

        [TestMethod()]
        public void BytesToChar()
        {
            foreach ((byte[] bytes, char value) in GetCharPairs())
            {
                // Test byte param method
                Assert.AreEqual<char>(value, BinaryConverter.ToChar(bytes[0], bytes[1]));

                // Test binary param method
                Assert.AreEqual<char>(value, BinaryConverter.ToChar(new Binary16(bytes[0], bytes[1])));
            }
        }

        [TestMethod()]
        public void GetBytesFromInt16()
        {
            foreach ((byte[] bytes, short value) in GetInt16Pairs())
            {
                Console.WriteLine(GenerateGetBytesLogMessage(value, bytes));

                // Test out-byte method
                {
                    BinaryConverter.GetBytes(value, out byte b0, out byte b1);

                    Assert.AreEqual<byte>(bytes[0], b0);
                    Assert.AreEqual<byte>(bytes[1], b1);
                }

                // Test binary-return method
                {
                    var bin16 = BinaryConverter.GetBytes(value);

                    Assert.AreEqual<byte>(bytes[0], bin16.b0);
                    Assert.AreEqual<byte>(bytes[1], bin16.b1);
                }
            }
        }

        [TestMethod()]
        public void BytesToInt16()
        {
            foreach ((byte[] bytes, short value) in GetInt16Pairs())
            {
                // Test byte param method
                Assert.AreEqual<short>(value, BinaryConverter.ToInt16(bytes[0], bytes[1]));

                // Test binary param method
                Assert.AreEqual<short>(value, BinaryConverter.ToInt16(new Binary16(bytes[0], bytes[1])));
            }
        }

        [TestMethod()]
        public void GetBytesFromUInt16()
        {
            foreach ((byte[] bytes, ushort value) in GetUInt16Pairs())
            {
                Console.WriteLine(GenerateGetBytesLogMessage(value, bytes));

                // Test out-byte method
                {
                    BinaryConverter.GetBytes(value, out byte b0, out byte b1);

                    Assert.AreEqual<byte>(bytes[0], b0);
                    Assert.AreEqual<byte>(bytes[1], b1);
                }

                // Test binary-return method
                {
                    var bin16 = BinaryConverter.GetBytes(value);

                    Assert.AreEqual<byte>(bytes[0], bin16.b0);
                    Assert.AreEqual<byte>(bytes[1], bin16.b1);
                }
            }
        }

        [TestMethod()]
        public void BytesToUInt16()
        {
            foreach ((byte[] bytes, ushort value) in GetUInt16Pairs())
            {
                // Test byte param method
                Assert.AreEqual<ushort>(value, BinaryConverter.ToUInt16(bytes[0], bytes[1]));

                // Test binary param method
                Assert.AreEqual<ushort>(value, BinaryConverter.ToUInt16(new Binary16(bytes[0], bytes[1])));
            }
        }

        [TestMethod()]
        public void GetBytesFromInt32()
        {
            foreach ((byte[] bytes, int value) in GetInt32Pairs())
            {
                Console.WriteLine(GenerateGetBytesLogMessage(value, bytes));

                // Test out-byte method
                {
                    BinaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3);

                    Assert.AreEqual<byte>(bytes[0], b0);
                    Assert.AreEqual<byte>(bytes[1], b1);
                    Assert.AreEqual<byte>(bytes[2], b2);
                    Assert.AreEqual<byte>(bytes[3], b3);
                }

                // Test binary-return method
                {
                    var bin32 = BinaryConverter.GetBytes(value);

                    Assert.AreEqual<byte>(bytes[0], bin32.b0);
                    Assert.AreEqual<byte>(bytes[1], bin32.b1);
                    Assert.AreEqual<byte>(bytes[2], bin32.b2);
                    Assert.AreEqual<byte>(bytes[3], bin32.b3);
                }
            }
        }

        [TestMethod()]
        public void BytesToInt32()
        {
            foreach ((byte[] bytes, int value) in GetInt32Pairs())
            {
                // Test byte param method
                Assert.AreEqual<int>(value, BinaryConverter.ToInt32(bytes[0], bytes[1], bytes[2], bytes[3]));

                // Test binary param method
                Assert.AreEqual<int>(value, BinaryConverter.ToInt32(new Binary32(bytes[0], bytes[1], bytes[2], bytes[3])));
            }
        }

        [TestMethod()]
        public void GetBytesFromUInt32()
        {
            foreach ((byte[] bytes, uint value) in GetUInt32Pairs())
            {
                Console.WriteLine(GenerateGetBytesLogMessage(value, bytes));

                // Test out-byte method
                {
                    BinaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3);

                    Assert.AreEqual<byte>(bytes[0], b0);
                    Assert.AreEqual<byte>(bytes[1], b1);
                    Assert.AreEqual<byte>(bytes[2], b2);
                    Assert.AreEqual<byte>(bytes[3], b3);
                }

                // Test binary-return method
                {
                    var bin32 = BinaryConverter.GetBytes(value);

                    Assert.AreEqual<byte>(bytes[0], bin32.b0);
                    Assert.AreEqual<byte>(bytes[1], bin32.b1);
                    Assert.AreEqual<byte>(bytes[2], bin32.b2);
                    Assert.AreEqual<byte>(bytes[3], bin32.b3);
                }
            }
        }

        [TestMethod()]
        public void BytesToUInt32()
        {
            foreach ((byte[] bytes, uint value) in GetUInt32Pairs())
            {
                // Test byte param method
                Assert.AreEqual<uint>(value, BinaryConverter.ToUInt32(bytes[0], bytes[1], bytes[2], bytes[3]));

                // Test binary param method
                Assert.AreEqual<uint>(value, BinaryConverter.ToUInt32(new Binary32(bytes[0], bytes[1], bytes[2], bytes[3])));
            }
        }

        [TestMethod()]
        public void GetBytesFromInt64()
        {
            foreach ((byte[] bytes, long value) in GetInt64Pairs())
            {
                Console.WriteLine(GenerateGetBytesLogMessage(value, bytes));

                // Test out-byte method
                {
                    BinaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7);

                    Assert.AreEqual<byte>(bytes[0], b0);
                    Assert.AreEqual<byte>(bytes[1], b1);
                    Assert.AreEqual<byte>(bytes[2], b2);
                    Assert.AreEqual<byte>(bytes[3], b3);
                    Assert.AreEqual<byte>(bytes[4], b4);
                    Assert.AreEqual<byte>(bytes[5], b5);
                    Assert.AreEqual<byte>(bytes[6], b6);
                    Assert.AreEqual<byte>(bytes[7], b7);
                }

                // Test binary-return method
                {
                    var bin64 = BinaryConverter.GetBytes(value);

                    Assert.AreEqual<byte>(bytes[0], bin64.b0);
                    Assert.AreEqual<byte>(bytes[1], bin64.b1);
                    Assert.AreEqual<byte>(bytes[2], bin64.b2);
                    Assert.AreEqual<byte>(bytes[3], bin64.b3);
                    Assert.AreEqual<byte>(bytes[4], bin64.b4);
                    Assert.AreEqual<byte>(bytes[5], bin64.b5);
                    Assert.AreEqual<byte>(bytes[6], bin64.b6);
                    Assert.AreEqual<byte>(bytes[7], bin64.b7);
                }
            }
        }

        [TestMethod()]
        public void BytesToInt64()
        {
            foreach ((byte[] bytes, long value) in GetInt64Pairs())
            {
                // Test byte param method
                Assert.AreEqual<long>(value, BinaryConverter.ToInt64(bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6], bytes[7]));

                // Test binary param method
                Assert.AreEqual<long>(value, BinaryConverter.ToInt64(new Binary64(bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6], bytes[7])));
            }
        }

        [TestMethod()]
        public void GetBytesFromUInt64()
        {
            foreach ((byte[] bytes, ulong value) in GetUInt64Pairs())
            {
                Console.WriteLine(GenerateGetBytesLogMessage(value, bytes));

                // Test out-byte method
                {
                    BinaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7);

                    Assert.AreEqual<byte>(bytes[0], b0);
                    Assert.AreEqual<byte>(bytes[1], b1);
                    Assert.AreEqual<byte>(bytes[2], b2);
                    Assert.AreEqual<byte>(bytes[3], b3);
                    Assert.AreEqual<byte>(bytes[4], b4);
                    Assert.AreEqual<byte>(bytes[5], b5);
                    Assert.AreEqual<byte>(bytes[6], b6);
                    Assert.AreEqual<byte>(bytes[7], b7);
                }

                // Test binary-return method
                {
                    var bin64 = BinaryConverter.GetBytes(value);

                    Assert.AreEqual<byte>(bytes[0], bin64.b0);
                    Assert.AreEqual<byte>(bytes[1], bin64.b1);
                    Assert.AreEqual<byte>(bytes[2], bin64.b2);
                    Assert.AreEqual<byte>(bytes[3], bin64.b3);
                    Assert.AreEqual<byte>(bytes[4], bin64.b4);
                    Assert.AreEqual<byte>(bytes[5], bin64.b5);
                    Assert.AreEqual<byte>(bytes[6], bin64.b6);
                    Assert.AreEqual<byte>(bytes[7], bin64.b7);
                }
            }
        }

        [TestMethod()]
        public void BytesToUInt64()
        {
            foreach ((byte[] bytes, ulong value) in GetUInt64Pairs())
            {
                // Test byte param method
                Assert.AreEqual<ulong>(value, BinaryConverter.ToUInt64(bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6], bytes[7]));

                // Test binary param method
                Assert.AreEqual<ulong>(value, BinaryConverter.ToUInt64(new Binary64(bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6], bytes[7])));
            }
        }

        [TestMethod()]
        public void GetBytesFromSingle()
        {
            foreach ((byte[] bytes, float value) in GetSinglePairs())
            {
                Console.WriteLine(GenerateGetBytesLogMessage(value, bytes));

                // Test out-byte method
                {
                    BinaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3);

                    Console.WriteLine($"{b0}, {b1}, {b2}, {b3}");
                    Assert.AreEqual<byte>(bytes[0], b0);
                    Assert.AreEqual<byte>(bytes[1], b1);
                    Assert.AreEqual<byte>(bytes[2], b2);
                    Assert.AreEqual<byte>(bytes[3], b3);
                }

                // Test binary-return method
                {
                    var bin32 = BinaryConverter.GetBytes(value);

                    Assert.AreEqual<byte>(bytes[0], bin32.b0);
                    Assert.AreEqual<byte>(bytes[1], bin32.b1);
                    Assert.AreEqual<byte>(bytes[2], bin32.b2);
                    Assert.AreEqual<byte>(bytes[3], bin32.b3);
                }
            }
        }

        [TestMethod()]
        public void BytesToSingle()
        {
            foreach ((byte[] bytes, float value) in GetSinglePairs())
            {
                // Test byte param method
                Assert.AreEqual<float>(value, BinaryConverter.ToSingle(bytes[0], bytes[1], bytes[2], bytes[3]));

                // Test binary param method
                Assert.AreEqual<float>(value, BinaryConverter.ToSingle(new Binary32(bytes[0], bytes[1], bytes[2], bytes[3])));
            }
        }

        [TestMethod()]
        public void GetBytesFromDouble()
        {
            foreach ((byte[] bytes, double value) in GetDoublePairs())
            {
                Console.WriteLine(GenerateGetBytesLogMessage(value, bytes));

                // Test out-byte method
                {
                    BinaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7);

                    Assert.AreEqual<byte>(bytes[0], b0);
                    Assert.AreEqual<byte>(bytes[1], b1);
                    Assert.AreEqual<byte>(bytes[2], b2);
                    Assert.AreEqual<byte>(bytes[3], b3);
                    Assert.AreEqual<byte>(bytes[4], b4);
                    Assert.AreEqual<byte>(bytes[5], b5);
                    Assert.AreEqual<byte>(bytes[6], b6);
                    Assert.AreEqual<byte>(bytes[7], b7);
                }

                // Test binary-return method
                {
                    var bin64 = BinaryConverter.GetBytes(value);

                    Assert.AreEqual<byte>(bytes[0], bin64.b0);
                    Assert.AreEqual<byte>(bytes[1], bin64.b1);
                    Assert.AreEqual<byte>(bytes[2], bin64.b2);
                    Assert.AreEqual<byte>(bytes[3], bin64.b3);
                    Assert.AreEqual<byte>(bytes[4], bin64.b4);
                    Assert.AreEqual<byte>(bytes[5], bin64.b5);
                    Assert.AreEqual<byte>(bytes[6], bin64.b6);
                    Assert.AreEqual<byte>(bytes[7], bin64.b7);
                }
            }
        }

        [TestMethod()]
        public void BytesToDouble()
        {
            foreach ((byte[] bytes, double value) in GetDoublePairs())
            {
                // Test byte param method
                Assert.AreEqual<double>(value, BinaryConverter.ToDouble(bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6], bytes[7]));

                // Test binary param method
                Assert.AreEqual<double>(value, BinaryConverter.ToDouble(new Binary64(bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6], bytes[7])));
            }
        }
    }
}
