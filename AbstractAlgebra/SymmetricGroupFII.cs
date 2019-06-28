using System.Linq;

using AbstractAlgebraFunctionIntInt;
using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGetPermutations;

namespace AbstractAlgebraSymmetricGroupFII
{
    public static class Utils
    {
        public static Group<FunctionIntInt> SymmetricGroupFII(int n)
        {
            var set = Enumerable.Range(1, n)
                .GetPermutations(n)
                .Select(elt => new FunctionIntInt(Enumerable.Range(1, n).Zip(elt, (a, b) => (a, b))));

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
