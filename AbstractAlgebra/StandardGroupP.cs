using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraPowerSet;

namespace AbstractAlgebraStandardGroupP
{
    public static class Utils
    {
        public static Group<MathSet<T>> P<T>(MathSet<T> D)
        {
            IEnumerable<T> SymmetricDifference<T>(IEnumerable<T> a, IEnumerable<T> b) =>
                a.Except(b).Union(b.Except(a));

            return new Group<MathSet<T>>
            {
                Identity = new MathSet<T> { },
                Set = D.PowerSet().Select(elt => elt.ToMathSet()).ToMathSet(),
                Op = (a,b) => SymmetricDifference(a,b).ToMathSet(),
                OpString = "+"
            };
        }

        public static Group<MathSet<int>> P(int n) => P(Enumerable.Range(0, n).ToMathSet());
    }
}
