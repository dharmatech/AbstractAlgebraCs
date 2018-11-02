using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;

using static System.Console;

namespace pinter_13_J_groups_acting_on_a_set_group_GapPerm
{
    class Program
    {
        static void Main(string[] args)
        {
            var ε = new GapPerm(0);         // new FunctionIntInt((1, 1), (2, 2), (3, 3));
            var α = new GapPerm("(23)");    // new FunctionIntInt((1, 1), (2, 3), (3, 2));
            var β = new GapPerm("(132)");   // new FunctionIntInt((1, 3), (2, 1), (3, 2));
            var γ = new GapPerm("(12)");    // new FunctionIntInt((1, 2), (2, 1), (3, 3));
            var σ = new GapPerm("(123)");   // new FunctionIntInt((1, 2), (2, 3), (3, 1));
            var κ = new GapPerm("(13)");    // new FunctionIntInt((1, 3), (2, 2), (3, 1));

            string lookup(GapPerm f)
            {
                var items = new[] { (ε, "ε"), (α, "α"), (β, "β"), (γ, "y"), (σ, "σ"), (κ, "k") };

                return items.First(elt => f == elt.Item1).Item2;
            }

            var S_3 = new Group<GapPerm>
            {
                Identity = ε,
                Set = new MathSet<GapPerm>(new[] { ε, α, β, γ, σ, κ }),
                Op = (a, b) => a.Compose(b),
                Lookup = lookup
            };

            foreach (var elt in S_3.Set) WriteLine("{0} :   {1}", lookup(elt), elt); WriteLine();
                        
            S_3.ShowOperationTableColored(); WriteLine();

            S_3.ShowInverses();

            {
                var A = new[] { 1, 2, 3 }.ToMathSet();

                var subgroups = S_3.ProperSubgroups();

                WriteLine("proper subgroups:"); subgroups.ToList().ForEach(WriteLine); WriteLine();

                foreach (var G in subgroups)
                {
                    WriteLine("G: {0}\n", G);

                    foreach (var u in A) WriteLine("  orbit of {0} : {1}", u, G.Set.ConvertAll(g => g.Apply(u)));

                    WriteLine();
                }

                foreach (var G in subgroups)
                {
                    WriteLine("G: {0}\n", G);

                    WriteLine("  equivalence relations:");

                    foreach (var g in G.Set)
                    {
                        foreach (var u in A) WriteLine("    {0}({1}) = {2}   {1} ~ {2}", G.Lookup(g), u, g.Apply(u));

                        WriteLine();
                    }

                    WriteLine("  conjugacy classes:");

                    foreach (var u in A) WriteLine("    [{0}] = {1}", u, G.Set.ConvertAll(g => g.Apply(u)));

                    WriteLine();
                }

                foreach (var G in subgroups)
                {
                    WriteLine("G: {0}", G);

                    foreach (var u in A) WriteLine("  stabilizer of {0} : {1}", u, G.Set.Where(g => g.Apply(u) == u).ToMathSet().ConvertAll(G.Lookup));

                    // G.Subgroup(g => g.Apply(u) == u)   returns a new Group instance

                    // G.Set.FindAll(g => g.Apply(u) == u)   returns a MathSet

                    WriteLine();

                }
            }


        }
    }
}
