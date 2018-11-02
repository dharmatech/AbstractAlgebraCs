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

namespace GeneratorsOfS4
{
    class Program
    {
        static void Main(string[] args)
        {
            var e = new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4));
            var a = new FunctionIntInt((1, 2), (2, 1), (3, 3), (4, 4)); // (1,2)
            var b = new FunctionIntInt((1, 2), (2, 3), (3, 4), (4, 1)); // (1,2,3,4)

            var items = new List<FunctionIntInt>() { e };

            var i = 1;

            // var result = new[] { new[] { a, b }, new[] { a, b } }.CartesianProduct().Select(elt => elt.ToList()).ToList();

            // var result = Enumerable.Repeat(new[] { a, b }, 3).CartesianProduct().Select(elt => elt.ToList()).ToList();

            //var result = Enumerable.Repeat(new[] { a, b }, 0)
            //    .CartesianProduct()
            //    .Select(ls => ls.Aggregate((x,y) => x.Compose(y)))                                
            //    .ToList()
            //    ;
            
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

                foreach (var elt in result) WriteLine(elt);

            }



        }
    }
}
