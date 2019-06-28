using System;
using System.Collections.Generic;
using System.Linq;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraQuotientGroup;
using AbstractAlgebraCosetGrouping;
using AbstractAlgebraCoset;

using AbstractAlgebraIsomorphism;

using static System.Console;

using static AbstractAlgebraStandardGroupZxZ.Utils;

namespace pinter_Z3_Z3xZ3
{
    // IsomorphicCompare

    class Compare : IEqualityComparer<Group<(int, int)>>
    {
        // public bool Equals(Group<(int, int)> A, Group<(int, int)> B) => true;

        // public bool Equals(Group<(int, int)> A, Group<(int, int)> B) => false;

        // public bool Equals(Group<(int, int)> A, Group<(int, int)> B) => IsIsomorphic(A, B);

        public bool Equals(Group<(int, int)> A, Group<(int, int)> B) => A.IsIsomorphic(B);

        public int GetHashCode(Group<(int, int)> A) => 0;
    }


    class IsomorphicCompare<T> : IEqualityComparer<Group<T>>
    {
        // public bool Equals(Group<T> A, Group<T> B) => IsIsomorphic(A, B);
        public bool Equals(Group<T> A, Group<T> B) => A.IsIsomorphic(B);

        public int GetHashCode(Group<T> A) => 0;
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var Z3xZ3 = ZxZ(3, 3);

            WriteLine("Z3xZ3 : {0}\n", Z3xZ3.Set);

            Write("Z3xZ3 "); Z3xZ3.ShowOperationTableColored(); WriteLine();

            WriteLine("Z3xZ3 normal proper subgroups:\n");

            foreach (var N in Z3xZ3.NormalProperSubgroups())
                WriteLine(N);

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
