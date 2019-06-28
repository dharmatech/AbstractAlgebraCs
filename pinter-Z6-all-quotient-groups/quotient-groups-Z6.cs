using System.Linq;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraQuotientGroup;

using static System.Console;

using static AbstractAlgebraStandardGroupZ.Utils;

namespace pinter_Z6_all_quotient_groups
{
    class Program
    {
        static void Main(string[] args)
        {
            var Z6 = Z(6);

            WriteLine("normal subgroups: {0}\n", Z6.NormalSubgroups());
                        
            foreach (var H in Z6.NormalSubgroups())
            {
                WriteLine("normal subgroup: {0}", H);

                WriteLine("  quotient group: {0}\n", Z6.QuotientGroup(H));
            }
        }
    }
}
