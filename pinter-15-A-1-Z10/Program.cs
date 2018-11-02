using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraCoset;
using AbstractAlgebraCosetGrouping;
using AbstractAlgebraQuotientGroup;

using static System.Console;

namespace pinter_15_A_1_Z10
{
    class Program
    {
        static void Main(string[] args)
        {
            var Z10 = new Group<int>
            {
                Identity = 0,
                Set = Enumerable.Range(0, 10).ToMathSet(),
                Op = (a, b) => (a + b) % 10,
                OpString = "+"
            };

            Write("Z10 "); Z10.ShowOperationTableColored(); WriteLine();

            var H = Z10.Subgroup(new[] { 0, 5 });

            WriteLine($"H: { H.Set }\n");

            foreach (var elt in Z10.CosetGrouping(H, "H"))
                WriteLine("{0}   {1}", elt.ToMathSet(), elt.Key);

            WriteLine();

            Write("Z10/{0, 5} ");
                        
            Z10.QuotientGroup(H).ShowOperationTableColored();
        }
    }
}
