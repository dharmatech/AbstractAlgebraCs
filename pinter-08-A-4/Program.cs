using System.Linq;

using AbstractAlgebraUtil;
using AbstractAlgebraCycles;

using static System.Console;

namespace pinter_08_A_4
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                var α = "(3714)".to_cycle();
                var β = "(123)".to_cycle();
                var γ = "(24135)".to_cycle();

                void cycles_to_disjoint_cycles(params Cycle[] cycles)
                {
                    // new Cycles(cycles).display().to_permutation(7).display().to_disjoint_cycles().display();

                    new Cycles(cycles).display().to_permutation(7).display().to_disjoint_cycles_alt().display();

                    WriteLine();
                }

                cycles_to_disjoint_cycles(α.inverse(), β);
                cycles_to_disjoint_cycles(γ.inverse(), α);
                cycles_to_disjoint_cycles(α, α, β);
                cycles_to_disjoint_cycles(β, β, α, γ);
                cycles_to_disjoint_cycles(γ, γ, γ, γ);
                cycles_to_disjoint_cycles(γ, γ, γ, α.inverse());
                cycles_to_disjoint_cycles(β.inverse(), γ);
                cycles_to_disjoint_cycles(α.inverse(), γ, γ, α);
            }

            {
                var a = "(3714)".to_cycle();
                var b = "(123)".to_cycle();

                var result = new Cycles(new[] { a, b }).to_permutation(7).to_disjoint_cycles_alt();

                var result_b = new Cycles(result.Select(elt => new Cycle(elt)));

                var result_c = result_b.SelectMany(elt => elt);

                var result_d = result_b.Concat(new[] { new Cycle(new[] { 4, 5 }) }).ToList();
            }

            {
                var a = "(3714)".to_cycle();
                var b = "(123)".to_cycle();

                var result = new Cycles(new[] { a, b }).to_permutation(7).to_disjoint_cycles_alt();
            }
        }
    }
}
