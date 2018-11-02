using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraFunctionIntInt;
using AbstractAlgebraGapPerm;

namespace AbstractAlgebraSymmetricGroup
{
    public static class Utils
    {
        // https://stackoverflow.com/a/10630026/268581

        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> ls, int n)
        {
            if (n == 1) return ls.Select(elt => new T[] { elt });

            return
                GetPermutations(ls, n - 1)
                .SelectMany(
                    elt => ls.Where(item => elt.Contains(item) == false),
                    (a, b) => a.Concat(new T[] { b }));
        }

        //public static IEnumerable<IEnumerable<(int, int)>> SymmetricGroup(int n) =>
        //    GetPermutations(Enumerable.Range(1, n), n)
        //        .Select(elt => Enumerable.Range(1, n).Zip(elt, (a, b) => (a, b)));

        public static MathSet<FunctionIntInt> SymmetricGroup(int n) => 
            GetPermutations(Enumerable.Range(1, n), n)
                .Select(elt => new FunctionIntInt(Enumerable.Range(1, n).Zip(elt, (a, b) => (a, b))))
                .ToMathSet();

        //public static MathSet<GapPerm> SymmetricGroupGapPerm(int n) =>
        //    GetPermutations(Enumerable.Range(1, n), n)
        //        .Select(elt => new GapPerm(elt.Prepend(0)))
        //        .ToMathSet();

        public static MathSet<GapPerm> SymmetricGroupGapPerm(int n) =>
            GetPermutations(Enumerable.Range(1, n), n)
                .Select(elt => new GapPerm(elt.Prepend(0)).Simplify())
                .ToMathSet();

        //public static MathSet<GapPerm> SymmetricGroupGapPerm(int n) =>
        //    GetPermutations(Enumerable.Range(1, n), n)
        //        .Select(elt => new GapPerm(elt.Prepend(0)))
        //        .ToMathSet();

    }
}
