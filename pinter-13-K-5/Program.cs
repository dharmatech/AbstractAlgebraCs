using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraCartesianProduct;

using static System.Console;

namespace pinter_13_K_5
{
    class Program
    {
        static void Main(string[] args)
        {
            var C3 = new Group<string>
            {
                Identity = "0000000",
                Set = new MathSet<string>
                {
                    "0000000",
                    "0001111",
                    "0010110",
                    "0011001",
                    "0100101",
                    "0101010",
                    "0110011",
                    "0111100",
                    "1000011",
                    "1001100",
                    "1010101",
                    "1011010",
                    "1100110",
                    "1101001",
                    "1110000",
                    "1111111"
                },
                Op = (a, b) => new String(a.Zip(b, (x, y) => x == y ? '0' : '1').ToArray())
            };

            C3.ShowOperationTableColored(); WriteLine();

            int dot_product(IEnumerable<int> a, IEnumerable<int> b) => a.Zip(b, (x, y) => x * y).Sum() % 2;

            var parity_check_matrix = new[]
            {
                new []{0, 1, 1, 1, 1, 0, 0 },
                new []{1, 0, 1, 1, 0, 1, 0 },
                new []{1, 1, 0, 1, 0, 0, 1 }
            };

            int weight(string a) => a.Aggregate(0, (n, elt) => elt == '1' ? n + 1 : n);

            {
                IEnumerable<string> BinaryWords(int n) =>
                    Enumerable.Repeat(new[] { "0", "1" }, n).CartesianProduct().Select(seq => String.Concat(seq));

                var cosets = BinaryWords(7).Select(elt => C3.RightCoset(elt)).ToMathSet();

                WriteLine("cosets {0}: ", cosets.Count); WriteLine();

                IEnumerable<int> Syndrome(string x) => parity_check_matrix.Select(row => dot_product(row, x.Select(item => item - '0')));

                string Leader(MathSet<string> coset) => coset.OrderBy(elt => weight(elt)).First();


                // B7.RightCosets(C3)               
                // Decode(cosets, x)

                // Decode(B7.RightCosets(C3), x)

                // Decode(B7, C3, x)


                string Decode(string x) =>
                    C3.Op(
                        cosets
                            .Select(coset => Leader(coset))
                            .First(leader => Enumerable.SequenceEqual(Syndrome(leader), Syndrome(x))), 
                        x);

                { var x = "1100001"; WriteLine("x: {0}   decoded: {1}", x, Decode(x)); }
                { var x = "1001011"; WriteLine("x: {0}   decoded: {1}", x, Decode(x)); }

            }
        }
    }
}
