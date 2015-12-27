using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class StorageClassSpecifier : DeclarationSpecifier
    {
        public class Auto : StorageClassSpecifier
        {
            public static Auto Value
            {
                get;
            } = new Auto();
        }

        public class Register : StorageClassSpecifier
        {
            public static Register Value
            {
                get;
            } = new Register();
        }

        public class Static : StorageClassSpecifier
        {
            public static Static Value
            {
                get;
            } = new Static();
        }

        public class Extern : StorageClassSpecifier
        {
            public static Extern Value
            {
                get;
            } = new Extern();
        }

        public class TypeDef : StorageClassSpecifier
        {
            public static TypeDef Value
            {
                get;
            } = new TypeDef();
        }

        public new static Parser<StorageClassSpecifier> Parser
        {
            get;
        } = Combinators.Or<StorageClassSpecifier>(Parsers.String("auto").Select(ignored => Auto.Value),
                                                  Parsers.String("register").Select(ignored => Register.Value),
                                                  Parsers.String("static").Select(ignored => Static.Value),
                                                  Parsers.String("extern").Select(ignored => Extern.Value),
                                                  Parsers.String("typedef").Select(typedef => TypeDef.Value),
                                                  DeclSpecStorageClassSpecifier.Parser);
    }
}
