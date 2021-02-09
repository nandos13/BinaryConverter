﻿using System;
using System.Runtime.InteropServices;

namespace JPAssets.Binary
{
    /// <summary>
    /// A 32-bit binary data structure.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public readonly struct Binary32 : IEquatable<Binary32>
    {
        [FieldOffset(0x0)] public readonly byte b0;
        [FieldOffset(0x1)] public readonly byte b1;
        [FieldOffset(0x2)] public readonly byte b2;
        [FieldOffset(0x3)] public readonly byte b3;

        public Binary32(byte b0, byte b1, byte b2, byte b3)
        {
            this.b0 = b0;
            this.b1 = b1;
            this.b2 = b2;
            this.b3 = b3;
        }

        public unsafe Binary32(byte* ptr)
        {
            this.b0 = *(ptr + 0);
            this.b1 = *(ptr + 1);
            this.b2 = *(ptr + 2);
            this.b3 = *(ptr + 3);
        }

        public bool Equals(Binary32 other)
        {
            return b0.Equals(other.b0)
                && b1.Equals(other.b1)
                && b2.Equals(other.b2)
                && b3.Equals(other.b3);
        }

        /// <summary>
        /// Creates a new <see cref="Binary32"/> instance with reversed-endianness.
        /// </summary>
        public Binary32 Reverse()
        {
            return new Binary32(b3, b2, b1, b0);
        }

        public override bool Equals(object obj)
        {
            return obj is Binary32 other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(b0, b1, b2, b3);
        }

        public static bool operator ==(Binary32 left, Binary32 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Binary32 left, Binary32 right)
        {
            return !left.Equals(right);
        }

        public static implicit operator Binary32(Binary16 bin)
        {
            return new Binary32(bin.b0, bin.b1, 0, 0);
        }

        public static explicit operator Binary16(Binary32 bin)
        {
            return new Binary16(bin.b0, bin.b1);
        }

        public override string ToString()
        {
            return BinaryStringUtility.ToString(b0, b1, b2, b3, delimeter: ' ');
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
