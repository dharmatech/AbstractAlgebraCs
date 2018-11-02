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

namespace pinter_14_F_4_Z6_Z3
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

            MathSet<int> Kernel(Func<int, int> f_, Group<int> G, Group<int> H) =>
                G.Set.Where(x => f(x) == H.Identity).ToMathSet();

            // |Z3| = 3

            // relatively prime with 3:   2 4 5 7 8 10

            WriteLine($"kernel f: {Kernel(f, Z6, Z3)}");

            foreach (var m in Enumerable.Range(1, 10).Where(n => n >= 2).Where(elt => RelativelyPrime(3, elt)))
            {
                foreach (var x in Z6.Set)
                    WriteLine($"x: {x}   OpN(x,{m}): {Z6.OpN(x, m)}   {(Kernel(f,Z6,Z3).Contains(Z6.OpN(x, m)) ? '*' : ' ')}");

                WriteLine();
            }

            


            



            // Integers.Where(n => RelativelyPrime(3,n))

            // FactorsGreaterThanOne(3)
            // FactorsGreaterThanOne(n)
            // 

            bool Divisible(int a, int b) => a % b == 0;

            //IEnumerable<int> Factors(int n) =>
            //    Enumerable.Range(1, n / 2).Where(elt => Divisible(n, elt));

            IEnumerable<int> Factors(int n) =>
                Enumerable.Range(1, n).Where(elt => Divisible(n, elt));
            
            {
                var result = Factors(10);
            }
            // var result = 5 / 2;

            bool RelativelyPrime(int a, int b) =>
                Factors(a).Intersect(Factors(b)).Count() == 1;
            
            // 1    1
            // 2    1 2
            // 3    1 3
            // 4    1 2 4
            // 5    1 5
            // 6    1 2 3 6
            // 7    1 7
            // 8    1 2 4 8
            // 9    1 3 9

            //for (var i = 1; i<=10; i++)
            //{
            //    for (var j = i+1; j<=10; j++)
            //        WriteLine($"{i} {j} -> {RelativelyPrime(i,j)}");

            //    WriteLine();
            //}


        }
    }
}
