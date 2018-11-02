using AbstractAlgebraUtil;
using AbstractAlgebraCycles;

using static System.Console;

namespace pinter_08_A_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Cycles.from_string("(145)(37)(682)").display().to_permutation(9).display(); WriteLine();
            Cycles.from_string("(17)(628)(9354)").display().to_permutation(9).display(); WriteLine();
            Cycles.from_string("(71825)(36)(49)").display().to_permutation(9).display(); WriteLine();
            Cycles.from_string("(12)(347)").display().to_permutation(9).display(); WriteLine();
            Cycles.from_string("(147)(1678)(74132)").display().to_permutation(9).display(); WriteLine();
            Cycles.from_string("(6148)(2345)(12493)").display().to_permutation(9).display(); WriteLine();
        }
    }
}
