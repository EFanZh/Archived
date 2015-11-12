using System.IO;

namespace PInvokeHelper
{
    internal static class Utility
    {
        public static void SkipWhitespaces(this StreamReader input)
        {
            int ch = input.Peek();

            while (ch > 0 && char.IsWhiteSpace((char)ch))
            {
                ch = input.Read();
            }
        }

        public static bool MatchString(this StreamReader input, string s)
        {
            long savedPosition = input.BaseStream.Position;

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (char c in s)
            {
                if (c != input.Read())
                {
                    input.BaseStream.Position = savedPosition;

                    return false;
                }
            }

            return true;
        }
    }
}
