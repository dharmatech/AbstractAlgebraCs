using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraPowerSet;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;
using AbstractAlgebraCosetGrouping;
using AbstractAlgebraQuotientGroup;

using static System.Console;

namespace pinter_16_A_4_P2_P3
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<T> SymmetricDifference<T>(IEnumerable<T> x, IEnumerable<T> y) =>
                            x.Except(y).Union(y.Except(x));

            var (a, b, c) = ("a", "b", "c");

            var P2 = new Group<MathSet<string>>
            {
                Identity = new MathSet<string> { },
                Set = new[] { a, b }.PowerSet().Select(elt => elt.ToMathSet()).ToMathSet(),
                Op = (x, y) => SymmetricDifference(x, y).ToMathSet()
            };

            Write("P2 "); P2.ShowOperationTableColored(); WriteLine();


            var P3 = new Group<MathSet<string>>
            {
                Identity = new MathSet<string> { },
                Set = new[] { a, b, c }.PowerSet().Select(elt => elt.ToMathSet()).ToMathSet(),
                Op = (x, y) => SymmetricDifference(x, y).ToMathSet()
            };

            Write("P3 "); P3.ShowOperationTableColored(); WriteLine();

            var K = P3.Subgroup(new[] { new MathSet<string>() { }, new[] { c }.ToMathSet() });

            foreach (var elt in P3.CosetGrouping(K, "K"))
                WriteLine("{0,-35}   {1}", elt.ToMathSet(), elt.Key.ConvertAll(P3.Lookup));

            WriteLine();

            Write("P3/K ");

            P3.QuotientGroup(K).ShowOperationTableColored();

        }
    }
}
