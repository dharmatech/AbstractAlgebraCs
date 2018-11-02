using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraUtil;
using AbstractAlgebraFunctionIntInt;

using AbstractAlgebra;
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
            Cycles.from_string("(137428)").display().to_transpositions().display(); WriteLine();
            Cycles.from_string("(416)(8235)").display().to_transpositions().display(); WriteLine();
            Cycles.from_string("(123)(456)(1574)").display().to_transpositions().display(); WriteLine();

            // new FunctionIntInt("31428765").to_disjoint_cycles().to_transpositions().display();
            new FunctionIntInt("31428765").to_disjoint_cycles_alt().to_transpositions().display();
        }
    }
}
