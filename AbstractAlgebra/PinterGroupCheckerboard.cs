using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;

namespace AbstractAlgebraPinterGroupCheckerboard
{
    public static class PinterGroupCheckerboard
    {
        // Checkerboard game group from Pinter 3.D

        public static string I = "I";
        public static string V = "V";
        public static string H = "H";
        public static string D = "D";

        public static Group<string> G = new Group<string>
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
    }
}
