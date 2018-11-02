using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;

using static System.Console;

namespace pinter_14_D_1_S3
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

            S_3.ShowOperationTableColored();

            WriteLine("subgroups       : {0}", S_3.Subgroups());
            WriteLine("normal subgroups: {0}", S_3.NormalSubgroups());
        }
    }
}
