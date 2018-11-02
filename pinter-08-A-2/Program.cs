
using AbstractAlgebraUtil;
using AbstractAlgebraFunctionIntInt;
using AbstractAlgebraCycles;

using static System.Console;

namespace pinter_08_A_2
{
    class Program
    {
        static void Main(string[] args)
        {
            void display_permutation_as_disjoint_cycles(string s)
            {
                new FunctionIntInt(s).display();
                new FunctionIntInt(s).to_disjoint_cycles_alt().display(); WriteLine();
            }

            display_permutation_as_disjoint_cycles("492517683");
            display_permutation_as_disjoint_cycles("749238165");
            display_permutation_as_disjoint_cycles("795312486");
            display_permutation_as_disjoint_cycles("987436512");
        }
    }
}
