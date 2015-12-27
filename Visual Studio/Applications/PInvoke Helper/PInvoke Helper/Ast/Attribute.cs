using ParserCombinators;

namespace PInvokeHelper.Ast
{
    internal class Attribute
    {
        internal class Callback : Attribute
        {
            public static Callback Value
            {
                get;
            } = new Callback();
        }

        internal class WinApi : Attribute
        {
            public static WinApi Value
            {
                get;
            } = new WinApi();
        }

        public static Parser<Attribute> Parser
        {
            get;
        } = Combinators.Or<Attribute>(Parsers.String("CALLBACK").Select(ignored => Callback.Value),
                                      Parsers.String("WINAPI").Select(ignored => WinApi.Value));
    }
}
