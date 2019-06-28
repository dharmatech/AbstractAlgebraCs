using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using AbstractAlgebraUtil;
using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraIsomorphism;

using static AbstractAlgebraStandardGroupP.Utils;

namespace pinter_09_C_3_P2
{
    class Program
    {
        static void Main(string[] args)
        {
            var H = new Group<Complex>
            {
                Identity = new Complex(1, 0),
                Set = new[] { new Complex(1, 0), new Complex(0, -1), new Complex(-1, 0), new Complex(0, 1) }.ToMathSet(),
                Op = (a, b) => a * b,
                OpString = "*"
            };

            P(2).IsIsomorphic(H).Display();
        }
    }
}
