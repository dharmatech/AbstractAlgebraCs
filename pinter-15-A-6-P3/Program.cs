using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraPowerSet;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraCosetGrouping;
using AbstractAlgebraQuotientGroup;

using static System.Console;

namespace pinter_15_A_6_P3
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<T> SymmetricDifference<T>(IEnumerable<T> a, IEnumerable<T> b) =>
                a.Except(b).Union(b.Except(a));

            var P3 = new Group<MathSet<int>>
            {
                Identity = new MathSet<int> { },
                Set = new[] { 1, 2, 3 }.PowerSet().Select(elt => elt.ToMathSet()).ToMathSet(),
                Op = (a, b) => SymmetricDifference(a, b).ToMathSet()
            };

            Write("P3 "); P3.ShowOperationTableColored(); WriteLine();

            var H = P3.Subgroup(new[] { new MathSet<int> { }, new MathSet<int> { 1 } });

            WriteLine("Elements of H: {0}\n", H.Set);

            WriteLine("Elements of quotient group P3/H:\n");

            foreach (var elt in P3.CosetGrouping(H, "H"))
                WriteLine($"{ elt.ToMathSet(),-30 }   { elt.Key }");

            WriteLine();

            Write("P3/{ {} {1} }");

            P3.QuotientGroup(H).ShowOperationTableColored();
        }
    }
}
