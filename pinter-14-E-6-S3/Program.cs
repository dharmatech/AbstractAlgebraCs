using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;
using AbstractAlgebraCartesianProduct;

using static System.Console;

namespace pinter_14_E_6_S3
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

            WriteLine("subgroups       : {0}", S_3.ProperSubgroups());
            WriteLine("normal subgroups: {0}", S_3.NormalProperSubgroups());

            {
                // var H = new[] { ε, β, σ }.ToMathSet();

                var H = new Group<GapPerm>
                {
                    Identity = ε,
                    Set = new MathSet<GapPerm>(new[] { ε, β, σ }),
                    Op = (a, b) => a.Compose(b),
                    Lookup = lookup
                };

                // var result = S_3.Set.Select(a => H.RightCoset(a)).ToMathSet();

                // var result = S_3.Set.Select(a => H.RightCoset(a) == H.LeftCoset(a) ? H.RightCoset(a) : new MathSet<GapPerm>()).ToMathSet();

                var S = S_3.Set
                    .Select(a => H.RightCoset(a) == H.LeftCoset(a) ? H.RightCoset(a) : new MathSet<GapPerm>())
                    .Aggregate((a, b) => a.Union(b).ToMathSet())
                    .ConvertAll(S_3.Lookup);

                WriteLine("H (normal subgroup of S): {0}", H.Set.ConvertAll(S_3.Lookup));
                WriteLine("S (subgroup of G)       : {0}", S);
            }

            {
                foreach (var H in S_3.ProperSubgroups())
                {
                    var S = S_3.Set
                        .Select(a => H.RightCoset(a) == H.LeftCoset(a) ? H.RightCoset(a) : new MathSet<GapPerm>())
                        .Aggregate((a, b) => a.Union(b).ToMathSet())
                        .ConvertAll(S_3.Lookup);

                    WriteLine("H (normal subgroup of S): {0}", H.Set.ConvertAll(S_3.Lookup));
                    WriteLine("S (subgroup of G)       : {0}", S);
                }

                //foreach (var H_ in S_3.ProperSubgroups())
                //{
                //    var H = new Group<GapPerm>
                //    {
                //        Identity = ε,
                //        Set = new MathSet<GapPerm>(new[] { ε, β, σ }),
                //        Op = (a, b) => a.Compose(b),
                //        Lookup = lookup
                //    };
                //}
            }

        }
    }
}
