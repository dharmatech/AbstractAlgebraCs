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

namespace pinter_14_F_6_Z6_Z3
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

            // ----------------------------------------------------------------------

            bool Divisible(int a, int b) => a % b == 0;

            IEnumerable<int> Factors(int n) =>
                Enumerable.Range(1, n).Where(elt => Divisible(n, elt));

            bool RelativelyPrime(int a, int b) =>
                Factors(a).Intersect(Factors(b)).Count() == 1;

            bool Prime(int n) =>
                Enumerable.Range(1, n / 2).Skip(1).Any(elt => Divisible(n, elt)) == false;

            // ----------------------------------------------------------------------

            MathSet<int> Kernel(Func<int, int> f_, Group<int> G, Group<int> H) =>
                G.Set.Where(x => f(x) == H.Identity).ToMathSet();

            MathSet<int> Range(Func<int, int> f_, Group<int> G, Group<int> H) =>
                G.Set.Select(f).ToMathSet();

            // ----------------------------------------------------------------------

            WriteLine($"range f: { Range(f,Z6,Z3) }");

            foreach (var elt in Range(f, Z6, Z3))
                WriteLine($"   {elt}   order: { Z3.Order(elt) }   {( Prime(Z3.Order(elt)) ? "prime" : "" )}   ");

            WriteLine();

            WriteLine($"Z6: { Z6.Set }");

            foreach (var elt in Z6.Set)
                WriteLine($"   {elt}   order: { Z6.Order(elt) }   ");

        }
    }
}
