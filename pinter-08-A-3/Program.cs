using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraCycles;
using AbstractAlgebraGapPerm;

using AbstractAlgebraUtil;

using static System.Console;

namespace pinter_08_A_3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Express seach of the following as a product of transpositions in S8.

            Cycles.FromString("(137428)").Display().to_transpositions().Display(); WriteLine();
            Cycles.FromString("(416)(8235)").Display().to_transpositions().Display(); WriteLine();
            Cycles.FromString("(123)(456)(1574)").Display().to_transpositions().Display(); WriteLine();

            new GapPerm(0, 3, 1, 4, 2, 8, 7, 6, 5).Display().ToDisjointCycles().to_transpositions().Display();
        }
    }
}
