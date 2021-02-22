using System.Runtime.CompilerServices;

namespace JPAssets.Binary
{
    // TODO: Add ability to "swizzle" (reverse) high & low 32-bit sequences for System.Double.
    //       See https://en.wikipedia.org/wiki/Endianness#Floating_point
    // TODO: Add method documentation.

    /// <summary>
    /// The <see cref="BinaryConverter"/> class contains methods for converting most base data types
    /// to their byte values, as well as convert arbitrary bytes into base data types.
    /// </summary>
    public sealed class BinaryConverter
    {
        /// <inheritdoc cref="System.BitConverter.IsLittleEndian"/>
        private static bool IsEnvironmentLittleEndian => System.BitConverter.IsLittleEndian;

        private readonly bool m_bigInts;
        private readonly bool m_bigFloats;
        private readonly bool m_bigEnvironment;

        public Endianness IntegerOrder => m_bigInts ? Endianness.Big : Endianness.Little;

        public Endianness FloatingPointOrder => m_bigFloats ? Endianness.Big : Endianness.Little;

        /// <summary>
        /// Creates a new <see cref="BinaryConverter"/> instance with the given byte order ("endianness")
        /// for integer and floating-point value types.
        /// </summary>
        /// <param name="integerOrder">The endianness to be used for integer data types.</param>
        /// <param name="floatOrder">The endianness to be used for floating-point data types.</param>
        public BinaryConverter(Endianness integerOrder, Endianness floatOrder)
        {
            BinaryUtility.CheckEndianness(integerOrder, nameof(integerOrder));
            BinaryUtility.CheckEndianness(floatOrder, nameof(floatOrder));

            m_bigInts = integerOrder == Endianness.Big;
            m_bigFloats = floatOrder == Endianness.Big;
            m_bigEnvironment = !IsEnvironmentLittleEndian;
        }

        /// <param name="bigEnvironment">Specifies the environment endianness. Exposed for testing purposes only.</param>
        /// <inheritdoc cref="BinaryConverter(Endianness, Endianness)"/>
        internal BinaryConverter(Endianness integerOrder, Endianness floatOrder, bool bigEnvironment)
            : this(integerOrder, floatOrder)
        {
            m_bigEnvironment = bigEnvironment;
        }

        private static readonly BinaryConverter s_littleConverter = new BinaryConverter(Endianness.Little, Endianness.Little);
        private static readonly BinaryConverter s_bigConverter = new BinaryConverter(Endianness.Big, Endianness.Big);

        public static BinaryConverter LittleEndian => s_littleConverter;

        public static BinaryConverter BigEndian => s_bigConverter;

        public static BinaryConverter Default => IsEnvironmentLittleEndian ? s_littleConverter : s_bigConverter;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool ShouldReverseIntegers() => m_bigEnvironment != m_bigInts;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool ShouldReverseFloats() => m_bigEnvironment != m_bigFloats;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Method consistency")]
        public byte GetBytes(bool value)
        {
            return value ? (byte)1 : (byte)0;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Method consistency")]
        public byte GetBytes(sbyte value)
        {
            return (byte)value;
        }

        public void GetBytes(char value, out byte b0, out byte b1)
        {
            b0 = (byte)(value >> 8);
            b1 = (byte)(value);

            if (ShouldReverseIntegers())
                BinaryUtility.ReverseEndianness(ref b0, ref b1);
        }

        public void GetBytes(short value, out byte b0, out byte b1)
        {
            b0 = (byte)(value >> 8);
            b1 = (byte)(value);

            if (ShouldReverseIntegers())
                BinaryUtility.ReverseEndianness(ref b0, ref b1);
        }

        public void GetBytes(ushort value, out byte b0, out byte b1)
        {
            b0 = (byte)(value >> 8);
            b1 = (byte)(value);

            if (ShouldReverseIntegers())
                BinaryUtility.ReverseEndianness(ref b0, ref b1);
        }

        public void GetBytes(int value, out byte b0, out byte b1, out byte b2, out byte b3)
        {
            b0 = (byte)(value >> 24);
            b1 = (byte)(value >> 16);
            b2 = (byte)(value >> 8);
            b3 = (byte)(value);

            if (ShouldReverseIntegers())
                BinaryUtility.ReverseEndianness(ref b0, ref b1, ref b2, ref b3);
        }

        public void GetBytes(uint value, out byte b0, out byte b1, out byte b2, out byte b3)
        {
            b0 = (byte)(value >> 24);
            b1 = (byte)(value >> 16);
            b2 = (byte)(value >> 8);
            b3 = (byte)(value);

            if (ShouldReverseIntegers())
                BinaryUtility.ReverseEndianness(ref b0, ref b1, ref b2, ref b3);
        }

        public void GetBytes(long value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7)
        {
            b0 = (byte)(value >> 56);
            b1 = (byte)(value >> 48);
            b2 = (byte)(value >> 40);
            b3 = (byte)(value >> 32);
            b4 = (byte)(value >> 24);
            b5 = (byte)(value >> 16);
            b6 = (byte)(value >> 8);
            b7 = (byte)(value);

            if (ShouldReverseIntegers())
                BinaryUtility.ReverseEndianness(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref b6, ref b7);
        }

        public void GetBytes(ulong value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7)
        {
            b0 = (byte)(value >> 56);
            b1 = (byte)(value >> 48);
            b2 = (byte)(value >> 40);
            b3 = (byte)(value >> 32);
            b4 = (byte)(value >> 24);
            b5 = (byte)(value >> 16);
            b6 = (byte)(value >> 8);
            b7 = (byte)(value);

            if (ShouldReverseIntegers())
                BinaryUtility.ReverseEndianness(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref b6, ref b7);
        }

        public unsafe void GetBytes(float value, out byte b0, out byte b1, out byte b2, out byte b3)
        {
            var temp = *(uint*)&value;

            b0 = (byte)(temp >> 24);
            b1 = (byte)(temp >> 16);
            b2 = (byte)(temp >> 8);
            b3 = (byte)(temp);

            if (ShouldReverseFloats())
                BinaryUtility.ReverseEndianness(ref b0, ref b1, ref b2, ref b3);
        }

        public unsafe void GetBytes(double value, out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7)
        {
            var temp = *(ulong*)&value;

            b0 = (byte)(temp >> 56);
            b1 = (byte)(temp >> 48);
            b2 = (byte)(temp >> 40);
            b3 = (byte)(temp >> 32);
            b4 = (byte)(temp >> 24);
            b5 = (byte)(temp >> 16);
            b6 = (byte)(temp >> 8);
            b7 = (byte)(temp);

            if (ShouldReverseFloats())
                BinaryUtility.ReverseEndianness(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref b6, ref b7);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int ToInt32Internal(byte b0, byte b1, byte b2, byte b3)
        {
            return (int)(b0 << 24 | b1 << 16 | b2 << 8 | b3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static long ToInt64Internal(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            int i1 = ToInt32Internal(b0, b1, b2, b3);
            int i2 = ToInt32Internal(b4, b5, b6, b7);

            return (long)i1 << 32 | (uint)i2;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Method consistency")]
        public bool ToBoolean(byte b)
        {
            return b != 0;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Method consistency")]
        public sbyte ToSByte(byte b)
        {
            return (sbyte)b;
        }

        public char ToChar(byte b0, byte b1)
        {
            if (ShouldReverseIntegers())
                BinaryUtility.ReverseEndianness(ref b0, ref b1);

            return (char)(b0 << 8 | b1);
        }

        public short ToInt16(byte b0, byte b1)
        {
            if (ShouldReverseIntegers())
                BinaryUtility.ReverseEndianness(ref b0, ref b1);

            return (short)(b0 << 8 | b1);
        }

        public ushort ToUInt16(byte b0, byte b1)
        {
            if (ShouldReverseIntegers())
                BinaryUtility.ReverseEndianness(ref b0, ref b1);

            return (ushort)(b0 << 8 | b1);
        }

        public int ToInt32(byte b0, byte b1, byte b2, byte b3)
        {
            if (ShouldReverseIntegers())
                BinaryUtility.ReverseEndianness(ref b0, ref b1, ref b2, ref b3);

            return ToInt32Internal(b0, b1, b2, b3);
        }

        public uint ToUInt32(byte b0, byte b1, byte b2, byte b3)
        {
            if (ShouldReverseIntegers())
                BinaryUtility.ReverseEndianness(ref b0, ref b1, ref b2, ref b3);

            return (uint)ToInt32Internal(b0, b1, b2, b3);
        }

        public long ToInt64(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            if (ShouldReverseIntegers())
                BinaryUtility.ReverseEndianness(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref b6, ref b7);

            return ToInt64Internal(b0, b1, b2, b3, b4, b5, b6, b7);
        }

        public ulong ToUInt64(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            if (ShouldReverseIntegers())
                BinaryUtility.ReverseEndianness(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref b6, ref b7);

            return (ulong)ToInt64Internal(b0, b1, b2, b3, b4, b5, b6, b7);
        }

        public unsafe float ToSingle(byte b0, byte b1, byte b2, byte b3)
        {
            if (ShouldReverseFloats())
                BinaryUtility.ReverseEndianness(ref b0, ref b1, ref b2, ref b3);

            var value = ToInt32Internal(b0, b1, b2, b3);
            return *(float*)&value;
        }

        public unsafe double ToDouble(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            if (ShouldReverseFloats())
                BinaryUtility.ReverseEndianness(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref b6, ref b7);

            var value = ToInt64Internal(b0, b1, b2, b3, b4, b5, b6, b7);
            return *(double*)&value;
        }
    }
}
