using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;
using AbstractAlgebraPowerSet;

using static System.Console;

namespace pinter_14_A_6
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<T> SymmetricDifference<T>(IEnumerable<T> a, IEnumerable<T> b) =>
                a.Except(b).Union(b.Except(a));
            
            var A = new[] { 1, 2, 3 };

            var PA = new Group<MathSet<int>>
            {
                Identity = new MathSet<int> { },
                Set = A.PowerSet().Select(elt => elt.ToMathSet()).ToMathSet(),
                Op = (a,b) => SymmetricDifference(a,b).ToMathSet()
            };

            PA.ShowOperationTableColored();
        }
    }
}
