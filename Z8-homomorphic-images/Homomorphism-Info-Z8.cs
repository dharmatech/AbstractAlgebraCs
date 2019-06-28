using System;
using System.Linq;

using AbstractAlgebraQuotientGroup;
using AbstractAlgebraIsomorphism;
using AbstractAlgebraHomomorphism;

using static System.Console;

using static AbstractAlgebraStandardGroupZ.Utils;

namespace Z8_homomorphic_images
{
    class Program
    {
        static void Main(string[] args)
        {
            var Z8 = Z(8);
                        
            WriteLine("Z8: {0}\n", Z8); Z8.ShowOperationTableColored(); WriteLine();

            foreach (var N in Z8.NormalProperSubgroups())
            {
                WriteLine("normal subgroup:   N = {0}", N);

                var Z8_N = Z8.QuotientGroup(N, "N");

                WriteLine("    quotient group:   Z8/N = {0}", Z8_N);

                WriteLine("    isomorphic image: {0}", Z8_N.IsomorphicImage());
                                                
                WriteLine("        homomorphisms:");

                foreach (var f in Z8.GenerateHomomorphisms(Z8_N))
                    WriteLine("            {0}", String.Join(" ", Z8.Set.Select(elt => (elt, f(elt)))));
                
                WriteLine();

                Z8_N.ShowOperationTableColored();

                WriteLine();
            }

            Z(4).ShowOperationTableColored();
        }
    }
}
