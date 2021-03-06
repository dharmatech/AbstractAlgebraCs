﻿using System.Collections.Generic;
using System.Linq;

using AbstractAlgebraUtil;
using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraIsomorphism;

using static AbstractAlgebraGenerate.Utils;

using static AbstractAlgebraStandardGroupD4.Utils;

namespace pinter_13_H_7
{
    class Program
    {
        static void Main(string[] args)
        {
            var eqs = new Dictionary<string, string>
            {
                { "e", "" },
                { "aaaa", "e" },    // a^4 = e
                { "bbbb", "e" },    // b^4 = e
                { "aa", "bb" },     // a^2 = b^2
                { "ba", "aaab" }    // ba = a^3 b
            };

            var G = new Group<string>
            {
                Identity = "e",
                Set = new MathSet<string>(new[] { "e", "a", "aa", "aaa", "b", "ab", "aab", "aaab" })
            };

            G.Op = (a, b) => Generate(eqs, a + b).First(elt => G.Set.Contains(elt));

            G.ShowOperationTableColored();

            //G.IsIsomorphic(D4).Display();

            G.IsomorphicImage().Display();
        }
    }
}
