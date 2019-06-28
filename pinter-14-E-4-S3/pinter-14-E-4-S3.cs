using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;
using AbstractAlgebraCartesianProduct;

using static AbstractAlgebraStandardGroupS3.Utils;

using static System.Console;

namespace pinter_14_E_4_S3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
                        
            S3.ShowOperationTableColored();

            WriteLine("subgroups       : {0}", S3.ProperSubgroups());
            WriteLine("normal subgroups: {0}", S3.NormalProperSubgroups());

            {
                var result =
                    new[] { S3.Set, S3.Set }.CartesianProduct().Select(elt =>
                    {
                        var a = elt.ElementAt(0);
                        var b = elt.ElementAt(1);

                        return S3.Op_(a, b, S3.Inverse(a), S3.Inverse(b));
                    })
                    .ToMathSet()
                    .ConvertAll(S3.Lookup);

                WriteLine("commutators: {0}", result);
            }
        }
    }
}
