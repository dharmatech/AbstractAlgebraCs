using AbstractAlgebraUtil;
using AbstractAlgebraCycles;
using AbstractAlgebraFunctionIntInt;

using static System.Console;


namespace pinter_08_A_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Compute each of the following products in S9.
            // (Write your answer as a single permutation.)
            //
            // (145)(37)(682)
            // (17)(628)(9354)
            // (71825)(36)(49)
            // (12)(347)
            // (147)(1678)(74132)
            // (6148)(2345)(12493)
                        
            Cycles.FromString("(145)(37)(682)").Display().ToPermutation(9).DisplayAsFunction(); WriteLine();
            Cycles.FromString("(17)(628)(9354)").Display().ToPermutation(9).DisplayAsFunction(); WriteLine();
            Cycles.FromString("(71825)(36)(49)").Display().ToPermutation(9).DisplayAsFunction(); WriteLine();
            Cycles.FromString("(12)(347)").Display().ToPermutation(9).DisplayAsFunction(); WriteLine();
            Cycles.FromString("(147)(1678)(74132)").Display().ToPermutation(9).DisplayAsFunction(); WriteLine();
            Cycles.FromString("(6148)(2345)(12493)").Display().ToPermutation(9).DisplayAsFunction(); WriteLine();
        }
    }
}
