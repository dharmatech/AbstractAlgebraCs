using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;
using AbstractAlgebraCartesianProduct;

using AbstractAlgebraCoset;
using AbstractAlgebraQuotientGroup;

namespace AbstractAlgebraCosetGrouping
{
    public static class Extensions
    {
        public static IEnumerable<IGrouping<MathSet<T>, Coset<T>>> CosetGrouping<T>(this Group<T> G, Group<T> H, string Name) =>
            G.Set
                .Select(elt => new Coset<T> { Group = H, Element = elt, Name = Name })
                .GroupBy(elt => elt.ToRightCoset());
    }
}
