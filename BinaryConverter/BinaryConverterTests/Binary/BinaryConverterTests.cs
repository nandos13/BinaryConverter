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
            return new (byte, bool)[]
            {
                (0,   false),
                (1,   true),
                (127, true),
                (128, true),
                (255, true)
            };
        }

        private static (byte b, sbyte value)[] GetSBytePairs()
        {
            return new (byte, sbyte)[]
            {
                (0,   0),
                (1,   1),
                (127, 127),
                (128, -128),
                (255, -1)
            };
        }

        private static (byte[] bytes, char value)[] GetCharPairs()
        {
            return new (byte[], char)[]
            {
                (new byte[2] { 0, 0 },     (char)0),
                (new byte[2] { 0, 1 },     (char)1),
                (new byte[2] { 0, 128 },   (char)128),
                (new byte[2] { 0, 255 },   (char)255),
                (new byte[2] { 1, 0 },     (char)256),
                (new byte[2] { 4, 0 },     (char)1024),
                (new byte[2] { 255, 255 }, (char)65535)
            };
        }

        private static (byte[] bytes, short value)[] GetInt16Pairs()
        {
            return new (byte[], short)[]
            {
                (new byte[2] { 0, 0 },     0),
                (new byte[2] { 0, 1 },     1),
                (new byte[2] { 0, 128 },   128),
                (new byte[2] { 0, 255 },   255),
                (new byte[2] { 1, 0 },     256),
                (new byte[2] { 4, 0 },     1024),
                (new byte[2] { 127, 255 },   32767),
                (new byte[2] { 128, 0 },   -32768),
                (new byte[2] { 255, 255 }, -1)
            };
        }

        private static (byte[] bytes, ushort value)[] GetUInt16Pairs()
        {
            return new (byte[], ushort)[]
            {
                (new byte[2] { 0, 0 },     0),
                (new byte[2] { 0, 1 },     1),
                (new byte[2] { 0, 128 },   128),
                (new byte[2] { 0, 255 },   255),
                (new byte[2] { 1, 0 },     256),
                (new byte[2] { 4, 0 },     1024),
                (new byte[2] { 255, 255 }, 65535)
            };
        }

        private static (byte[] bytes, int value)[] GetInt32Pairs()
        {
            return new (byte[], int)[]
            {
                (new byte[4] { 0, 0, 0, 0 },         0),
                (new byte[4] { 0, 0, 0, 1 },         1),
                (new byte[4] { 0, 0, 0, 128 },       128),
                (new byte[4] { 0, 0, 0, 255 },       255),
                (new byte[4] { 0, 0, 1, 0 },         256),
                (new byte[4] { 0, 0, 4, 0 },         1024),
                (new byte[4] { 0, 0, 127, 255 },     32767),
                (new byte[4] { 0, 128, 0, 0 },       8388608),
                (new byte[4] { 127, 255, 255, 255 }, 2147483647),
                (new byte[4] { 128, 0, 0, 0 },       -2147483648),
                (new byte[4] { 255, 255, 128, 0 },   -32768),
                (new byte[4] { 255, 255, 255, 255 }, -1)
            };
        }

        private static (byte[] bytes, uint value)[] GetUInt32Pairs()
        {
            return new (byte[], uint)[]
            {
                (new byte[4] { 0, 0, 0, 0 },         0),
                (new byte[4] { 0, 0, 0, 1 },         1),
                (new byte[4] { 0, 0, 0, 128 },       128),
                (new byte[4] { 0, 0, 0, 255 },       255),
                (new byte[4] { 0, 0, 1, 0 },         256),
                (new byte[4] { 0, 0, 4, 0 },         1024),
                (new byte[4] { 0, 0, 127, 255 },     32767),
                (new byte[4] { 0, 128, 0, 0 },       8388608),
                (new byte[4] { 127, 255, 255, 255 }, 2147483647),
                (new byte[4] { 128, 0, 0, 0},        2147483648),
                (new byte[4] { 255, 255, 255, 255 }, 4294967295)
            };
        }

        [TestMethod()]
        public void Test_GetBytes_Bool()
        {
            Assert.AreEqual<byte>(0, BinaryConverter.GetBytes(false));

            Assert.AreNotEqual<byte>(0, BinaryConverter.GetBytes(true));
        }

        [TestMethod()]
        public void Test_ToBoolean()
        {
            foreach ((byte b, bool value) in GetBooleanPairs())
            {
                Assert.AreEqual<bool>(value, BinaryConverter.ToBoolean(b));
            }
        }

        [TestMethod()]
        public void Test_GetBytes_SByte()
        {
            foreach ((byte b, sbyte value) in GetSBytePairs())
            {
                Console.WriteLine(GenerateGetBytesLogMessage(value, b));
                Assert.AreEqual<byte>(b, BinaryConverter.GetBytes(value));
            }
        }

        [TestMethod()]
        public void Test_ToSByte()
        {
            foreach ((byte b, sbyte value) in GetSBytePairs())
            {
                Assert.AreEqual<sbyte>(value, BinaryConverter.ToSByte(b));
            }
        }

        [TestMethod()]
        public void Test_GetBytes_Char()
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
        public void Test_ToChar()
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
        public void Test_GetBytes_Int16()
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
        public void Test_ToInt16()
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
        public void Test_GetBytes_UInt16()
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
        public void Test_ToUInt16()
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
        public void Test_GetBytes_Int32()
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
        public void Test_ToInt32()
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
        public void Test_GetBytes_UInt32()
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
        public void Test_ToUInt32()
        {
            foreach ((byte[] bytes, uint value) in GetUInt32Pairs())
            {
                // Test byte param method
                Assert.AreEqual<uint>(value, BinaryConverter.ToUInt32(bytes[0], bytes[1], bytes[2], bytes[3]));

                // Test binary param method
                Assert.AreEqual<uint>(value, BinaryConverter.ToUInt32(new Binary32(bytes[0], bytes[1], bytes[2], bytes[3])));
            }
        }
    }
}
