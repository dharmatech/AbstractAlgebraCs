using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraUtil;
using AbstractAlgebraFunctionIntInt;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;

using AbstractAlgebraCycles;

using static System.Console;

namespace pinter_08_A_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Cycles.FromString("(137428)").Display().to_transpositions().Display(); WriteLine();
            Cycles.FromString("(416)(8235)").Display().to_transpositions().Display(); WriteLine();
            Cycles.FromString("(123)(456)(1574)").Display().to_transpositions().Display(); WriteLine();

            // new FunctionIntInt("31428765").to_disjoint_cycles().to_transpositions().display();
            new FunctionIntInt("31428765").to_disjoint_cycles_alt().to_transpositions().Display();
        }
    }
}
