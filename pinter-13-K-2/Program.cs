using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraCartesianProduct;

using static System.Console;


namespace pinter_13_K_2
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

            int weight(string a) => a.Aggregate(0, (n, elt) => elt == '1' ? n + 1 : n);

            {
                IEnumerable<string> BinaryWords(int n) =>
                    Enumerable.Repeat(new[] { "0", "1" }, n).CartesianProduct().Select(seq => String.Concat(seq));

                var cosets = BinaryWords(7).Select(elt => C3.RightCoset(elt)).ToMathSet();

                WriteLine("cosets {0}: ", cosets.Count);

                foreach (var coset in cosets)
                {
                    var leader = coset.OrderBy(elt => weight(elt)).First();

                    WriteLine("    {0}    leader: {1}", coset, leader);

                    foreach (var item in coset.GroupBy(elt => weight(elt)).OrderBy(elt => elt.Key))
                        WriteLine("        weight {0} : {1}", item.Key, item.ToMathSet());
                }

                WriteLine();
            }

            //foreach (var elt in C3.Set)
            //{
            //    Write("C3 + {0}: ", elt);

            //    WriteLine(C3.Set.ConvertAll(item => C3.Op(item, elt)).ConvertAll(item => (item, weight(item))));
            //}

            WriteLine();

            //{
            //    var x = "1100001";

            //    var coset = C3.Set.ConvertAll(item => C3.Op(item, x)); // coset   C3 + x

            //    var coset_leader = coset.Aggregate(coset.ElementAt(0), (a, b) => weight(a) <= weight(b) ? a : b);

            //    var result = C3.Op(coset_leader, x);

            //    WriteLine(result);
            //}

            string decode(string x) => C3.Op(x, C3.RightCoset(x).OrderBy(elt => weight(elt)).First());


            //string decode(string x)
            //{
            //    var coset = C3.RightCoset(x);

            //    var leader = coset.OrderBy(elt => weight(elt)).First(); 

            //    return C3.Op(x, leader);
            //}
            
            //     coset.Minimum(elt => weight(elt)) 


            void ShowDecode(string x) => WriteLine("x: {0}   decoded: {1}", x, decode(x));

            ShowDecode("1100001");
            ShowDecode("0111011");
            ShowDecode("1001011");
        }
    }
}
