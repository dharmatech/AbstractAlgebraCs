using System.Linq;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;
using AbstractAlgebraGetPermutations;

namespace AbstractAlgebraAlternatingGroup
{
    public static class Utils
    {
        public static Group<GapPerm> AlternatingGroup(int n)
        {
            var set = Enumerable.Range(1, n).GetPermutations(n)
                .Select(elt => new GapPerm(elt.Prepend(0)).Simplify())
                .Where(f => 
                {
                    var cycles = f.ToDisjointCycles();

                    if (cycles.Count() == 0) return true;

                    return cycles.to_transpositions().Count() % 2 == 0;
                });

            return new Group<GapPerm>()
            {
                Identity = set.ElementAt(0),
                Set = set.ToMathSet(),
                Op = (a, b) => a.Compose(b),
                // Lookup
                OpString = "·"
            };
        }
    }
}
