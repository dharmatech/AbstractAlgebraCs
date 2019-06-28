using System;
using System.Linq;

using AbstractAlgebraQuotientGroup;
using AbstractAlgebraIsomorphism;
using AbstractAlgebraHomomorphism;

using static AbstractAlgebraStandardGroupZxZ.Utils;

using static System.Console;

using static AbstractAlgebraStandardGroupD4.Utils;

namespace D4_homomorphisms
{
    class Program
    {
        static void Main(string[] args)
        {                                                                     
            WriteLine("D4: {0}\n", D4); D4.ShowOperationTableColored(); WriteLine();

            foreach (var N in D4.NormalProperSubgroups())
                WriteLine("normal subgroup:   N = {0}", N);

            WriteLine();
            
            foreach (var N in D4.NormalProperSubgroups())
            {

                WriteLine("normal subgroup:   N = {0}", N);

                var D4_N = D4.QuotientGroup(N, "N");

                WriteLine("    quotient group:   D4/N = {0}", D4_N); ;

                WriteLine("    isomorphic image: {0}", D4_N.IsomorphicImage());
                                
                WriteLine("        homomorphisms:");
                                
                foreach (var f in D4.GenerateHomomorphisms(D4_N))
                {
                    WriteLine("            {0}", String.Join(" ", D4.Set.Select(elt => (D4.Lookup(elt), f(elt)))));
                }

                // HomomorphismToString(G, f)

                WriteLine();

                D4_N.ShowOperationTableColored();

                WriteLine();
            }

            ZxZ(2, 2).ShowOperationTableColored();
        }
    }
}
