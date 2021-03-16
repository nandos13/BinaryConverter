using System;
using System.Runtime.InteropServices;

namespace JPAssets.Binary
{
    /// <summary>
    /// A 32-bit binary data structure.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = kByteCount)]
    [System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public readonly struct Binary32 : IEquatable<Binary32>
    {
        /// <summary>
        /// Denotes the size of this data structure in bytes.
        /// </summary>
        private const int kByteCount = 4;

        [FieldOffset(0)]
        private readonly uint m_data;

        public unsafe Binary32(byte* ptr)
        {
            m_data = BinaryConversionUtility.ToData<uint>(*(ptr + 0), *(ptr + 1), *(ptr + 2), *(ptr + 3));
        }

        public unsafe Binary32(byte b0, byte b1, byte b2, byte b3)
        {
            var ptr = stackalloc byte[kByteCount] { b0, b1, b2, b3 };
            this = new Binary32(ptr);
        }

        /// <inheritdoc cref="ValidationUtility.CheckArrayOffsetAndCount{T}(T[], string, int, int)"/>
        public unsafe Binary32(byte[] buffer, int offset)
        {
            ValidationUtility.CheckArrayOffsetAndCount(buffer, nameof(buffer), offset, kByteCount);

            fixed (byte* bufferPtr = buffer)
            {
                var offsetPtr = bufferPtr + offset;
                this = new Binary32(offsetPtr);
            }
        }

        public Binary32(Binary16 low, Binary16 high)
        {
            low.ExtractBytes(out byte b0, out byte b1);
            high.ExtractBytes(out byte b2, out byte b3);

            this = new Binary32(b0, b1, b2, b3);
        }

        public Binary32(int value)
        {
            unsafe
            {
                this = new Binary32((byte*)&value);
            }
        }

        public Binary32(uint value)
        {
            unsafe
            {
                this = new Binary32((byte*)&value);
            }
        }

        public Binary32(float value)
        {
            unsafe
            {
                this = new Binary32((byte*)&value);
            }
        }

        /// <summary>
        /// Creates a new <see cref="Binary32"/> instance with reversed-endianness.
        /// </summary>
        public Binary32 Reverse()
        {
            unsafe
            {
                var reversedData = EndiannessUtility.ReverseEndianness(m_data);
                return new Binary32((byte*)&reversedData);
            }
        }

        /// <inheritdoc cref="BinaryConversionUtility.ExtractBytes{T}(T, out byte, out byte, out byte, out byte)"/>
        public void ExtractBytes(out byte b0, out byte b1, out byte b2, out byte b3)
        {
            BinaryConversionUtility.ExtractBytes(m_data, out b0, out b1, out b2, out b3);
        }

        /// <returns>A <see cref="int"/> representation of the binary data.</returns>
        public int ToInt32()
        {
            unsafe
            {
                fixed (uint* dataPtr = &m_data)
                    return BinaryConversionUtility.ToData<int>((byte*)dataPtr);
            }
        }

        /// <returns>A <see cref="uint"/> representation of the binary data.</returns>
        public uint ToUInt32()
        {
            return m_data;
        }

        /// <returns>A <see cref="float"/> representation of the binary data.</returns>
        public float ToSingle()
        {
            unsafe
            {
                fixed (uint* dataPtr = &m_data)
                    return BinaryConversionUtility.ToData<float>((byte*)dataPtr);
            }
        }

        /// <returns>True if the binary data is equal; Otherwise, false.</returns>
        public bool Equals(Binary32 other)
        {
            return m_data.Equals(other.m_data);
        }

        public override bool Equals(object obj)
        {
            return obj is Binary32 other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return m_data.GetHashCode();
        }

        public static bool operator ==(Binary32 left, Binary32 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Binary32 left, Binary32 right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            unsafe
            {
                fixed (uint* dataPtr = &m_data)
                    return BinaryStringUtility.ToString(ptr: (byte*)dataPtr, count: kByteCount, delimeter: ' ');
            }
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
