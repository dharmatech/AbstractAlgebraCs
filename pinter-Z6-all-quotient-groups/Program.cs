using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebra;
using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraQuotientGroup;

using static System.Console;

namespace pinter_Z6_all_quotient_groups
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

            WriteLine("normal subgroups: {0}\n", Z6.NormalSubgroups());
                        
            foreach (var H in Z6.NormalSubgroups())
            {
                WriteLine("normal subgroup: {0}", H);

                WriteLine("  quotient group: {0}\n", Z6.QuotientGroup(H));
            }
        }
    }
}
