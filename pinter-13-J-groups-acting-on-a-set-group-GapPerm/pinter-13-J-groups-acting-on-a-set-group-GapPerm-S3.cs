using System;
using System.Linq;
using System.Text;

using AbstractAlgebraMathSet;

using static AbstractAlgebraStandardGroupS3.Utils;

using static System.Console;

namespace pinter_13_J_groups_acting_on_a_set_group_GapPerm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            foreach (var elt in S3.Set) WriteLine("{0} :   {1}", S3.Lookup(elt), elt); WriteLine();
                        
            S3.ShowOperationTableColored(); WriteLine();

            S3.ShowInverses();

            {
                var A = new[] { 1, 2, 3 }.ToMathSet();

                var subgroups = S3.ProperSubgroups();

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
