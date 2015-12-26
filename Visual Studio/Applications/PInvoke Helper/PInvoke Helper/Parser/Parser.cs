using System.Collections.Generic;

namespace PInvokeHelper.Parser
{
    internal class Parser
    {
        public static Statement[] Parse(string input)
        {
            var statements = new List<Statement>();
            var i = 0;

            Helper.SkipWhitespaces(input, ref i);

            for(var statement = Statement.Parse(input, ref i);
                statement != null; 
                statement = Statement.Parse(input, ref i))
            {
                statements.Add(statement);
            }

            Helper.SkipWhitespaces(input, ref i);

            if (i == input.Length)
            {
                return statements.ToArray();
            }
            else
            {
                return null;
            }
        }
    }
}
