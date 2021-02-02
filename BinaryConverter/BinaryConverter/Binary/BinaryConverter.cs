namespace JPAssets.Binary
{
    // TODO: Implement unit tests.
    public static class BinaryConverter
    {
        public static byte GetBytes(bool value)
        {
            return value ? (byte)1 : (byte)0;
        }

        public static byte GetBytes(sbyte value)
        {
            return (byte)value;
        }

        public static void GetBytes(char value, out byte b0, out byte b1)
        {
            b0 = (byte)(value);
            b1 = (byte)(value >> 8);
        }

        public static void GetBytes(short value, out byte b0, out byte b1)
        {
            b0 = (byte)(value);
            b1 = (byte)(value >> 8);
        }

        public static void GetBytes(ushort value, out byte b0, out byte b1)
        {
            b0 = (byte)(value);
            b1 = (byte)(value >> 8);
        }

        public static void GetBytes(int value, out byte b0, out byte b1, out byte b2, out byte b3)
        {
            b0 = (byte)(value);
            b1 = (byte)(value >> 8);
            b2 = (byte)(value >> 16);
            b3 = (byte)(value >> 24);
        }

        public static void GetBytes(uint value, out byte b0, out byte b1, out byte b2, out byte b3)
        {
            b0 = (byte)(value);
            b1 = (byte)(value >> 8);
            b2 = (byte)(value >> 16);
            b3 = (byte)(value >> 24);
        }

        public static void GetBytes(long value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7)
        {
            b0 = (byte)(value);
            b1 = (byte)(value >> 8);
            b2 = (byte)(value >> 16);
            b3 = (byte)(value >> 24);
            b4 = (byte)(value >> 32);
            b5 = (byte)(value >> 40);
            b6 = (byte)(value >> 48);
            b7 = (byte)(value >> 56);
        }

        public static void GetBytes(ulong value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7)
        {
            b0 = (byte)(value);
            b1 = (byte)(value >> 8);
            b2 = (byte)(value >> 16);
            b3 = (byte)(value >> 24);
            b4 = (byte)(value >> 32);
            b5 = (byte)(value >> 40);
            b6 = (byte)(value >> 48);
            b7 = (byte)(value >> 56);
        }

        public static unsafe void GetBytes(float value, out byte b0, out byte b1, out byte b2, out byte b3)
        {
            var temp = *(uint*)&value;

            b0 = (byte)(temp);
            b1 = (byte)(temp >> 8);
            b2 = (byte)(temp >> 16);
            b3 = (byte)(temp >> 24);
        }

        public static unsafe void GetBytes(double value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7)
        {
            var temp = *(ulong*)&value;

            b0 = (byte)(temp);
            b1 = (byte)(temp >> 8);
            b2 = (byte)(temp >> 16);
            b3 = (byte)(temp >> 24);
            b4 = (byte)(temp >> 32);
            b5 = (byte)(temp >> 40);
            b6 = (byte)(temp >> 48);
            b7 = (byte)(temp >> 56);
        }

        public static Binary16 GetBytes(char value)
        {
            return new Binary16(
                b0: (byte)(value),
                b1: (byte)(value >> 8)
                );
        }

        public static Binary16 GetBytes(short value)
        {
            return new Binary16(
                b0: (byte)(value),
                b1: (byte)(value >> 8)
                );
        }

        public static Binary16 GetBytes(ushort value)
        {
            return new Binary16(
                b0: (byte)(value),
                b1: (byte)(value >> 8)
                );
        }

        public static Binary32 GetBytes(int value)
        {
            return new Binary32(
                b0: (byte)(value),
                b1: (byte)(value >> 8),
                b2: (byte)(value >> 16),
                b3: (byte)(value >> 24)
                );
        }

        public static Binary32 GetBytes(uint value)
        {
            return new Binary32(
                b0: (byte)(value),
                b1: (byte)(value >> 8),
                b2: (byte)(value >> 16),
                b3: (byte)(value >> 24)
                );
        }

        public static Binary64 GetBytes(long value)
        {
            return new Binary64(
                b0: (byte)(value),
                b1: (byte)(value >> 8),
                b2: (byte)(value >> 16),
                b3: (byte)(value >> 24),
                b4: (byte)(value >> 32),
                b5: (byte)(value >> 40),
                b6: (byte)(value >> 48),
                b7: (byte)(value >> 56)
                );
        }

        public static Binary64 GetBytes(ulong value)
        {
            return new Binary64(
                b0: (byte)(value),
                b1: (byte)(value >> 8),
                b2: (byte)(value >> 16),
                b3: (byte)(value >> 24),
                b4: (byte)(value >> 32),
                b5: (byte)(value >> 40),
                b6: (byte)(value >> 48),
                b7: (byte)(value >> 56)
                );
        }

        public static unsafe Binary32 GetBytes(float value)
        {
            var temp = *(uint*)&value;

            return new Binary32(
                b0: (byte)(temp),
                b1: (byte)(temp >> 8),
                b2: (byte)(temp >> 16),
                b3: (byte)(temp >> 24)
                );
        }

        public static unsafe Binary64 GetBytes(double value)
        {
            var temp = *(ulong*)&value;

            return new Binary64(
                b0: (byte)(temp),
                b1: (byte)(temp >> 8),
                b2: (byte)(temp >> 16),
                b3: (byte)(temp >> 24),
                b4: (byte)(temp >> 32),
                b5: (byte)(temp >> 40),
                b6: (byte)(temp >> 48),
                b7: (byte)(temp >> 56)
                );
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
            return (char)(b0 | b1 << 8);
        }

        public static short ToInt16(byte b0, byte b1)
        {
            return (short)(b0 | b1 << 8);
        }

        public static ushort ToUInt16(byte b0, byte b1)
        {
            return (ushort)(b0 | b1 << 8);
        }

        public static int ToInt32(byte b0, byte b1, byte b2, byte b3)
        {
            return (int)(b0 | b1 << 8 | b2 << 16 | b3 << 24);
        }

        public static uint ToUInt32(byte b0, byte b1, byte b2, byte b3)
        {
            return (uint)(b0 | b1 << 8 | b2 << 16 | b3 << 24);
        }

        public static long ToInt64(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            int i1 = (int)(b0 | b1 << 8 | b2 << 16 | b3 << 24);
            int i2 = (int)(b4 | b5 << 8 | b6 << 16 | b7 << 24);
            return (uint)i1 | (long)i2 << 32;
        }

        public static ulong ToUInt64(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            int i1 = (int)(b0 | b1 << 8 | b2 << 16 | b3 << 24);
            int i2 = (int)(b4 | b5 << 8 | b6 << 16 | b7 << 24);
            return (ulong)((uint)i1 | (long)i2 << 32);
        }

        public static unsafe float ToSingle(byte b0, byte b1, byte b2, byte b3)
        {
            var value = ToInt32(b0, b1, b2, b3);
            return *(float*)&value;
        }

        public static unsafe double ToDouble(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            var value = ToInt64(b0, b1, b2, b3, b4, b5, b6, b7);
            return *(double*)&value;
        }

        public static char ToChar(Binary16 bin)
        {
            return (char)(bin.b0 | bin.b1 << 8);
        }

        public static short ToInt16(Binary16 bin)
        {
            return (short)(bin.b0 | bin.b1 << 8);
        }

        public static ushort ToUInt16(Binary16 bin)
        {
            return (ushort)(bin.b0 | bin.b1 << 8);
        }

        public static int ToInt32(Binary32 bin)
        {
            return (int)(bin.b0 | bin.b1 << 8 | bin.b2 << 16 | bin.b3 << 24);
        }

        public static uint ToUInt32(Binary32 bin)
        {
            return (uint)(bin.b0 | bin.b1 << 8 | bin.b2 << 16 | bin.b3 << 24);
        }

        public static long ToInt64(Binary64 bin)
        {
            int i1 = (int)(bin.b0 | bin.b1 << 8 | bin.b2 << 16 | bin.b3 << 24);
            int i2 = (int)(bin.b4 | bin.b5 << 8 | bin.b6 << 16 | bin.b7 << 24);
            return (uint)i1 | (long)i2 << 32;
        }

        public static ulong ToUInt64(Binary64 bin)
        {
            int i1 = (int)(bin.b0 | bin.b1 << 8 | bin.b2 << 16 | bin.b3 << 24);
            int i2 = (int)(bin.b4 | bin.b5 << 8 | bin.b6 << 16 | bin.b7 << 24);
            return (ulong)((uint)i1 | (long)i2 << 32);
        }

        public static unsafe float ToSingle(Binary32 bin)
        {
            var value = ToInt32(bin.b0, bin.b1, bin.b2, bin.b3);
            return *(float*)&value;
        }

        public static unsafe double ToDouble(Binary64 bin)
        {
            var value = ToInt64(bin.b0, bin.b1, bin.b2, bin.b3, bin.b4, bin.b5, bin.b6, bin.b7);
            return *(double*)&value;
        }
    }
}
