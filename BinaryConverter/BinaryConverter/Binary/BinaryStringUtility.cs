namespace JPAssets.Binary
{
    /// <summary>
    /// Utility class for creating strings that represent a binary value.
    /// When creating strings for a binary value of more than one byte, an optional delimiter character
    /// can be specified to break up each byte in the resulting string.
    /// <para>
    /// Note: Byte arguments are arranged in low-to-high order.
    /// </para>
    /// </summary>
    public static class BinaryStringUtility
    {
        public const char kDefaultDelimeter = ' ';

        private const char kCharZero = '0';
        private const char kCharOne = '1';

        private static unsafe string ToString(byte* bytes, int count, bool addDelimeter, char delimeter)
        {
            int totalCharCount = count * 8 + (addDelimeter ? count - 1 : 0);

            var array = new char[totalCharCount];

            int charIndex = 0;

            for (int i = 0; i < count; i++)
            {
                if (addDelimeter && i > 0)
                    array[charIndex++] = delimeter;

                byte* b = (bytes + i);

                for (int j = 0; j < 8; j++)
                {
                    var flag = 1 << (7 - j);
                    var bit = ((*b) & flag) != 0;

                    array[charIndex++] = bit ? kCharOne : kCharZero;
                }
            }

            return new string(array);
        }

        /// <summary>
        /// Creates a string representing the given binary value.
        /// </summary>
        /// <param name="b0">The 1st (lowest) byte in the binary value.</param>
        /// <param name="b1">The 2nd byte in the binary value.</param>
        /// <param name="b2">The 3rd byte in the binary value.</param>
        /// <param name="b3">The 4th byte in the binary value.</param>
        /// <param name="b4">The 5th byte in the binary value.</param>
        /// <param name="b5">The 6th byte in the binary value.</param>
        /// <param name="b6">The 7th byte in the binary value.</param>
        /// <param name="b7">The 8th byte in the binary value.</param>
        /// <param name="delimeter">A delimiting character to print between each byte.</param>
        public static unsafe string ToString(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7, char delimeter)
        {
            var bytes = stackalloc byte[] { b7, b6, b5, b4, b3, b2, b1, b0 };
            return ToString(bytes, 8, true, delimeter);
        }

        /// <inheritdoc cref="BinaryStringUtility.ToString(byte, byte, byte, byte, byte, byte, byte, byte, char)"/>
        public static unsafe string ToString(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7)
        {
            var bytes = stackalloc byte[] { b7, b6, b5, b4, b3, b2, b1, b0 };
            return ToString(bytes, 8, false, default);
        }

        /// <inheritdoc cref="BinaryStringUtility.ToString(byte, byte, byte, byte, byte, byte, byte, byte, char)"/>
        public static unsafe string ToString(byte b0, byte b1, byte b2, byte b3, char delimeter)
        {
            var bytes = stackalloc byte[] { b3, b2, b1, b0 };
            return ToString(bytes, 4, true, delimeter);
        }

        /// <inheritdoc cref="BinaryStringUtility.ToString(byte, byte, byte, byte, byte, byte, byte, byte, char)"/>
        public static unsafe string ToString(byte b0, byte b1, byte b2, byte b3)
        {
            var bytes = stackalloc byte[] { b3, b2, b1, b0 };
            return ToString(bytes, 4, false, default);
        }

        /// <inheritdoc cref="BinaryStringUtility.ToString(byte, byte, byte, byte, byte, byte, byte, byte, char)"/>
        public static unsafe string ToString(byte b0, byte b1, char delimeter)
        {
            var bytes = stackalloc byte[] { b1, b0 };
            return ToString(bytes, 2, true, delimeter);
        }

        /// <inheritdoc cref="BinaryStringUtility.ToString(byte, byte, byte, byte, byte, byte, byte, byte, char)"/>
        public static unsafe string ToString(byte b0, byte b1)
        {
            var bytes = stackalloc byte[] { b1, b0 };
            return ToString(bytes, 2, false, default);
        }

        /// <inheritdoc cref="BinaryStringUtility.ToString(byte, byte, byte, byte, byte, byte, byte, byte, char)"/>
        public static unsafe string ToString(byte b)
        {
            var bytes = stackalloc byte[] { b };
            return ToString(bytes, 1, false, default);
        }
    }
}
