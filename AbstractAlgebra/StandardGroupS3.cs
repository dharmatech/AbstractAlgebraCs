using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraGapPerm;
using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;

namespace AbstractAlgebraStandardGroupS3
{
    public class Utils
    {
        public static GapPerm ε = new GapPerm(0);         // new FunctionIntInt((1, 1), (2, 2), (3, 3));
        public static GapPerm α = new GapPerm("(23)");    // new FunctionIntInt((1, 1), (2, 3), (3, 2));
        public static GapPerm β = new GapPerm("(132)");   // new FunctionIntInt((1, 3), (2, 1), (3, 2));
        public static GapPerm γ = new GapPerm("(12)");    // new FunctionIntInt((1, 2), (2, 1), (3, 3));
        public static GapPerm σ = new GapPerm("(123)");   // new FunctionIntInt((1, 2), (2, 3), (3, 1));
        public static GapPerm κ = new GapPerm("(13)");    // new FunctionIntInt((1, 3), (2, 2), (3, 1));

        static string lookup(GapPerm f)
        {
            var items = new[] { (ε, "ε"), (α, "α"), (β, "β"), (γ, "γ"), (σ, "σ"), (κ, "κ") };

            return items.First(elt => f == elt.Item1).Item2;
        }

        public static Group<GapPerm> S3 = new Group<GapPerm>
        {
            Identity = ε,
            Set = new[] { ε, α, β, γ, σ, κ }.ToMathSet(),
            Op = (a, b) => a.Compose(b),
            Lookup = lookup,
            OpString = "·"
        };
    }
}
