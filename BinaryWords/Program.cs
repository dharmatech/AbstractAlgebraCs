using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraCartesianProduct;

namespace BinaryWords
{
    class Program
    {
        static void Main(string[] args)
        {
            //IEnumerable<string> BinaryWords(int n)
            //{
            //    var elts = new[] { "0", "1" };

            //    return Enumerable.Repeat(elts, n).CartesianProduct().Select(seq => String.Concat(seq));
            //}

            IEnumerable<string> BinaryWords(int n) =>
                Enumerable.Repeat(new[] { "0", "1" }, n).CartesianProduct().Select(seq => String.Concat(seq));
            
            {
                var elts = new[] { "0", "1" };

                var result = new[] { elts, elts, elts, elts, elts }.CartesianProduct().Select(seq => string.Concat(seq));
            }

            {
                var result = BinaryWords(5);
            }
        }
    }
}
