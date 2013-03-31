using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Automata
{
    class Program
    {
        static void Main(string[] args)
        {
            var nfa = new NFA<string, string>()
            {
                States = new HashSet<string>() { "1", "2", "3", "4", "5", "6", "X", "Y" },
                InputSymbols = new HashSet<string>() { "a", "b" },
                TransitionRelation = new Dictionary<KeyValuePair<string, string>, HashSet<string>>()
                {
                    { new KeyValuePair<string, string>("X", "e"), new HashSet<string>{ "5" } },
                    { new KeyValuePair<string, string>("5", "a"), new HashSet<string>{ "5" } },
                    { new KeyValuePair<string, string>("5", "b"), new HashSet<string>{ "5" } },
                    { new KeyValuePair<string, string>("5", "e"), new HashSet<string>{ "1" } },
                    { new KeyValuePair<string, string>("1", "a"), new HashSet<string>{ "3" } },
                    { new KeyValuePair<string, string>("1", "b"), new HashSet<string>{ "4" } },
                    { new KeyValuePair<string, string>("3", "a"), new HashSet<string>{ "2" } },
                    { new KeyValuePair<string, string>("4", "b"), new HashSet<string>{ "2" } },
                    { new KeyValuePair<string, string>("2", "e"), new HashSet<string>{ "6" } },
                    { new KeyValuePair<string, string>("6", "a"), new HashSet<string>{ "6" } },
                    { new KeyValuePair<string, string>("6", "b"), new HashSet<string>{ "6" } },
                    { new KeyValuePair<string, string>("6", "e"), new HashSet<string>{ "Y" } }
                },
                InitialState = "X",
                AcceptingStates = new HashSet<string>() { "Y" },
                EmptySymbol = "e"
            };
            var dfa = nfa.ToDFA(new HashSet<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17" }, "d");
            Console.WriteLine("BP");
        }
    }
}
