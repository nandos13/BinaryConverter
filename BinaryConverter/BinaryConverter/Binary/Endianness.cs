namespace JPAssets.Binary
{
    /// <summary>
    /// Indicates the order ("endianness") in which data may be stored.
    /// </summary>
    public enum Endianness
    {
        // Denotes little-endianness order (least significant byte comes first)
        Little = 0,

        // Denotes big-endianness order (most significant byte comes first)
        Big = 1
    }
}
