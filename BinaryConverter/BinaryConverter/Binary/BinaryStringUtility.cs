namespace JPAssets.Binary
{
    /// <summary>
    /// Utility class for creating strings that represent a binary value.
    /// When creating strings for a binary value of more than one byte, an optional delimiter character
    /// can be specified to break up each byte in the resulting string.
    /// <para>
    /// Note: Byte arguments are arranged in high-to-low order.
    /// </para>
    /// </summary>
    public static class BinaryStringUtility
    {
        /// <summary>
        /// When specified as the delimeter character, no delimeter will be used between bytes.
        /// </summary>
        public const char kNoDelimeter = '\0';

        private const char kCharZero = '0';
        private const char kCharOne = '1';

        /// <summary>
        /// Creates a string representing the given binary value.
        /// </summary>
        /// <param name="bytes">A pointer to the first byte of the binary value.</param>
        /// <param name="count">The number of bytes to process.</param>
        /// <param name="delimeter">A delimiting character to print between each byte if overridden.</param>
        private static unsafe string ToString(byte* bytes, int count, char delimeter = kNoDelimeter)
        {
            bool addDelimeter = delimeter != kNoDelimeter;

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
        /// <param name="delimeter">A delimiting character to print between each byte if overridden.</param>
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
