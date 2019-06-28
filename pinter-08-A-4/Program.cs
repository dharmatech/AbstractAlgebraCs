using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraUtil;
using AbstractAlgebraCycles;
using AbstractAlgebraGapPerm;

using static System.Console;

namespace pinter_08_A_4
{
    class Program
    {
        static void Main(string[] args)
        {
            var α = "(3714)".to_cycle();
            var β = "(123)".to_cycle();
            var γ = "(24135)".to_cycle();

            new Cycles(α.inverse(), β).Display().ToPermutation(7).Display(); WriteLine();
            new Cycles(γ.inverse(), α).Display().ToPermutation(7).Display(); WriteLine();
            new Cycles(α, α, β).Display().ToPermutation(7).Display(); WriteLine();
            new Cycles(β, β, α, γ).Display().ToPermutation(7).Display(); WriteLine();
            new Cycles(γ, γ, γ, γ).Display().ToPermutation(7).Display(); WriteLine();
            new Cycles(γ, γ, γ, α.inverse()).Display().ToPermutation(7).Display(); WriteLine();
            new Cycles(β.inverse(), γ).Display().ToPermutation(7).Display(); WriteLine();
            new Cycles(α.inverse(), γ, γ, α).Display().ToPermutation(7).Display(); WriteLine();
        }
    }
}
