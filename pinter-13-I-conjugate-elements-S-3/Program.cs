using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraFunctionIntInt;
using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;

using static System.Console;

namespace pinter_13_I_conjugate_elements_S_3
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                var ε = new FunctionIntInt((1, 1), (2, 2), (3, 3));
                var α = new FunctionIntInt((1, 1), (2, 3), (3, 2));
                var β = new FunctionIntInt((1, 3), (2, 1), (3, 2));
                var γ = new FunctionIntInt((1, 2), (2, 1), (3, 3));
                var σ = new FunctionIntInt((1, 2), (2, 3), (3, 1));
                var κ = new FunctionIntInt((1, 3), (2, 2), (3, 1));

                string lookup(FunctionIntInt f)
                {
                    var items = new[] { (ε, "ε"), (α, "α"), (β, "β"), (γ, "y"), (σ, "σ"), (κ, "k") };

                    return items.First(elt => f == elt.Item1).Item2;
                }

                var S_3 = new Group<FunctionIntInt>
                {
                    Identity = ε,
                    Set = new MathSet<FunctionIntInt>(new[] { ε, α, β, γ, σ, κ }),
                    Op = (a, b) => a.Compose(b),
                    Lookup = lookup
                };

                S_3.ShowOperationTable(); WriteLine();

                foreach (var item in S_3.Set) WriteLine("({0})^-1 -> {1}", S_3.Lookup(item), S_3.Lookup(S_3.Inverse(item))); WriteLine();

                WriteLine("conjugacy classes (partition of group):");

                WriteLine(S_3.Set.Select(a => S_3.ConjugacyClass(a).ConvertAll(S_3.Lookup)).ToMathSet()); WriteLine();

                foreach (var a in S_3.Set)
                    WriteLine("conjugacy class of {0,4}: {1}",
                        S_3.Lookup(a),
                        S_3.ConjugacyClass(a).ConvertAll(S_3.Lookup));

                WriteLine();

                foreach (var a in S_3.Set)
                    WriteLine("centralizer of {0,4}: {1}",
                        S_3.Lookup(a),
                        S_3.Centralizer(a).ConvertAll(S_3.Lookup));

                WriteLine();

                S_3.ShowConjugates();

                S_3.ShowCentralizers();
            }

        }
    }
}
