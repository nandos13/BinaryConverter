using System;
using System.Runtime.InteropServices;

// TODO: Documentation
namespace JPAssets.Binary
{
    /// <summary>
    /// A 16-bit binary data structure.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = kByteCount)]
    [System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public readonly struct Binary16 : IEquatable<Binary16>
    {
        private const int kByteCount = 2;

        [FieldOffset(0)]
        private readonly ushort m_data;

        public unsafe Binary16(byte* ptr)
        {
            m_data = BinaryUtility.ToData<ushort>(*(ptr + 0), *(ptr + 1));
        }

        public unsafe Binary16(byte b0, byte b1)
        {
            var ptr = stackalloc byte[kByteCount] { b0, b1 };
            this = new Binary16(ptr);
        }

        /// <inheritdoc cref="ValidationUtility.CheckArrayOffsetAndCount{T}(T[], string, int, int)"/>
        public unsafe Binary16(byte[] buffer, int offset)
        {
            ValidationUtility.CheckArrayOffsetAndCount(buffer, nameof(buffer), offset, kByteCount);

            fixed (byte* bufferPtr = buffer)
            {
                var offsetPtr = bufferPtr + offset;
                this = new Binary16(offsetPtr);
            }
        }

        public Binary16(char value)
        {
            unsafe
            {
                this = new Binary16((byte*)&value);
            }
        }

        public Binary16(short value)
        {
            unsafe
            {
                this = new Binary16((byte*)&value);
            }
        }

        public Binary16(ushort value)
        {
            unsafe
            {
                this = new Binary16((byte*)&value);
            }
        }

        /// <summary>
        /// Creates a new <see cref="Binary16"/> instance with reversed-endianness.
        /// </summary>
        public Binary16 Reverse()
        {
            unsafe
            {
                var reversedData = BinaryUtility.ReverseEndianness(m_data);
                return new Binary16((byte*)&reversedData);
            }
        }

        public void ExtractBytes(out byte b0, out byte b1)
        {
            BinaryUtility.ExtractBytes(m_data, out b0, out b1);
        }

        public char AsChar()
        {
            unsafe
            {
                fixed (ushort* dataPtr = &m_data)
                    return *(char*)dataPtr;
            }
        }

        public short AsInt16()
        {
            unsafe
            {
                fixed (ushort* dataPtr = &m_data)
                    return *(short*)dataPtr;
            }
        }

        public ushort AsUInt16()
        {
            return m_data;
        }

        public bool Equals(Binary16 other)
        {
            return m_data.Equals(other.m_data);
        }

        public override bool Equals(object obj)
        {
            return obj is Binary16 other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return m_data.GetHashCode();
        }

        public static bool operator ==(Binary16 left, Binary16 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Binary16 left, Binary16 right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            unsafe
            {
                fixed (ushort* dataPtr = &m_data)
                    return BinaryStringUtility.ToString(ptr: (byte*)dataPtr, count: kByteCount, delimeter: ' ');
            }
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
