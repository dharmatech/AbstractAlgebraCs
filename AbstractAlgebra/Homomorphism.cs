using System;
using System.Collections.Generic;
using System.Linq;

using AbstractAlgebraCartesianProduct;
using AbstractAlgebraGroup;

namespace AbstractAlgebraHomomorphism
{
    public static class Extensions
    {
        static IEnumerable<Func<T1, T2>> GenerateCandidateFunctions<T1, T2>(Group<T1> A, Group<T2> B) =>
            Enumerable.Repeat(B.Set, A.Set.Count)
                .CartesianProduct()
                .Where(elt => elt.Intersect(B.Set).Count() == B.Set.Count)
                .Select(seq =>
                {
                    Func<T1, T2> f = a => seq.ElementAt(A.Set.ToList().IndexOf(a));

                    return f;
                });

        static bool IsHomomorphism<T1, T2>(Group<T1> A, Group<T2> B, Func<T1, T2> f)
        {
            foreach (var x in A.Set)
                foreach (var y in A.Set)
                    // f(A.Op(x,y)) == B.Op(f(x), f(y))
                    if (EqualityComparer<T2>.Default.Equals(f(A.Op(x, y)), B.Op(f(x), f(y))) == false)
                        return false;

            return true;
        }

        // CartetianProduct([] { A.Set, B.Set })
        //     .Any(elt => EqualityComparer<T2>.Default.Equals(f(A.Op(x, y)), B.Op(f(x), f(y))) == false)
        // ? false : true 

        public static IEnumerable<Func<T1, T2>> GenerateHomomorphisms<T1, T2>(this Group<T1> A, Group<T2> B) =>
            GenerateCandidateFunctions(A, B)
                .Where(elt => IsHomomorphism(A, B, elt));
    }
}
