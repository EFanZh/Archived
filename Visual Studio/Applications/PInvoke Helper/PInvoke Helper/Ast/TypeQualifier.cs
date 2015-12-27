using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class TypeQualifier : SpecifierQualifier
    {
        public class Const : TypeQualifier
        {
            public static Const Value
            {
                get;
            } = new Const();
        }

        public class Volatile : TypeQualifier
        {
            public static Volatile Value
            {
                get;
            } = new Volatile();
        }

        public new static Parser<TypeQualifier> Parser
        {
            get;
        } = Combinators.Or<TypeQualifier>(Parsers.String("const").Select(ignored => Const.Value),
                                          Parsers.String("volatile").Select(ignored => Volatile.Value));
    }
}
