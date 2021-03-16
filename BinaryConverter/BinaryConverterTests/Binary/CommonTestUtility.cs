using System;

namespace JPAssets.Binary.Tests
{
    internal static class CommonTestUtility
    {
        internal static Random GetRandomAndLogSeed()
        {
            int seed = Environment.TickCount;
            Console.WriteLine($"Got the following random seed for testing: {seed.ToString()}.");

            return new Random(seed);
        }
    }
}
