using System;

namespace TheMatrix
{
    internal static class Utilities
    {
        private static string base_string = "012345678ABCDEFGHIJKLMNOPQRabcdefghijklmnopqrstuvwxyz"; // "!\"#$%&'()*+,-./0123456789:;<=>?ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_abcdefghijklmnopqrstuvwxyz{|}~ ;";

        static Utilities()
        {
            Random = new Random();
        }

        public static Random Random
        {
            get;
            private set;
        }

        public static bool Probability(double p)
        {
            return Random.NextDouble() < p;
        }

        public static double GetRandomDouble(double min, double max)
        {
            return min + Random.NextDouble() * (max - min);
        }

        public static int RoundToInt(double x)
        {
            return (int)Math.Round(x);
        }

        public static char GetRandomCharacter()
        {
            return base_string[Random.Next(base_string.Length)];
        }
    }
}
