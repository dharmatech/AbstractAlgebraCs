using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraIsomorphism;

using static AbstractAlgebraStandardGroupZ.Utils;
using static AbstractAlgebraStandardGroupZxZ.Utils;

using static System.Console;

namespace pinter_09_C_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var I = "I";
            var V = "V";
            var H = "H";
            var D = "D";

            var G1 = new Group<string>
            {
                Identity = I,
                Set = new[] { I, V, H, D }.ToMathSet(),
                Op = (a, b) =>
                {
                    if (a == I && b == I) return I;
                    if (a == I && b == V) return V;
                    if (a == I && b == H) return H;
                    if (a == I && b == D) return D;

                    if (a == V && b == I) return V;
                    if (a == V && b == V) return I;
                    if (a == V && b == H) return D;
                    if (a == V && b == D) return H;

                    if (a == H && b == I) return H;
                    if (a == H && b == V) return D;
                    if (a == H && b == H) return I;
                    if (a == H && b == D) return V;

                    if (a == D && b == I) return D;
                    if (a == D && b == V) return H;
                    if (a == D && b == H) return V;
                    if (a == D && b == D) return I;

                    throw new Exception();
                },
                OpString = "*"
            };

            var G2 = new Group<Complex>
            {
                Identity = new Complex(1, 0),
                Set = new[] { new Complex(1, 0), new Complex(0, -1), new Complex(-1, 0), new Complex(0, 1) }.ToMathSet(),
                Op = (a,b) => a * b,
                OpString = "*"
            };
            
            WriteLine(G1.IsIsomorphic(G2));   // pinter-09-C-1
            WriteLine(G1.IsIsomorphic(Z(4))); // pinter-09-C-2
        }
    }
}
