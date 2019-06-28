using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraUtil;
using AbstractAlgebraGapPerm;
using AbstractAlgebraMathSet;

using static AbstractAlgebraSymmetricGroup.Utils;

using static AbstractAlgebraStandardGroupZ.Utils;

namespace pinter_10_B_1_Z25
{
    class Program
    {
        static void Main(string[] args)
        {
            Z(25).Order(10).Display();
            Z(16).Order(6).Display();

            SymmetricGroup(6).Order(new GapPerm(0, 6, 1, 3, 2, 5, 4)).Display();

            {
                var Z24 = Z(24);

                Z24.Set.Where(elt => Z24.Order(elt) == 2).ToMathSet().Display();
                Z24.Set.Where(elt => Z24.Order(elt) == 3).ToMathSet().Display();
                Z24.Set.Where(elt => Z24.Order(elt) == 4).ToMathSet().Display();
                Z24.Set.Where(elt => Z24.Order(elt) == 6).ToMathSet().Display();
            }
        }
    }
}
