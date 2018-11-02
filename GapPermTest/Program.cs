using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Immutable;

using AbstractAlgebraGapPerm;

using static System.Console;

namespace GapPermTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var perm = new GapPerm("(123)");

            {
                var f = new GapPerm("(23)");
                var g = new GapPerm("(56)");

                var h = f.Compose(g);
            }

            {
                var f = new GapPerm("(234)");
                var g = new GapPerm("(345)");

                var h = f.Compose(g);
            }

            {
                var f = new GapPerm("(12)(34)");
            }

            {
                var f = new GapPerm("(123)");

                var result = new[] 
                {
                    f.Apply(0),
                    f.Apply(1),
                    f.Apply(3),
                    f.Apply(4)
                };
            }

            {
                var f = new GapPerm("(12)");
                var g = new GapPerm(0, 2, 1);

                WriteLine(f.Equals(g));

                WriteLine(f == g);

                WriteLine(f.GetHashCode());
                WriteLine(g.GetHashCode());
            }

            {
                var f = new GapPerm("(1234)");

                var result = f.Inverse();
            }

            {
                var f = new GapPerm(10, 20, 30, 40, 4).Simplify();
            }

            {
                var f = new GapPerm();
                var g = new GapPerm(0, 2, 1);

                var result_a = f.Apply(10);

                var result_b = f.Inverse();

                var result_c = f.Compose(g);

                var result_d = g.Compose(f);

                // var result_e = f.
            }

            {
                var f = new GapPerm(3, 1, 2, 0);

                var g = f.Inverse();
            }

            {
                var f = new GapPerm(0, 2, 1, 3);

                var g = f.Inverse();

            }

            {
                var f = new GapPerm("(123)(234)(456)").ToDisjointCycles();
                var g = new GapPerm("(12)(3456)").ToDisjointCycles();

                var h = new GapPerm("(123)(234)(456)");

                
            }
        }
    }
}
