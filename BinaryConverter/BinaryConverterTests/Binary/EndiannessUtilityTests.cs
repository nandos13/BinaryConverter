using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

using static JPAssets.Binary.Tests.CommonTestUtility;

namespace JPAssets.Binary.Tests
{
    // TODO: Add test method names.
    // TODO: Implement tests for all EndiannessUtility methods.

    [TestClass()]
    public class EndiannessUtilityTests
    {
        [TestMethod()]
        public void TestReverseEndiannessThrowsForNullByteArray()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                EndiannessUtility.ReverseEndianness((byte[])null);
            });

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                EndiannessUtility.ReverseEndianness((byte[])null, 0, 0);
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
                EndiannessUtility.ReverseEndianness(new byte[length], -1, halfLength);
            });

            // Test negative count
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                EndiannessUtility.ReverseEndianness(new byte[length], 0, -1);
            });
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                var bytes = new byte[length];
                fixed (byte* ptr = bytes)
                    EndiannessUtility.ReverseEndianness(ptr, -1);
            });

            // Test offset >= length
            Assert.ThrowsException<ArgumentException>(() =>
            {
                EndiannessUtility.ReverseEndianness(new byte[length], length, halfLength);
            });
            Assert.ThrowsException<ArgumentException>(() =>
            {
                EndiannessUtility.ReverseEndianness(new byte[length], length + 1, halfLength);
            });

            // Test count overflow
            Assert.ThrowsException<ArgumentException>(() =>
            {
                EndiannessUtility.ReverseEndianness(new byte[length], 0, length + 1);
            });
            Assert.ThrowsException<ArgumentException>(() =>
            {
                EndiannessUtility.ReverseEndianness(new byte[length], 1, length);
            });

            // Test the method does not fail with valid input
            EndiannessUtility.ReverseEndianness(new byte[length], 0, 0);
            EndiannessUtility.ReverseEndianness(new byte[length], 0, 1);
            EndiannessUtility.ReverseEndianness(new byte[length], 0, halfLength);
            EndiannessUtility.ReverseEndianness(new byte[length], 0, length);
            EndiannessUtility.ReverseEndianness(new byte[length], halfLength, 1);
            EndiannessUtility.ReverseEndianness(new byte[length], halfLength, halfLength);
        }

        [TestMethod()]
        public void ReverseEndiannessForByteArray()
        {
            var random = GetRandomAndLogSeed();

            foreach (var length in new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 16, 32, 64 })
            {
                // Get random bytes
                var buffer = new byte[length];
                random.NextBytes(buffer);

                // Copy original buffer state
                var copyBuffer = new byte[length];
                Array.Copy(buffer, 0, copyBuffer, 0, length);

                // Reverse entire buffer & test
                EndiannessUtility.ReverseEndianness(buffer, 0, length);
                for (int i = 0; i < length; i++)
                    Assert.AreEqual<byte>(copyBuffer[i], buffer[length - i - 1]);

                // Reverse back to original endianness order & test
                EndiannessUtility.ReverseEndianness(buffer, 0, length);
                for (int i = 0; i < length; i++)
                    Assert.AreEqual<byte>(copyBuffer[i], buffer[i]);
            }

            // TODO: Implement tests for ReverseEndianness(byte[])
            // TODO: Implement tests for ReverseEndianness(byte[], int, int)
            throw new NotImplementedException();
        }
    }
}
