using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;
using AbstractAlgebraCartesianProduct;

using static System.Console;

namespace pinter_14_F_2_Z6_Z3
{
    class Program
    {
        static void Main(string[] args)
        {
            var Z6 = new Group<int>
            {
                Identity = 0,
                Set = Enumerable.Range(0, 6).ToMathSet(),
                Op = (a, b) => (a + b) % 6
            };

            var Z3 = new Group<int>
            {
                Identity = 0,
                Set = Enumerable.Range(0, 3).ToMathSet(),
                Op = (a, b) => (a + b) % 3
            };

            int f(int a)
            {
                if (a == 0) return 0;
                if (a == 1) return 1;
                if (a == 2) return 2;
                if (a == 3) return 0;
                if (a == 4) return 1;
                if (a == 5) return 2;

                throw new Exception();
            }

            WriteLine("order of Z6: 6   order of Z3: 3");

            foreach (var b in Z3.Set.Except(new[] { 0 }))
            {
                WriteLine("b: {0}   order: {1}", b, Z3.Order(b));
            }

        }
    }
}
