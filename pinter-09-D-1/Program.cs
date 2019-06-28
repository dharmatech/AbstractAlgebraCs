using System.Numerics;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraIsomorphism;

using static AbstractAlgebraStandardGroupZ.Utils;
using static AbstractAlgebraStandardGroupZxZ.Utils;
using static AbstractAlgebraStandardGroupP.Utils;

using static System.Console;

namespace pinter_09_D_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var V = new Group<Complex>
            {
                Identity = new Complex(1, 0),
                Set = new[] { new Complex(1, 0), new Complex(0, -1), new Complex(-1, 0), new Complex(0, 1) }.ToMathSet(),
                Op = (a, b) => a * b,
                OpString = "*"
            };

            WriteLine("Z4 and V: {0}",  Z(4).IsIsomorphic(V));

            WriteLine("Z2xZ2 and P2: {0}", ZxZ(2, 2).IsIsomorphic(P(2)));
        }
    }
}
