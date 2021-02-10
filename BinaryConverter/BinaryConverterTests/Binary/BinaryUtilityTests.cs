using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace JPAssets.Binary.Tests
{
    [TestClass()]
    public class BinaryUtilityTests
    {
        private const int kRandomTestIterations = 16;

        private static Random GetRandomAndLogSeed()
        {
            int seed = Environment.TickCount;
            Console.WriteLine($"Executing test with random seed: {seed.ToString()}.");

            return new Random(seed);
        }

        [TestMethod()]
        public void TestReverseEndiannessThrowsForNullByteArray()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                BinaryUtility.ReverseEndianness((byte[])null);
            });

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                BinaryUtility.ReverseEndianness((byte[])null, 0, 0);
            });
        }

        [TestMethod()]
        public unsafe void TestReverseEndiannessThrowsWhenOffsetOrCountInvalid()
        {
            const int length = 4;
            const int halfLength = length / 2;

            // Test negative offset
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                BinaryUtility.ReverseEndianness(new byte[length], -1, halfLength);
            });

            // Test negative count
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                BinaryUtility.ReverseEndianness(new byte[length], 0, -1);
            });
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                var bytes = new byte[length];
                fixed (byte* ptr = bytes)
                    BinaryUtility.ReverseEndianness(ptr, -1);
            });

            // Test offset >= length
            Assert.ThrowsException<ArgumentException>(() =>
            {
                BinaryUtility.ReverseEndianness(new byte[length], length, halfLength);
            });
            Assert.ThrowsException<ArgumentException>(() =>
            {
                BinaryUtility.ReverseEndianness(new byte[length], length + 1, halfLength);
            });

            // Test count overflow
            Assert.ThrowsException<ArgumentException>(() =>
            {
                BinaryUtility.ReverseEndianness(new byte[length], 0, length + 1);
            });
            Assert.ThrowsException<ArgumentException>(() =>
            {
                BinaryUtility.ReverseEndianness(new byte[length], 1, length);
            });

            // Test the method does not fail with valid input
            BinaryUtility.ReverseEndianness(new byte[length], 0, 0);
            BinaryUtility.ReverseEndianness(new byte[length], 0, 1);
            BinaryUtility.ReverseEndianness(new byte[length], 0, halfLength);
            BinaryUtility.ReverseEndianness(new byte[length], 0, length);
            BinaryUtility.ReverseEndianness(new byte[length], halfLength, 1);
            BinaryUtility.ReverseEndianness(new byte[length], halfLength, halfLength);
        }

        [TestMethod()]
        public unsafe void ReverseEndiannessForBytePointer()
        {
            var random = GetRandomAndLogSeed();

            // TODO: Implement tests for ReverseEndianness(byte*, int)
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ReverseEndiannessForByteArray()
        {
            var random = GetRandomAndLogSeed();

            // TODO: Implement tests for ReverseEndianness(byte[])
            // TODO: Implement tests for ReverseEndianness(byte[], int, int)
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ReverseEndiannessForTwoBytes()
        {
            var random = GetRandomAndLogSeed();
            byte[] buffer = new byte[2];

            for (int i = 0; i < kRandomTestIterations; i++)
            {
                // Get random byte values
                random.NextBytes(buffer);

                // Copy to local variables
                byte b0 = buffer[0];
                byte b1 = buffer[1];

                // Reverse, compare to expected
                BinaryUtility.ReverseEndianness(ref b0, ref b1);
                Assert.AreEqual<byte>(buffer[1], b0);
                Assert.AreEqual<byte>(buffer[0], b1);

                // Reverse to original order & compare again
                BinaryUtility.ReverseEndianness(ref b0, ref b1);
                Assert.AreEqual<byte>(buffer[0], b0);
                Assert.AreEqual<byte>(buffer[1], b1);
            }
        }

        [TestMethod()]
        public void ReverseEndiannessForFourBytes()
        {
            var random = GetRandomAndLogSeed();
            byte[] buffer = new byte[4];

            for (int i = 0; i < kRandomTestIterations; i++)
            {
                // Get random byte values
                random.NextBytes(buffer);

                // Copy to local variables
                byte b0 = buffer[0];
                byte b1 = buffer[1];
                byte b2 = buffer[2];
                byte b3 = buffer[3];

                // Reverse, compare to expected
                BinaryUtility.ReverseEndianness(ref b0, ref b1, ref b2, ref b3);
                Assert.AreEqual<byte>(buffer[3], b0);
                Assert.AreEqual<byte>(buffer[2], b1);
                Assert.AreEqual<byte>(buffer[1], b2);
                Assert.AreEqual<byte>(buffer[0], b3);

                // Reverse to original order & compare again
                BinaryUtility.ReverseEndianness(ref b0, ref b1, ref b2, ref b3);
                Assert.AreEqual<byte>(buffer[0], b0);
                Assert.AreEqual<byte>(buffer[1], b1);
                Assert.AreEqual<byte>(buffer[2], b2);
                Assert.AreEqual<byte>(buffer[3], b3);
            }
        }

        [TestMethod()]
        public void ReverseEndiannessForEightBytes()
        {
            var random = GetRandomAndLogSeed();
            byte[] buffer = new byte[8];

            for (int i = 0; i < kRandomTestIterations; i++)
            {
                // Get random byte values
                random.NextBytes(buffer);

                // Copy to local variables
                byte b0 = buffer[0];
                byte b1 = buffer[1];
                byte b2 = buffer[2];
                byte b3 = buffer[3];
                byte b4 = buffer[4];
                byte b5 = buffer[5];
                byte b6 = buffer[6];
                byte b7 = buffer[7];

                // Reverse, compare to expected
                BinaryUtility.ReverseEndianness(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref b6, ref b7);
                Assert.AreEqual<byte>(buffer[7], b0);
                Assert.AreEqual<byte>(buffer[6], b1);
                Assert.AreEqual<byte>(buffer[5], b2);
                Assert.AreEqual<byte>(buffer[4], b3);
                Assert.AreEqual<byte>(buffer[3], b4);
                Assert.AreEqual<byte>(buffer[2], b5);
                Assert.AreEqual<byte>(buffer[1], b6);
                Assert.AreEqual<byte>(buffer[0], b7);

                // Reverse to original order & compare again
                BinaryUtility.ReverseEndianness(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref b6, ref b7);
                Assert.AreEqual<byte>(buffer[0], b0);
                Assert.AreEqual<byte>(buffer[1], b1);
                Assert.AreEqual<byte>(buffer[2], b2);
                Assert.AreEqual<byte>(buffer[3], b3);
                Assert.AreEqual<byte>(buffer[4], b4);
                Assert.AreEqual<byte>(buffer[5], b5);
                Assert.AreEqual<byte>(buffer[6], b6);
                Assert.AreEqual<byte>(buffer[7], b7);
            }
        }

        [TestMethod()]
        public void ReverseEndiannessForBinary16()
        {
            var random = GetRandomAndLogSeed();
            byte[] buffer = new byte[2];

            for (int i = 0; i < kRandomTestIterations; i++)
            {
                // Get random byte values
                random.NextBytes(buffer);

                // Create bin
                var bin = new Binary16(buffer[0], buffer[1]);

                // Reverse, compare to expected
                var reversed = BinaryUtility.ReverseEndianness(bin);
                Assert.AreEqual<byte>(bin.b1, reversed.b0);
                Assert.AreEqual<byte>(bin.b0, reversed.b1);

                // Reverse to original order & compare again
                var doubleReversed = BinaryUtility.ReverseEndianness(reversed);
                Assert.AreEqual<Binary16>(bin, doubleReversed);
            }
        }

        [TestMethod()]
        public void ReverseEndiannessForBinary32()
        {
            var random = GetRandomAndLogSeed();
            byte[] buffer = new byte[4];

            for (int i = 0; i < kRandomTestIterations; i++)
            {
                // Get random byte values
                random.NextBytes(buffer);

                // Create bin
                var bin = new Binary32(buffer[0], buffer[1], buffer[2], buffer[3]);

                // Reverse, compare to expected
                var reversed = BinaryUtility.ReverseEndianness(bin);
                Assert.AreEqual<byte>(bin.b3, reversed.b0);
                Assert.AreEqual<byte>(bin.b2, reversed.b1);
                Assert.AreEqual<byte>(bin.b1, reversed.b2);
                Assert.AreEqual<byte>(bin.b0, reversed.b3);

                // Reverse to original order & compare again
                var doubleReversed = BinaryUtility.ReverseEndianness(reversed);
                Assert.AreEqual<Binary32>(bin, doubleReversed);
            }
        }

        [TestMethod()]
        public void ReverseEndiannessForBinary64()
        {
            var random = GetRandomAndLogSeed();
            byte[] buffer = new byte[8];

            for (int i = 0; i < kRandomTestIterations; i++)
            {
                // Get random byte values
                random.NextBytes(buffer);

                // Create bin
                var bin = new Binary64(buffer[0], buffer[1], buffer[2], buffer[3], buffer[4], buffer[5], buffer[6], buffer[7]);

                // Reverse, compare to expected
                var reversed = BinaryUtility.ReverseEndianness(bin);
                Assert.AreEqual<byte>(bin.b7, reversed.b0);
                Assert.AreEqual<byte>(bin.b6, reversed.b1);
                Assert.AreEqual<byte>(bin.b5, reversed.b2);
                Assert.AreEqual<byte>(bin.b4, reversed.b3);
                Assert.AreEqual<byte>(bin.b3, reversed.b4);
                Assert.AreEqual<byte>(bin.b2, reversed.b5);
                Assert.AreEqual<byte>(bin.b1, reversed.b6);
                Assert.AreEqual<byte>(bin.b0, reversed.b7);

                // Reverse to original order & compare again
                var doubleReversed = BinaryUtility.ReverseEndianness(reversed);
                Assert.AreEqual<Binary64>(bin, doubleReversed);
            }
        }
    }
}