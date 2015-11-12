using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PInvokeHelper.Parser
{
    internal class Parser
    {
        private readonly StreamReader input;
        private Stack<long> savedPositions;

        public Parser(Stream input)
        {
            this.input = new StreamReader(input);
        }

        private void SaveState()
        {
            savedPositions.Push(input.BaseStream.Position);
        }

        private void RestoreState()
        {
            input.BaseStream.Position = savedPositions.Pop();
        }

        private bool ParseTypeDefDeclaration(out TypeDefDeclaration result)
        {
            result = null;

            input.SkipWhitespaces();

            if (!input.MatchString("typedef"))
            {
                return false;
            }

            return true;
        }

        private bool ParseType(out Type result)
        {
        }

        private string ParseIdentifier()
        {
            int ch = input.Peek();

            if (ch == -1)
            {
                return null;
            }

            if (!char.IsLetter((char)ch) && ch != '_')
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();

            sb.Append(ch);

            ch = input.Peek();

            while (char.IsLetterOrDigit((char)ch) || ch == '_')
            {
                sb.Append(ch);
            }

            return sb.ToString();
        }

        private bool ParseToPointer(PointerType resultType, out Symbol result)
        {
        }

        private bool ParseExpression(Type resultType, out Symbol result)
        {
            switch (input.Read())
            {
                case '*':
                    ParseToPointer(new PointerType(resultType), out result);
                    break;
            }
        }

        private bool ParseExpressions(Type resultType, out Symbol[] result)
        {
            SaveState();

            var symbols = new List<Symbol>();
            Symbol symbol;

            while (ParseExpression(resultType, out symbol))
            {
                symbols.Add(symbol);

                input.SkipWhitespaces();

                int ch = input.Read();

                if (ch == ',')
                {
                    input.SkipWhitespaces();
                }
                else if (input.Peek() == ';')
                {
                    break;
                }
                else
                {
                    RestoreState();

                    result = null;

                    return false;
                }
            }

            result = symbols.ToArray();

            return true;
        }

        private bool ParseSymbolDeclaration(out SymbolDeclaration result)
        {
            result = null;

            Type resultType;

            if (!ParseType(out resultType))
            {
                return false;
            }

            input.SkipWhitespaces();

            Symbol[] definedSymbols;

            if (!ParseExpressions(resultType, out definedSymbols))
            {
                return false;
            }

            result = new SymbolDeclaration(resultType, definedSymbols);

            return true;
        }

        private bool ParseDeclaration(out SymbolDeclaration result)
        {
            if (input.MatchString("typedef"))
            {
                input.SkipWhitespaces();

                ParseSymbolDeclaration(out result);
            }
            else
            {
                ParseSymbolDeclaration(out result);
            }

            return true;
        }

        private static SymbolDeclaration[] Parse()
        {
            return null;
        }
    }
}
