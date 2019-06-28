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

namespace pinter_14_E_5_S3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
                        
            S3.ShowOperationTableColored();

            WriteLine("subgroups       : {0}", S3.ProperSubgroups());
            WriteLine("normal subgroups: {0}", S3.NormalProperSubgroups());

            foreach (var K in S3.NormalProperSubgroups())
            {
                WriteLine("  K (normal subgroup): {0}", K);

                foreach (var H in S3.ProperSubgroups())
                {
                    WriteLine("    H (subgroup): {0}", H);

                    var result = new[] { H.Set, K.Set }.CartesianProduct().Select(elt =>
                    {
                        var h = elt.ElementAt(0);
                        var k = elt.ElementAt(1);

                        return S3.Op(h, k);
                    })
                    .Select(S3.Lookup)
                    .ToMathSet();

                    WriteLine("      HK = {0}{1} = {2}", H, K, result);
                }
            }

        }
    }
}
