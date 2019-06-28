using System.Linq;

using AbstractAlgebraMathSet;

using static System.Console;

using static AbstractAlgebraSymmetricGroup.Utils;

using System.Diagnostics;

namespace pinter_13_J_S4
{
    class Program
    {
        static void Main(string[] args)
        {
            var S4 = SymmetricGroup(4);
                        
            S4.ShowOperationTableColored(); WriteLine();

            S4.ShowInverses();

            {
                var A = new[] { 1, 2, 3, 4 }.ToMathSet();
                
                var stopwatch = new Stopwatch(); stopwatch.Start();

                var subgroups = S4.ProperSubgroups();
                
                var result = subgroups.Count();

                stopwatch.Stop();

                WriteLine("{0} {1}", result, stopwatch.Elapsed);

                // 28 00:13:50.6414363      Lippert PowerSet   (LINQ)
                // 28 00:07:02.3448297      FastPowerSet
                // 28 00:01:02.3453946      Subgroups (power_set_divisible_size)

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
