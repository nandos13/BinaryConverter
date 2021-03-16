using System;
using System.Runtime.InteropServices;

namespace JPAssets.Binary
{
    /// <summary>
    /// A 64-bit binary data structure.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = kByteCount)]
    [System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public readonly struct Binary64 : IEquatable<Binary64>
    {
        /// <summary>
        /// Denotes the size of this data structure in bytes.
        /// </summary>
        private const int kByteCount = 8;

        [FieldOffset(0)]
        private readonly ulong m_data;

        public unsafe Binary64(byte* ptr)
        {
            m_data = BinaryConversionUtility.ToData<uint>(*(ptr + 0), *(ptr + 1), *(ptr + 2), *(ptr + 3), *(ptr + 4), *(ptr + 5), *(ptr + 6), *(ptr + 7));
        }

        public unsafe Binary64(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            var ptr = stackalloc byte[kByteCount] { b0, b1, b2, b3, b4, b5, b6, b7 };
            this = new Binary64(ptr);
        }

        /// <inheritdoc cref="ValidationUtility.CheckArrayOffsetAndCount{T}(T[], string, int, int)"/>
        public unsafe Binary64(byte[] buffer, int offset)
        {
            ValidationUtility.CheckArrayOffsetAndCount(buffer, nameof(buffer), offset, kByteCount);

            fixed (byte* bufferPtr = buffer)
            {
                var offsetPtr = bufferPtr + offset;
                this = new Binary64(offsetPtr);
            }
        }

        public Binary64(Binary32 low, Binary32 high)
        {
            low.ExtractBytes(out byte b0, out byte b1, out byte b2, out byte b3);
            high.ExtractBytes(out byte b4, out byte b5, out byte b6, out byte b7);

            this = new Binary64(b0, b1, b2, b3, b4, b5, b6, b7);
        }

        public Binary64(long value)
        {
            unsafe
            {
                this = new Binary64((byte*)&value);
            }
        }

        public Binary64(ulong value)
        {
            unsafe
            {
                this = new Binary64((byte*)&value);
            }
        }

        public Binary64(double value)
        {
            unsafe
            {
                this = new Binary64((byte*)&value);
            }
        }

        /// <summary>
        /// Creates a new <see cref="Binary64"/> instance with reversed-endianness.
        /// </summary>
        public Binary64 Reverse()
        {
            unsafe
            {
                var reversedData = EndiannessUtility.ReverseEndianness(m_data);
                return new Binary64((byte*)&reversedData);
            }
        }

        /// <inheritdoc cref="BinaryConversionUtility.ExtractBytes{T}(T, out byte, out byte, out byte, out byte, out byte, out byte, out byte, out byte)"/>
        public void ExtractBytes(out byte b0, out byte b1, out byte b2, out byte b3, out byte b4, out byte b5, out byte b6, out byte b7)
        {
            BinaryConversionUtility.ExtractBytes(m_data, out b0, out b1, out b2, out b3, out b4, out b5, out b6, out b7);
        }

        /// <returns>A <see cref="long"/> representation of the binary data.</returns>
        public long ToInt64()
        {
            unsafe
            {
                fixed (ulong* dataPtr = &m_data)
                    return BinaryConversionUtility.ToData<long>((byte*)dataPtr);
            }
        }

        /// <returns>A <see cref="ulong"/> representation of the binary data.</returns>
        public ulong ToUInt64()
        {
            return m_data;
        }

        /// <returns>A <see cref="double"/> representation of the binary data.</returns>
        public double ToDouble()
        {
            unsafe
            {
                fixed (ulong* dataPtr = &m_data)
                    return BinaryConversionUtility.ToData<double>((byte*)dataPtr);
            }
        }

        /// <returns>True if the binary data is equal; Otherwise, false.</returns>
        public bool Equals(Binary64 other)
        {
            return m_data.Equals(other.m_data);
        }

        public override bool Equals(object obj)
        {
            return obj is Binary64 other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return m_data.GetHashCode();
        }

        public static bool operator ==(Binary64 left, Binary64 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Binary64 left, Binary64 right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            unsafe
            {
                fixed (ulong* dataPtr = &m_data)
                    return BinaryStringUtility.ToString(ptr: (byte*)dataPtr, count: kByteCount, delimeter: ' ');
            }
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
