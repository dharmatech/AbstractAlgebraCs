using System.Linq;

using AbstractAlgebraGetPermutations;
using AbstractAlgebraFunctionIntInt;
using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraCycles;

namespace AbstractAlgebraAlternatingGroupFII
{
    public static class Utils
    {
        public static Group<FunctionIntInt> AlternatingGroupFII(int n)
        {
            var set = Enumerable.Range(1, n).GetPermutations(n)
                .Select(elt => new FunctionIntInt(Enumerable.Range(1, n).Zip(elt, (a, b) => (a, b))))
                .Where(f =>
                {
                    var cycles = f.to_disjoint_cycles_alt();

                    if (cycles.Count() == 0) { return true; }

                    return cycles.to_transpositions().Count() % 2 == 0;
                });

            return new Group<FunctionIntInt>()
            {
                Identity = set.ElementAt(0),
                Set = set.ToMathSet(),
                Op = (a, b) => a.Compose(b),
                Lookup = f => set.ToList().FindIndex(elt => elt.Equals(f)).ToString(),
                OpString = "·"
            };
        }
    }
}
