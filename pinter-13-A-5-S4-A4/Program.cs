using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraSymmetricGroup;
using AbstractAlgebraShowCosets;

using static System.Console;

using static AbstractAlgebraSymmetricGroup.Utils;
using static AbstractAlgebraAlternatingGroup.Utils;

namespace pinter_13_A_5_S4_A4
{
    class Program
    {
        static void Main(string[] args)
        {
            var S4 = SymmetricGroup(4);
            var A4 = AlternatingGroup(4);

            WriteLine("S4: {0}", S4);
            WriteLine("A4: {0}", A4);

            WriteLine();

            S4.ShowCoset(A4);

        }
    }
}
