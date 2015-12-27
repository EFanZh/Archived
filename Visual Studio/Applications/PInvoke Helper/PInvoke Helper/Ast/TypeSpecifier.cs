using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class TypeSpecifier : SpecifierQualifier
    {
        public class Void : TypeSpecifier
        {
            public static Void Value
            {
                get;
            } = new Void();
        }

        public class Char : TypeSpecifier
        {
            public static Char Value
            {
                get;
            } = new Char();
        }

        public new static Parser<TypeSpecifier> Parser
        {
            get;
        } = Combinators.Or<TypeSpecifier>(Parsers.String("void").Select(ignored => Void.Value),
                                          Parsers.String("char").Select(ignored => Char.Value),
                                          StructOrUnionSpecifier.Parser,
                                          EnumSpecifier.Parser,
                                          TypeDefName.Parser);
    }
}
