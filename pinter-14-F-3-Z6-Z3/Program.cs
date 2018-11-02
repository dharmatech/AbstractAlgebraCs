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

namespace pinter_14_F_3_Z6_Z3
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

            // var kernel = Z6.Set.Where(x => f(x) == Z3.Identity);

            // Kernel(f,G,H)

            MathSet<int> Kernel(Func<int,int> f_, Group<int> G, Group<int> H) =>
                G.Set.Where(x => f(x) == H.Identity).ToMathSet();
                        
            WriteLine("kernel of f: {0}", Kernel(f, Z6, Z3));

            var n = Z3.Set.Count;

            foreach (var x in Z6.Set)
                WriteLine("OpN({0},{1}) -> {2}", x, n, Z6.OpN(x, n));
        }
    }
}
