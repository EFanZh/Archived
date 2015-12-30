using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class StructOrUnion
    {
        public class Struct : StructOrUnion
        {
            public static Struct Value
            {
                get;
            } = new Struct();
        }

        public class Union : StructOrUnion
        {
            public static Union Value
            {
                get;
            } = new Union();
        }

        public static Parser<StructOrUnion> Parser
        {
            get;
        } = Combinators.Or<StructOrUnion>(Parsers.String("struct").Select(ignored => Struct.Value),
                                          Parsers.String("union").Select(ignored => Struct.Value));
    }
}
