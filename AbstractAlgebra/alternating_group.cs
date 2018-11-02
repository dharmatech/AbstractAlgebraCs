using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// using AbstractAlgebra.permutation;
using AbstractAlgebraMathSet;

using AbstractAlgebraFunctionIntInt;

using static AbstractAlgebraSymmetricGroup.Utils;

using AbstractAlgebraCycles;

namespace AbstractAlgebra.alternating_group
{
    public static class Utils
    {
        //public static IEnumerable<IEnumerable<(int, int)>> AlternatingGroup(int n) =>
        //    SymmetricGroup(n).Where(f =>
        //    {
        //        var cycles = f.to_disjoint_cycles();

        //        if (cycles.Count() == 0) { return true; }

        //        return cycles.to_transpositions().Count() % 2 == 0;
        //    });

        public static MathSet<FunctionIntInt> AlternatingGroup(int n) =>
            SymmetricGroup(n).Where(f =>
            {
                var cycles = f.to_disjoint_cycles_alt();

                if (cycles.Count() == 0) { return true; }

                return cycles.to_transpositions().Count() % 2 == 0;
            }).ToMathSet();
    }
}
