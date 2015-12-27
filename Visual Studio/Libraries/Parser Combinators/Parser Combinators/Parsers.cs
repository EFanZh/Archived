namespace ParserCombinators
{
    public static class Parsers
    {
        public static Parser<IgnoredResult> Whitespaces
        {
            get
            {
                return (string input, ref int index, out bool success) =>
                       {
                           var i = index;

                           while (i < input.Length && char.IsWhiteSpace(input[i]))
                           {
                               index++;
                           }

                           success = i > index;

                           index = i;

                           return IgnoredResult.Value;
                       };
            }
        }

        public static Parser<IgnoredResult> OptionalWhitespaces
        {
            get
            {
                return (string input, ref int index, out bool success) =>
                {
                    while (index < input.Length && char.IsWhiteSpace(input[index]))
                    {
                        index++;
                    }

                    success = true;

                    return IgnoredResult.Value;
                };
            }
        }

        public static Parser<IgnoredResult> Character(char c)
        {
            return (string input, ref int index, out bool success) =>
            {
                if (index < input.Length)
                {
                    if (input[index] == c)
                    {
                        index++;

                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                }
                else
                {
                    success = false;
                }

                return IgnoredResult.Value;
            };
        }

        public static Parser<IgnoredResult> String(string pattern)
        {
            return (string input, ref int index, out bool success) =>
            {
                var remainLength = input.Length - index;

                if (remainLength < pattern.Length)
                {
                    success = false;
                }
                else
                {
                    for (var i = 0; i < pattern.Length; i++)
                    {
                        if (input[index + i] != pattern[i])
                        {
                            success = false;

                            return IgnoredResult.Value;
                        }
                    }

                    index += pattern.Length;

                    success = true;
                }

                return IgnoredResult.Value;
            };
        }
    }
}
