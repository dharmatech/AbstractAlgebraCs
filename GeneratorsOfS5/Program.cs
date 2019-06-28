using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraCartesianProduct;

using AbstractAlgebraFunctionIntInt;

using AbstractAlgebraCycles;

using AbstractAlgebraMathSet;

using static System.Console;

namespace GeneratorsOfS5
{
    class Program
    {
        static void Main(string[] args)
        {
            var e = Cycles.FromString("()")     .ToPermutation(5);
            var a = Cycles.FromString("(12)")   .ToPermutation(5);    // Perm(5, "(12)")
            var b = Cycles.FromString("(12345)").ToPermutation(5);    // Perm(5, "(12345)")

            // GenerateGroup (i, set)
         
            var items = new List<FunctionIntInt>() { e };

            var i = 1;
                        
            while (true)
            {
                var added = false;

                foreach (var elt in
                    Enumerable.Repeat(new[] { a, b }, i)
                    .CartesianProduct()
                    .Select(ls => ls.Aggregate((x, y) => x.Compose(y))))
                {
                    if (items.Contains(elt) == false)
                    {
                        items.Add(elt);
                        added = true;
                    }
                }

                if (added == false) break;

                i++;
            }

            {
                WriteLine("i: {0}", i);

                var result = items.Select(elt => elt.to_disjoint_cycles_alt());

                WriteLine("number of items: {0}", result.Count());

                WriteLine("items:\n");
                                
                foreach (var elt in result) Write("{0,10}", elt);

            }
        }
    }
}
