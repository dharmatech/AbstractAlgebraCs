using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraFunctionIntInt;
using AbstractAlgebraGapPerm;
using AbstractAlgebraGroup;
using AbstractAlgebraGetPermutations;

namespace AbstractAlgebraSymmetricGroup
{
    public static class Utils
    {                                
        public static Group<GapPerm> SymmetricGroup(int n)
        {
            var set = Enumerable.Range(1, n)
                .GetPermutations(n)
                .Select(elt => new GapPerm(elt.Prepend(0)).Simplify());

            return new Group<GapPerm>()
            {
                Identity = set.ElementAt(0),
                Set = set.ToMathSet(),
                Op = (a,b) => a.Compose(b),
                // Lookup
                OpString = "·"
            };
        }
    }
}
