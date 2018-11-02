using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraCartesianProduct;

using static System.Console;

namespace pinter_13_K_3_C2
{
    class Program
    {
        static void Main(string[] args)
        {
            var C2 = new Group<string>
            {
                Identity = "000000",
                Set = new MathSet<string>
                {
                    "000000",
                    "001001",
                    "010111",
                    "011110",
                    "100011",
                    "101010",
                    "110100",
                    "111101"
                },
                Op = (a, b) => new String(a.Zip(b, (x, y) => x == y ? '0' : '1').ToArray())
            };

            var parity_check_matrix = new[]
            {
                new [] { 0, 1, 0, 1, 0, 0 },
                new [] { 1, 1, 0, 0, 1, 0 },
                new [] { 1, 1, 1, 0, 0, 1 }
            };

            C2.ShowOperationTableColored(); WriteLine();

            int dot_product(IEnumerable<int> a, IEnumerable<int> b) => a.Zip(b, (x, y) => x * y).Sum() % 2;

            {
                IEnumerable<string> BinaryWords(int n) =>
                    Enumerable.Repeat(new[] { "0", "1" }, n).CartesianProduct().Select(seq => String.Concat(seq));

                var cosets = BinaryWords(7).Select(elt => C2.RightCoset(elt)).ToMathSet();

                WriteLine("cosets {0}: ", cosets.Count); WriteLine();

                foreach (var coset in cosets)
                {
                    WriteLine("    {0}", coset); WriteLine();

                    WriteLine("        parity check matrix   (Hx)"); WriteLine();

                    foreach (var elt in coset)
                    {
                        var val = elt.Select(item => item - '0');

                        WriteLine("            {0} -> {1}", elt, "{" + String.Join(" ", parity_check_matrix.Select(row => dot_product(row, val))) + "}");
                    }

                    WriteLine();
                }
            }
        }
    }
}
