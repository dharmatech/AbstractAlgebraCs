using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;

using static System.Console;

namespace pinter_16_C_1
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

            foreach (var n in Enumerable.Range(1,20))
            {
                var Zn = Z(n);

                WriteLine("Z{0,-2} = {1}", n, Zn.Set);
                                
                WriteLine("H   = {0}", Zn.Set.Select(elt => Zn.Op(elt, elt)).OrderBy(elt => elt).ToMathSet());

                WriteLine("K   = {0}", Zn.Set.Where(elt => Zn.Op(elt, elt) == 0).ToMathSet());

                WriteLine();
            }
        }
    }
}
