using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraShowCosets;

using static System.Console;

using static AbstractAlgebraStandardGroupD4.Utils;

namespace pinter_13_A_4_D4
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("D4: {0}", D4); WriteLine();

            D4.ShowOperationTableColored(); WriteLine();
             
            D4.ShowCosets();
        }
    }
}
