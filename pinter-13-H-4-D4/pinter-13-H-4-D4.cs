using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraGapPerm;
using AbstractAlgebraShowCosets;

using static AbstractAlgebraStandardGroupD4.Utils;

using static System.Console;

namespace pinter_13_H_4_D4
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("D4: {0}", D4);

            D4.ShowOperationTableColored();

            void check(GapPerm a, GapPerm b)
            {
                WriteLine("a    {0}", D4.Lookup(a));
                WriteLine("b    {0}", D4.Lookup(b));
                WriteLine("ba   {0}", D4.Lookup(b.Compose(a)));
                WriteLine("a³b  {0}", D4.Lookup(a.Compose(a.Compose(a.Compose(b)))));
            }

            check(R1, R4); WriteLine();
            check(R1, R5); WriteLine();
            check(R1, R6); WriteLine();
            check(R1, R7); WriteLine();

            WriteLine();

            D4.ShowCosets();
        }
    }
}
