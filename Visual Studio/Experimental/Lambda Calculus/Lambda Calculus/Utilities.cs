using System.Collections.Generic;
using System.Linq;

namespace LambdaCalculus
{
    internal static class Utilities
    {
        private static string EnumerateNextString(string str)
        {
            string data = "abcdefghijklmnopqrstuvwxyz";
            if (string.IsNullOrEmpty(str))
            {
                return data[0].ToString();
            }
            else
            {
                string left = str.Substring(0, str.Length - 1);
                if (str.Last() == data.Last())
                {
                    return EnumerateNextString(left) + data[0];
                }
                else
                {
                    return left + data[data.IndexOf(str[str.Length - 1]) + 1];
                }
            }
        }

        public static VariableTerm GetFreeVariable(IEnumerable<VariableTerm> free_variables)
        {
            string str = EnumerateNextString(string.Empty);
            while (free_variables.Contains(str))
            {
                str = EnumerateNextString(str);
            }
            return str;
        }
    }
}
