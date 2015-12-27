using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class ExtendedDeclModifier
    {
        public class Thread : ExtendedDeclModifier
        {
            public static Thread Value
            {
                get;
            } = new Thread();
        }

        public class Naked : ExtendedDeclModifier
        {
            public static Naked Value
            {
                get;
            } = new Naked();
        }

        public class DllImport : ExtendedDeclModifier
        {
            public static DllImport Value
            {
                get;
            } = new DllImport();
        }

        public class DllExport : ExtendedDeclModifier
        {
            public static DllExport Value
            {
                get;
            } = new DllExport();
        }

        public static Parser<ExtendedDeclModifier> Parser
        {
            get;
        } = Combinators.Or<ExtendedDeclModifier>(Parsers.String("thread").Select(ignored => Thread.Value),
                                                 Parsers.String("naked").Select(ignored => Naked.Value),
                                                 Parsers.String("dllimport").Select(ignored => DllImport.Value),
                                                 Parsers.String("dllexport").Select(ignored => DllExport.Value));
    }
}
