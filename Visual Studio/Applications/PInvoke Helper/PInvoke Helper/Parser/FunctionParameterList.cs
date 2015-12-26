using System.Collections.Generic;

namespace PInvokeHelper.Parser
{
    internal class FunctionParameterList
    {
        public FunctionParameterList(FunctionParameterDeclarator[] parameters)
        {
            Parameters = parameters;
        }

        public FunctionParameterDeclarator[] Parameters
        {
            get;
        }

        public static FunctionParameterList Parse(string input, ref int i)
        {
            var j = i;

            if (!Helper.ParseString(input, ref j, "("))
            {
                return null;
            }

            Helper.SkipWhitespaces(input, ref j);

            var parameters = new List<FunctionParameterDeclarator>();
            var declarator = FunctionParameterDeclarator.Parse(input, ref j);

            if (declarator != null)
            {
                parameters.Add(declarator);
                
                Helper.SkipWhitespaces(input, ref j);

                while (Helper.ParseString(input, ref j, ","))
                {
                    Helper.SkipWhitespaces(input, ref j);

                    declarator = FunctionParameterDeclarator.Parse(input, ref j);

                    if (declarator == null)
                    {
                        return null;
                    }
                    else
                    {
                        parameters.Add(declarator);
                    }

                    Helper.SkipWhitespaces(input, ref j);
                }
            }

            if (Helper.ParseString(input, ref j, ")"))
            {
                i = j;

                return new FunctionParameterList(parameters.ToArray());
            }
            else
            {
                return null;
            }
        }
    }
}
