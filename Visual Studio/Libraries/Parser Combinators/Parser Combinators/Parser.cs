namespace ParserCombinators
{
    public delegate T Parser<out T>(string input, ref int index, out bool success);
}
