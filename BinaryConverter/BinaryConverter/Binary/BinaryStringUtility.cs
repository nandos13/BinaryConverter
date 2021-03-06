﻿using System;

namespace JPAssets.Binary
{
    /// <summary>
    /// Utility class for creating strings that represent a binary value.
    /// When creating strings for a binary value of more than one byte, an optional delimiter character
    /// can be specified to break up each byte in the resulting string.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "HAA0501:Explicit new array type allocation")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "HAA0502:Explicit new reference type allocation")]
    public static class BinaryStringUtility
    {
        /// <summary>
        /// When specified as the delimeter character, no delimeter will be used between bytes.
        /// </summary>
        public const char kNoDelimeter = '\0';

        private const char kCharZero = '0';
        private const char kCharOne = '1';

        /// <summary>
        /// Creates an array of <see cref="System.Char"/> representing the given binary value.
        /// </summary>
        /// <param name="ptr">A pointer to the first byte of the binary value.</param>
        /// <param name="count">The number of bytes to process.</param>
        /// <param name="delimeter">A delimiting character to print between each byte if overridden.</param>
        private static unsafe char[] ToCharsInternal(byte* ptr, int count, char delimeter)
        {
            bool addDelimeter = delimeter != kNoDelimeter;

            int totalCharCount = count * 8 + (addDelimeter ? count - 1 : 0);

            var array = new char[totalCharCount];

            int charIndex = 0;

            for (int i = 0; i < count; i++)
            {
                if (addDelimeter && i > 0)
                    array[charIndex++] = delimeter;

                byte* b = (ptr + i);

                for (int j = 0; j < 8; j++)
                {
                    var flag = 1 << (7 - j);
                    var bit = ((*b) & flag) != 0;

                    array[charIndex++] = bit ? kCharOne : kCharZero;
                }
            }

            return array;
        }

        /// <inheritdoc cref="ToCharsInternal(byte*, int, char)"/>
        /// <inheritdoc cref="ValidationUtility.CheckCount(int)"/>
        public static unsafe char[] ToChars(byte* ptr, int count, char delimeter = kNoDelimeter)
        {
            ValidationUtility.CheckCount(count);
            return ToCharsInternal(ptr, count, delimeter);
        }

        /// <summary>
        /// Creates a <see cref="System.String"/> representing the given binary value.
        /// </summary>
        /// <inheritdoc cref="ToCharsInternal(byte*, int, char)"/>
        /// <inheritdoc cref="ValidationUtility.CheckCount(int)"/>
        public static unsafe string ToString(byte* ptr, int count, char delimeter = kNoDelimeter)
        {
            ValidationUtility.CheckCount(count);
            return new string(ToCharsInternal(ptr, count, delimeter));
        }

        /// <param name="bytes">An array of bytes to create a string representation for.</param>
        /// <param name="offset">Offset of the start index.</param>
        /// <inheritdoc cref="BinaryStringUtility.ToCharsInternal(byte*, int, char)"/>
        /// /// <inheritdoc cref="ValidationUtility.CheckArrayOffsetAndCount{T}(T[], string, int, int)"/>
        public static unsafe char[] ToChars(byte[] bytes, int offset, int count, char delimeter = kNoDelimeter)
        {
            ValidationUtility.CheckArrayOffsetAndCount(bytes, nameof(bytes), offset, count);

            if (count == 0)
                return Array.Empty<char>();

            fixed (byte* ptr = bytes)
                return ToCharsInternal(ptr + offset, count, delimeter);
        }

        /// <inheritdoc cref="BinaryStringUtility.ToChars(byte[], int, int, char)"/>
        public static unsafe char[] ToChars(byte[] bytes, char delimeter = kNoDelimeter)
        {
            _ = bytes ?? throw new ArgumentNullException(nameof(bytes));

            return ToChars(bytes, 0, bytes.Length, delimeter);
        }

        /// <inheritdoc cref="BinaryStringUtility.ToString(byte*, int, char)"/>
        /// <inheritdoc cref="BinaryStringUtility.ToChars(byte[], int, int, char)"/>
        public static string ToString(byte[] bytes, int offset, int count, char delimeter = kNoDelimeter)
        {
            return new string(ToChars(bytes, offset, count, delimeter));
        }

        /// <inheritdoc cref="BinaryStringUtility.ToString(byte[], int, int, char)"/>
        public static string ToString(byte[] bytes, char delimeter = kNoDelimeter)
        {
            _ = bytes ?? throw new ArgumentNullException(nameof(bytes));

            return new string(ToChars(bytes, 0, bytes.Length, delimeter));
        }

        /// <param name="b0">The 1st (lowest) byte in the binary value.</param>
        /// <param name="b1">The 2nd byte in the binary value.</param>
        /// <param name="b2">The 3rd byte in the binary value.</param>
        /// <param name="b3">The 4th byte in the binary value.</param>
        /// <param name="b4">The 5th byte in the binary value.</param>
        /// <param name="b5">The 6th byte in the binary value.</param>
        /// <param name="b6">The 7th byte in the binary value.</param>
        /// <param name="b7">The 8th byte in the binary value.</param>
        /// <inheritdoc cref="ToString(byte*, int, char)"/>
        public static unsafe string ToString(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7, char delimeter = kNoDelimeter)
        {
            var bytes = stackalloc byte[] { b0, b1, b2, b3, b4, b5, b6, b7 };
            return ToString(bytes, 8, delimeter);
        }

        /// <inheritdoc cref="BinaryStringUtility.ToString(byte, byte, byte, byte, byte, byte, byte, byte, char)"/>
        public static unsafe string ToString(byte b0, byte b1, byte b2, byte b3, char delimeter = kNoDelimeter)
        {
            var bytes = stackalloc byte[] { b0, b1, b2, b3 };
            return ToString(bytes, 4, delimeter);
        }

        /// <inheritdoc cref="BinaryStringUtility.ToString(byte, byte, byte, byte, byte, byte, byte, byte, char)"/>
        public static unsafe string ToString(byte b0, byte b1, char delimeter = kNoDelimeter)
        {
            var bytes = stackalloc byte[] { b0, b1 };
            return ToString(bytes, 2, delimeter);
        }

        /// <inheritdoc cref="BinaryStringUtility.ToString(byte, byte, byte, byte, byte, byte, byte, byte, char)"/>
        public static unsafe string ToString(byte b)
        {
            var bytes = stackalloc byte[] { b };
            return ToString(bytes, 1, kNoDelimeter);
        }
    }
}
