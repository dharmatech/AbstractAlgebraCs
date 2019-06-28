using System.Collections.Generic;
using System.Linq;

namespace AbstractAlgebraGetPermutations
{
    public static class Extensions
    {
        // https://stackoverflow.com/a/10630026/268581

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> ls, int n)
        {
            if (n == 1) return ls.Select(elt => new T[] { elt });

            return
                GetPermutations(ls, n - 1)
                .SelectMany(
                    elt => ls.Where(item => elt.Contains(item) == false),
                    (a, b) => a.Concat(new T[] { b }));
        }
    }
}
