using System;
using System.Runtime.InteropServices;

namespace JPAssets.Binary
{
    /// <summary>
    /// A 16-bit binary data structure.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public readonly struct Binary16 : IEquatable<Binary16>
    {
        [FieldOffset(0x0)] public readonly byte b0;
        [FieldOffset(0x1)] public readonly byte b1;

        public Binary16(byte b0, byte b1)
        {
            this.b0 = b0;
            this.b1 = b1;
        }

        public unsafe Binary16(byte* ptr)
        {
            this.b0 = *(ptr + 0);
            this.b1 = *(ptr + 1);
        }

        /// <summary>
        /// Creates a new <see cref="Binary16"/> instance with reversed-endianness.
        /// </summary>
        public Binary16 Reverse()
        {
            return new Binary16(b1, b0);
        }

        public bool Equals(Binary16 other)
        {
            return b0.Equals(other.b0)
                && b1.Equals(other.b1);
        }

        public override bool Equals(object obj)
        {
            return obj is Binary16 other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(b0, b1);
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
            return BinaryStringUtility.ToString(b0, b1, delimeter: ' ');
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
