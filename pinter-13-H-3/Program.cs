using System.Collections.Generic;
using System.Linq;

using AbstractAlgebraUtil;
using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraIsomorphism;

using static AbstractAlgebraGenerate.Utils;

using static AbstractAlgebraStandardGroupZxZ.Utils;

namespace pinter_13_H_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var eqs = new Dictionary<string, string>
            {
                { "e", "" },
                { "aaaa", "e" },    // a^4 = e
                { "bb", "e" },      // b^2 = e
                { "ba", "ab" }      // ba = ab
            };
                        
            var G = new Group<string>
            {
                Identity = "e",
                Set = new MathSet<string>(new[] { "e", "a", "aa", "aaa", "b", "ab", "aab", "aaab" })
            };

            G.Op = (a, b) => Generate(eqs, a + b).First(elt => G.Set.Contains(elt));

            G.ShowOperationTableColored();

            G.IsIsomorphic(ZxZ(4, 2)).Display();
        }
    }
}
