using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraCartesianProduct;

using static System.Console;

namespace pinter_13_K_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string add(string a, string b) => new String(a.Zip(b, (x, y) => x == y ? '0' : '1').ToArray());
            
            var C1 = new Group<string>
            {
                Identity = "00000",
                Set = new MathSet<string>
                {
                    "00000",
                    "00111",
                    "01001",
                    "01110",
                    "10011",
                    "10100",
                    "11010",
                    "11101"
                },
                Op = add
            };

            C1.ShowOperationTableColored(); WriteLine();

            int weight(string a) => a.Aggregate(0, (n, elt) => elt == '1' ? n + 1 : n);

            {
                IEnumerable<string> BinaryWords(int n) =>
                    Enumerable.Repeat(new[] { "0", "1" }, n).CartesianProduct().Select(seq => String.Concat(seq));

                var cosets = BinaryWords(5).Select(elt => C1.RightCoset(elt)).ToMathSet();

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

            string decode(string x)
            {
                var coset = C1.Set.ConvertAll(item => C1.Op(item, x));

                var leader = coset.OrderBy(elt => weight(elt)).First();

                return C1.Op(x, leader);
            }

            void ShowDecode(string x) => WriteLine("x: {0}   decoded: {1}", x, decode(x));

            //string decode(string x)
            //{
            //    return C1.Op(x, C1.Set.ConvertAll(item => C1.Op(item, x)).OrderBy(elt => weight(elt)).First());
            //}

            ShowDecode("11100");
            ShowDecode("01101");
            ShowDecode("11011");
            ShowDecode("00011");
        }
    }
}
