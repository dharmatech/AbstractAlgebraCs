using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGapPerm;
using AbstractAlgebraGroup;

using static System.Console;

namespace pinter_13_J_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var ε = new GapPerm(0);
            var α = new GapPerm("(12)(34)(56)");
            var β = new GapPerm("(23)");
            var αβ = α.Compose(β);
            var βα = β.Compose(α);
            var αβα = α.Compose(β.Compose(α));
            var βαβ = β.Compose(α.Compose(β));
            var αβαβ = α.Compose(β.Compose(α.Compose(β)));

            string lookup(GapPerm f)
            {
                var items = new[] {
                    (ε, "ε"), (α, "α"), (β, "β"),
                    (αβ, "αβ"), (βα, "βα"), (αβα, "αβα"), (βαβ, "βαβ"), (αβαβ, "αβαβ")
                };

                return items.First(elt => f == elt.Item1).Item2;
            }

            var G = new Group<GapPerm>
            {
                Identity = ε,
                Set = new MathSet<GapPerm>(new[] { ε, α, β, αβ, βα, αβα, βαβ, αβαβ }),
                Op = (a,b) => a.Compose(b),
                Lookup = lookup
            };

            foreach (var elt in G.Set) WriteLine("{0,-4}: {1}", lookup(elt), elt); WriteLine();

            G.ShowInverses();

            G.ShowOperationTableColored(); WriteLine();

            MathSet<int> orbit(Group<GapPerm> grp, int u) => grp.Set.ConvertAll(elt => elt.Apply(u));
            
            WriteLine("orbit of 1: {0}", orbit(G, 1));
            WriteLine("orbit of 2: {0}", orbit(G, 2));
            WriteLine("orbit of 3: {0}", orbit(G, 3));
            WriteLine("orbit of 5: {0}", orbit(G, 5));

            WriteLine();

            MathSet<GapPerm> stabilizer(Group<GapPerm> grp, int u) => 
                grp.Set.Where(elt => elt.Apply(u) == u).ToMathSet();
            
            WriteLine("stabilizer of 1: {0}", stabilizer(G, 1).Select(lookup).ToMathSet());
            WriteLine("stabilizer of 2: {0}", stabilizer(G, 2).Select(lookup).ToMathSet());
            WriteLine("stabilizer of 4: {0}", stabilizer(G, 4).Select(lookup).ToMathSet());
            WriteLine("stabilizer of 5: {0}", stabilizer(G, 5).Select(lookup).ToMathSet());

        }
    }
}
