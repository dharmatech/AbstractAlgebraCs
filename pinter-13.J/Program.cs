using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraFunctionIntInt;
using AbstractAlgebraMathSet;

using AbstractAlgebra.Program;

using static System.Console;

using static AbstractAlgebra.Program.Utils;

namespace pinter_13_J
{
    class Program
    {
        static void Main(string[] args)
        {
            // consider: http://reference.wolfram.com/language/tutorial/PermutationGroups.html

            // Pinter 13.J

            // Let A be a set
            //    { 1 2 3 }
            // and let G be any subgroup of S_A
            //    S_3   (symmetric group on 3 elements)
            //       { 1 2 3 }
            //       { 1 3 2 }
            //       { 2 1 3 }
            //       { 2 3 1 }
            //       { 3 1 2 }
            //       { 3 2 1 }

            //   subgroups of S_3:
            //            
            //   { ε }
            //   { ε α }
            //   { ε y }
            //   { ε k }
            //   { ε ß σ }
            //   { ε α ß y σ k }

            //   each of these is a group of permutations of A
            //   we say it is a group acting on the set A.
            //
            //   Assume here that G is a finite group.
            //
            //   If   u ∈ A   the orbit of u (with respect to G) is the set
            //
            //      O(u) = { g(u) : g ∈ G }

            /*
             
            example

            G: { ε ß σ }

            O(1) -> { ε(1) ß(1) σ(1) }
                    {   1    3    2  }

            // orbit(G, 1)
                       
  
             */


            // If   u ∈ A   the stabilizer of u is the set G_u = { g ∈ G : g(u) = u }
            // that is, the set of all the permutations in G which leave u fixed.

            // G = { ε α }   ε = { 1 2 3 }   α = { 1 3 2 }
            // u = 1
            // stabilizer of 1 is the set G_1 = { g ∈ G : g(1) = 1 }
            //    ε(1) = 1
            //    α(1) = 1
            //
            // stabilizer(G, 1)

            // G.stabilizer(1)   G (subgroup) is an instance of Group

            

            var ε = new FunctionIntInt((1, 1), (2, 2), (3, 3));
            var α = new FunctionIntInt((1, 1), (2, 3), (3, 2));
            var β = new FunctionIntInt((1, 3), (2, 1), (3, 2));
            var γ = new FunctionIntInt((1, 2), (2, 1), (3, 3));
            var σ = new FunctionIntInt((1, 2), (2, 3), (3, 1));
            var κ = new FunctionIntInt((1, 3), (2, 2), (3, 1));

            var S_3 = new[] { ε, α, β, γ, σ, κ }.ToMathSet();
            
            var items = new[] { (ε, "ε"), (α, "α"), (β, "β"), (γ, "y"), (σ, "σ"), (κ, "k") };

            string lookup(FunctionIntInt f) => items.First(elt => f == elt.Item1).Item2;

            WriteLine("S_3: {0}", S_3.ConvertAll(lookup)); WriteLine();


            foreach (var elt in S_3) WriteLine("{0} {1}", lookup(elt), elt); WriteLine();
            
            show_operation_table_colored(S_3, (a, b) => a.Compose(b), lookup); WriteLine();

            // ShowGroupCosets(S_3, (a, b) => a.Compose(b), lookup); WriteLine();

            {
                var A = new[] { 1, 2, 3 }.ToMathSet();

                var (subgroups, proper_subgroups) = Subgroups_tuple(S_3, (a, b) => a.Compose(b));

                WriteLine("proper subgroups:");

                foreach (var G in proper_subgroups.Select(elt => elt.ToMathSet()))
                    WriteLine(G.ConvertAll(lookup));

                WriteLine();

                foreach (var G in proper_subgroups.Select(elt => elt.ToMathSet()))
                {
                    WriteLine("G: {0}", G.ConvertAll(lookup));

                    foreach (var u in A) WriteLine("orbit of {0}: {1}", u, G.ConvertAll(g => g.Apply(u)));
                    
                    WriteLine();
                }

                // G.orbit(1)

                foreach (var G in proper_subgroups.Select(elt => elt.ToMathSet()))
                {
                    WriteLine("------------------------------------------------------------");
                    WriteLine("G: {0}\n", G.ConvertAll(lookup));

                    foreach (var g in G)
                    {
                        foreach (var u in A) WriteLine("{0}({1}) = {2}    {1} ~ {2}", lookup(g), u, g.Apply(u));

                        WriteLine();
                    }

                    foreach (var u in A)
                        WriteLine("[{0}] = {1}", u, G.ConvertAll(g => g.Apply(u)));

                    WriteLine();
                }

                foreach (var G in proper_subgroups.Select(elt => elt.ToMathSet()))
                {
                    WriteLine("------------------------------------------------------------");
                    WriteLine("G: {0}", G.ConvertAll(lookup));

                    foreach (var u in A) WriteLine("stabilizer of {0} : {1}", u, G.Where(g => g.Apply(u) == u).ToMathSet().ConvertAll(lookup));

                    // foreach (var u in A) WriteLine("stabilizer of {0} : {1}", u, G.Where(g => g.Apply(u) == u).ToMathSet().ConvertAll(lookup));
                    
                    // G.FindAll(g => g.Apply(u) == u).ConvertAll(lookup)

                    // G.Stabilizer(u).ConvertAll(lookup)


                    WriteLine();
                }

            }

            
        }
    }
}
