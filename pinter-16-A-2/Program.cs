using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebra;
using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraCosetGrouping;
using AbstractAlgebraQuotientGroup;

using static System.Console;

namespace pinter_16_A_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Group<int> Z(int n) =>
                new Group<int>
                {
                    Identity = 0,
                    Set = Enumerable.Range(0, n).ToMathSet(),
                    Op = (a, b) => (a + b) % n,
                    OpString = "+"
                };
                        
            var Z6 = Z(6);

            var gen3 = Z6.Subgroup(new[] { 0, 3 });

            WriteLine("<3> : {0}\n", gen3.Set);

            WriteLine("cosets of <3> :\n");

            foreach (var elt in Z6.CosetGrouping(gen3, "<3>"))
                WriteLine("{0}   {1}", elt.ToMathSet(), elt.Key);

            WriteLine();

            Write("Z6/<3> ");

            Z6.QuotientGroup(gen3, coset => coset.Element.ToString("D3"), "<3>").ShowOperationTableColored();

            WriteLine();

            var Z3 = Z(3); Write("Z3 "); Z3.ShowOperationTableColored();
        }
    }
}
