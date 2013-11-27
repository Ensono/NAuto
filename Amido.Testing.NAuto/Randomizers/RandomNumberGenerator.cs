using System;

namespace Amido.Testing.NAuto.Randomizers
{
    public static class RandomNumberGenerator
    {
        private static readonly Random Random = new Random();

        public static int GetInteger(int max)
        {
            return Random.Next(max);
        }

        public static int GetInteger(int min, int max)
        {
            return Random.Next(min, max);
        }

        public static double GetDouble(double min, double max)
        {
            return min + (Random.NextDouble() * (max - min));
        }
    }
}
