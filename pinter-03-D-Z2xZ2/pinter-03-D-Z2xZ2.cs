using System;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraIsomorphism;

using static System.Console;

using static AbstractAlgebraStandardGroupZxZ.Utils;

namespace pinter_03_D_Z2xZ2
{
    class Program
    {
        static void Main(string[] args)
        {
            var I = "I";
            var V = "V";
            var H = "H";
            var D = "D";
            
            var G = new Group<string>
            {
                Identity = I,
                Set = new[] { I, V, H, D }.ToMathSet(),
                Op = (a,b) => 
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

            G.ShowOperationTableColored(); WriteLine();

            var Z2xZ2 = ZxZ(2, 2);

            Z2xZ2.ShowOperationTableColored(); WriteLine();

            WriteLine("G is isomorphic to {0}", G.IsomorphicImage());
        }
    }
}
