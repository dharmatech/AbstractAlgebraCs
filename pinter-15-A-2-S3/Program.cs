using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;
using AbstractAlgebraCartesianProduct;

using AbstractAlgebraCoset;
using AbstractAlgebraCosetGrouping;
using AbstractAlgebraQuotientGroup;

using static System.Console;

namespace pinter_15_A_2_S3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            var ε = new GapPerm(0);         // new FunctionIntInt((1, 1), (2, 2), (3, 3));
            var α = new GapPerm("(23)");    // new FunctionIntInt((1, 1), (2, 3), (3, 2));
            var β = new GapPerm("(132)");   // new FunctionIntInt((1, 3), (2, 1), (3, 2));
            var γ = new GapPerm("(12)");    // new FunctionIntInt((1, 2), (2, 1), (3, 3));
            var σ = new GapPerm("(123)");   // new FunctionIntInt((1, 2), (2, 3), (3, 1));
            var κ = new GapPerm("(13)");    // new FunctionIntInt((1, 3), (2, 2), (3, 1));

            string lookup(GapPerm f)
            {
                var items = new[] { (ε, "ε"), (α, "α"), (β, "β"), (γ, "γ"), (σ, "σ"), (κ, "κ") };

                return items.First(elt => f == elt.Item1).Item2;
            }

            var S3 = new Group<GapPerm>
            {
                Identity = ε,
                Set = new[] { ε, α, β, γ, σ, κ }.ToMathSet(),
                Op = (a, b) => a.Compose(b),
                Lookup = lookup
            };

            // var S3 = new Group(new[] { ε, α, β, γ, σ, κ }, (a, b) => a.Compose(b), lookup)

            Write("S3 "); S3.ShowOperationTableColored(); WriteLine();
            
            // consider - G.IsSubgroup(elts)

            var H = S3.Subgroup(new[] { ε, β, σ });

            WriteLine($"H: { H.Set.ConvertAll(lookup) }\n");
            
            foreach (var elt in S3.CosetGrouping(H, "H"))
                WriteLine("{0}   {1}", elt.ToMathSet(), elt.Key.ConvertAll(lookup));

            WriteLine();

            Write("Z10/{ ε, β, σ } ");
                        
            S3.QuotientGroup(H, coset => new[] { ε, α, β, γ, σ, κ }.ToList().IndexOf(coset.Element).ToString()).ShowOperationTableColored();
        }
    }
}
