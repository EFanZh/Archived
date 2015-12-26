namespace PInvokeHelper.Parser2
{
    internal static class Helper
    {
        public static void SkipWhitespaces(string input, ref int i)
        {
            var j = i;

            while (j < input.Length && char.IsWhiteSpace(input[j]))
            {
                ++j;
            }

            i = j;
        }

        public static bool ParseString(string input, ref int i, string match)
        {
            var j = i;
            var k = 0;

            while (j < input.Length && k < match.Length && input[j] == match[k])
            {
                ++j;
                ++k;
            }

            if (k == match.Length)
            {
                i = j;

                return true;
            }
            else
            {
                return false;
            }
        }

        public static string ParseIdentifier(string input, ref int i)
        {
            var j = i;

            if (j < input.Length && (input[j] == '_' || char.IsLetter(input[j])))
            {
                ++j;

                while (input[j] == '_' || char.IsLetterOrDigit(input[j]))
                {
                    ++j;
                }

                var result = input.Substring(i, j - i);

                i = j;

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
