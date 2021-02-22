using System;

namespace JPAssets.Binary
{
    public static class BinaryConverterExtensions
    {
        public static Binary16 GetBytes(this BinaryConverter binaryConverter, char value)
        {
            _ = binaryConverter ?? throw new ArgumentNullException(nameof(binaryConverter));

            binaryConverter.GetBytes(value, out byte b0, out byte b1);
            return new Binary16(b0, b1);
        }

        public static Binary16 GetBytes(this BinaryConverter binaryConverter, short value)
        {
            _ = binaryConverter ?? throw new ArgumentNullException(nameof(binaryConverter));

            binaryConverter.GetBytes(value, out byte b0, out byte b1);
            return new Binary16(b0, b1);
        }

        public static Binary16 GetBytes(this BinaryConverter binaryConverter, ushort value)
        {
            _ = binaryConverter ?? throw new ArgumentNullException(nameof(binaryConverter));

            binaryConverter.GetBytes(value, out byte b0, out byte b1);
            return new Binary16(b0, b1);
        }

        public static Binary32 GetBytes(this BinaryConverter binaryConverter, int value)
        {
            _ = binaryConverter ?? throw new ArgumentNullException(nameof(binaryConverter));

            binaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3);
            return new Binary32(b0, b1, b2, b3);
        }

        public static Binary32 GetBytes(this BinaryConverter binaryConverter, uint value)
        {
            _ = binaryConverter ?? throw new ArgumentNullException(nameof(binaryConverter));

            binaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3);
            return new Binary32(b0, b1, b2, b3);
        }

        public static Binary64 GetBytes(this BinaryConverter binaryConverter, long value)
        {
            _ = binaryConverter ?? throw new ArgumentNullException(nameof(binaryConverter));

            binaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7);
            return new Binary64(b0, b1, b2, b3, b4, b5, b6, b7);
        }

        public static Binary64 GetBytes(this BinaryConverter binaryConverter, ulong value)
        {
            _ = binaryConverter ?? throw new ArgumentNullException(nameof(binaryConverter));

            binaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7);
            return new Binary64(b0, b1, b2, b3, b4, b5, b6, b7);
        }

        public static Binary32 GetBytes(this BinaryConverter binaryConverter, float value)
        {
            _ = binaryConverter ?? throw new ArgumentNullException(nameof(binaryConverter));

            binaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3);
            return new Binary32(b0, b1, b2, b3);
        }

        public static Binary64 GetBytes(this BinaryConverter binaryConverter, double value)
        {
            _ = binaryConverter ?? throw new ArgumentNullException(nameof(binaryConverter));

            binaryConverter.GetBytes(value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7);
            return new Binary64(b0, b1, b2, b3, b4, b5, b6, b7);
        }

        public static char ToChar(this BinaryConverter binaryConverter, Binary16 bin)
        {
            _ = binaryConverter ?? throw new ArgumentNullException(nameof(binaryConverter));

            return binaryConverter.ToChar(bin.b0, bin.b1);
        }

        public static short ToInt16(this BinaryConverter binaryConverter, Binary16 bin)
        {
            _ = binaryConverter ?? throw new ArgumentNullException(nameof(binaryConverter));

            return binaryConverter.ToInt16(bin.b0, bin.b1);
        }

        public static ushort ToUInt16(this BinaryConverter binaryConverter, Binary16 bin)
        {
            _ = binaryConverter ?? throw new ArgumentNullException(nameof(binaryConverter));

            return binaryConverter.ToUInt16(bin.b0, bin.b1);
        }

        public static int ToInt32(this BinaryConverter binaryConverter, Binary32 bin)
        {
            _ = binaryConverter ?? throw new ArgumentNullException(nameof(binaryConverter));

            return binaryConverter.ToInt32(bin.b0, bin.b1, bin.b2, bin.b3);
        }

        public static uint ToUInt32(this BinaryConverter binaryConverter, Binary32 bin)
        {
            _ = binaryConverter ?? throw new ArgumentNullException(nameof(binaryConverter));

            return binaryConverter.ToUInt32(bin.b0, bin.b1, bin.b2, bin.b3);
        }

        public static long ToInt64(this BinaryConverter binaryConverter, Binary64 bin)
        {
            _ = binaryConverter ?? throw new ArgumentNullException(nameof(binaryConverter));

            return binaryConverter.ToInt64(bin.b0, bin.b1, bin.b2, bin.b3, bin.b4, bin.b5, bin.b6, bin.b7);
        }

        public static ulong ToUInt64(this BinaryConverter binaryConverter, Binary64 bin)
        {
            _ = binaryConverter ?? throw new ArgumentNullException(nameof(binaryConverter));

            return binaryConverter.ToUInt64(bin.b0, bin.b1, bin.b2, bin.b3, bin.b4, bin.b5, bin.b6, bin.b7);
        }
    }
}
