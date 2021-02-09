using System;
using System.Runtime.InteropServices;

namespace JPAssets.Binary
{
    /// <summary>
    /// A 64-bit binary data structure.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public readonly struct Binary64 : IEquatable<Binary64>
    {
        [FieldOffset(0x0)] public readonly byte b0;
        [FieldOffset(0x1)] public readonly byte b1;
        [FieldOffset(0x2)] public readonly byte b2;
        [FieldOffset(0x3)] public readonly byte b3;
        [FieldOffset(0x4)] public readonly byte b4;
        [FieldOffset(0x5)] public readonly byte b5;
        [FieldOffset(0x6)] public readonly byte b6;
        [FieldOffset(0x7)] public readonly byte b7;

        public Binary64(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            this.b0 = b0;
            this.b1 = b1;
            this.b2 = b2;
            this.b3 = b3;
            this.b4 = b4;
            this.b5 = b5;
            this.b6 = b6;
            this.b7 = b7;
        }

        public unsafe Binary64(byte* ptr)
        {
            this.b0 = *(ptr + 0);
            this.b1 = *(ptr + 1);
            this.b2 = *(ptr + 2);
            this.b3 = *(ptr + 3);
            this.b4 = *(ptr + 4);
            this.b5 = *(ptr + 5);
            this.b6 = *(ptr + 6);
            this.b7 = *(ptr + 7);
        }

        /// <summary>
        /// Creates a new <see cref="Binary64"/> instance with reversed-endianness.
        /// </summary>
        public Binary64 Reverse()
        {
            return new Binary64(b7, b6, b5, b4, b3, b2, b1, b0);
        }

        public bool Equals(Binary64 other)
        {
            return b0.Equals(other.b0)
                && b1.Equals(other.b1)
                && b2.Equals(other.b2)
                && b3.Equals(other.b3)
                && b4.Equals(other.b4)
                && b5.Equals(other.b5)
                && b6.Equals(other.b6)
                && b7.Equals(other.b7);
        }

        public override bool Equals(object obj)
        {
            return obj is Binary64 other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(b0, b1, b2, b3, b4, b5, b6, b7);
        }

        public static bool operator ==(Binary64 left, Binary64 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Binary64 left, Binary64 right)
        {
            return !left.Equals(right);
        }

        public static implicit operator Binary64(Binary16 bin)
        {
            return new Binary64(bin.b0, bin.b1, 0, 0, 0, 0, 0, 0);
        }

        public static implicit operator Binary64(Binary32 bin)
        {
            return new Binary64(bin.b0, bin.b1, bin.b2, bin.b3, 0, 0, 0, 0);
        }

        public static explicit operator Binary16(Binary64 bin)
        {
            return new Binary16(bin.b0, bin.b1);
        }

        public static explicit operator Binary32(Binary64 bin)
        {
            return new Binary32(bin.b0, bin.b1, bin.b2, bin.b3);
        }

        public override string ToString()
        {
            return BinaryStringUtility.ToString(b0, b1, b2, b3, b4, b5, b6, b7, delimeter: ' ');
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
