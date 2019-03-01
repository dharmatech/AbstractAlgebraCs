using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebra;
using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;
using AbstractAlgebraQuotientGroup;
using AbstractAlgebraCosetGrouping;
using AbstractAlgebraCoset;

using static System.Console;

using static pinter_Z3_Z3xZ3.Utils;

namespace pinter_Z3_Z3xZ3
{
    public static class Utils
    {
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> ls, int n)
        {
            if (n == 1) return ls.Select(elt => new T[] { elt });

            return
                GetPermutations(ls, n - 1)
                .SelectMany(
                    elt => ls.Where(item => elt.Contains(item) == false),
                    (a, b) => a.Concat(new T[] { b }));
        }

        public static IEnumerable<Func<T1, T2>> GenerateInjectiveFunctions<T1, T2>(Group<T1> A, Group<T2> B)
        {
            return GetPermutations(B.Set, B.Set.Count)
                .Select(seq =>
                {
                    Func<T1, T2> f = a => seq.ElementAt(A.Set.ToList().IndexOf(a));

                    return f;
                });
        }

        public static bool IsIsomorphism<T1, T2>(Group<T1> A, Group<T2> B, Func<T1, T2> f)
        {
            foreach (var x in A.Set)
            {
                foreach (var y in A.Set)
                {
                    // f(A.Op(x,y)) == B.Op(f(x), f(y))

                    if (EqualityComparer<T2>.Default.Equals(f(A.Op(x, y)), B.Op(f(x), f(y))) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsIsomorphic<T1, T2>(Group<T1> A, Group<T2> B) =>
            A.Set.Count == B.Set.Count
            &&
            GenerateInjectiveFunctions(A, B).Any(f => IsIsomorphism(A, B, f));

    }

    // IsomorphicCompare

    class Compare : IEqualityComparer<Group<(int, int)>>
    {
        // public bool Equals(Group<(int, int)> A, Group<(int, int)> B) => true;

        // public bool Equals(Group<(int, int)> A, Group<(int, int)> B) => false;

        public bool Equals(Group<(int, int)> A, Group<(int, int)> B) => IsIsomorphic(A, B);

        public int GetHashCode(Group<(int, int)> A) => 0;
    }


    class IsomorphicCompare<T> : IEqualityComparer<Group<T>>
    {
        public bool Equals(Group<T> A, Group<T> B) => IsIsomorphic(A, B);

        public int GetHashCode(Group<T> A) => 0;
    }


    class Program
    {
        static void Main(string[] args)
        {
            var Z3xZ3 = new Group<(int, int)>
            {
                Identity = (0, 0),
                Set = new[] { (0, 0), (0, 1), (0, 2), (1, 0), (1, 1), (1, 2), (2, 0), (2, 1), (2, 2), }.ToMathSet(),
                Op = (a, b) => ((a.Item1 + b.Item1) % 3, (a.Item2 + b.Item2) % 3),
                OpString = "+"
            };

            WriteLine("Z3xZ3 : {0}\n", Z3xZ3.Set);

            Write("Z3xZ3 "); Z3xZ3.ShowOperationTableColored(); WriteLine();

            WriteLine("Z3xZ3 normal proper subgroups:\n");

            foreach (var N in Z3xZ3.NormalProperSubgroups())
                WriteLine(N.Set);

            WriteLine();

            foreach (var N in Z3xZ3.NormalProperSubgroups())
            {
                WriteLine("----------------------------------------------------------------------");
                WriteLine("normal subgroup   N = {0}", N.Set); WriteLine();

                WriteLine("cosets of N:\n");

                //foreach (var elt in Z3xZ3.CosetGrouping(N, "N"))
                //    WriteLine("{0,-35}   {1}", elt.ToMathSet(), elt.Key.ConvertAll(Z3xZ3.Lookup));

                foreach (var elt in Z3xZ3.CosetGrouping(N, "N"))
                    WriteLine("{0}   {1}",
                        String.Join(" ", elt.Select(item => String.Format("{0} =", item.ToString()))),
                        elt.Key.ConvertAll(Z3xZ3.Lookup));
                
                WriteLine();

                WriteLine("Z3xZ3/N (all cosets of N):   {0}\n", Z3xZ3.QuotientGroup(N, "N").Set);

                Write("Z3xZ3/N ");
                                
                Z3xZ3.QuotientGroup(N, "N").ShowOperationTableColored(); WriteLine();
            }


            // WriteLine(IsIsomorphic(Z3xZ3, Z3xZ3));

            {
                var result = Z3xZ3.NormalProperSubgroups().Distinct(new Compare());

                // for Z3xZ3 there are 4 normal proper subgroups
                // however, they are all isomorphic to each other
                // so there is effectively only one normal proper subgroup
            }

            {
                // as mentioned above, the 4 normal proper subgroups are isomorphic to each other.
                //
                // So if we form the quotient group   Z3xZ3/N
                // for each N in the 4 normal proper subgroups
                // the resulting quotient groups will also all be isomorphic to each other

                var result = Z3xZ3.NormalProperSubgroups().Select(N => Z3xZ3.QuotientGroup(N, "N")).Distinct(new IsomorphicCompare<Coset<(int,int)>>());
            }

            {
                var result = Z3xZ3                                          // The group Z3xZ3
                    .NormalProperSubgroups()                                // Generate the normal subgroups
                    .Select(N => Z3xZ3.QuotientGroup(N, "N"))               // For each normal subgroup N, form the quotient group Z3xZ3/N
                    .Distinct(new IsomorphicCompare<Coset<(int, int)>>());  // Eliminate duplicates up to isomorphism
            }
            

        }
    }
}
