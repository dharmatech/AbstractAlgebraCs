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

namespace pinter_16_A_1_Z20_gen5
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

            // IntegersModulo

            var Z20 = Z(20);

            var gen5 = Z20.Subgroup(new[] { 0, 5, 10, 15 });

            WriteLine("<5> : {0}\n", gen5.Set);

            WriteLine("cosets of <5> :\n");

            foreach (var elt in Z20.CosetGrouping(gen5, "<5>"))
                WriteLine("{0}   {1}", elt.ToMathSet(), elt.Key);

            WriteLine();

            Write("Z20/<5> ");
                        
            Z20.QuotientGroup(gen5, coset => coset.Element.ToString("D3"), "<5>").ShowOperationTableColored();

            WriteLine();
            
            var Z5 = Z(5);   Write("Z5 ");   Z5.ShowOperationTableColored();
        }
    }
}
