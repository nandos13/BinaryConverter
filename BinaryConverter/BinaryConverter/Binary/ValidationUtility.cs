using System;

namespace JPAssets.Binary
{
    internal static class ValidationUtility
    {
        internal static void CheckCount(int count, int upperBound = int.MaxValue)
        {
            if (count < 0 || count > upperBound) throw new ArgumentOutOfRangeException(nameof(count));
        }

        internal static void CheckOffset(int offset, int upperBound)
        {
            if (offset < 0 || offset > upperBound) throw new ArgumentOutOfRangeException(nameof(offset));
        }

        internal static void CheckArrayOffsetAndCount<T>(T[] array, string arrayParamName, int offset, int count)
        {
            _ = array ?? throw new ArgumentNullException(arrayParamName);

            int length = array.Length;
            ValidationUtility.CheckOffset(offset, length - 1);
            ValidationUtility.CheckCount(count, length - offset);
        }
    }
}
