using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;
using AbstractAlgebraQuotientGroup;
using AbstractAlgebraCosetGrouping;

using static System.Console;

using static AbstractAlgebraStandardGroupD4.Utils;

namespace pinter_D4_all_quotient_groups
{
    class Program
    {
        static void Main(string[] args)
        {            
            WriteLine("normal subgroups: \n");

            foreach (var elt in D4.NormalSubgroups())
                WriteLine("    {0}", elt);
            
            WriteLine("----------------------------------------------------------------------");
            
            foreach (var H in D4.NormalSubgroups())
            {
                WriteLine("normal subgroup: H = {0}", H);

                WriteLine("  coset grouping:");

                foreach (var elt in D4.CosetGrouping(H, "H"))
                    WriteLine("    each of these: {0}   are equal to:   {1}", elt.ToMathSet(), elt.Key.ConvertAll(D4.Lookup));

                WriteLine("  quotient group: D4/H = {0}\n", D4.QuotientGroup(H));

                D4.QuotientGroup(H).ShowOperationTableColored();

                WriteLine("----------------------------------------------------------------------");

            }

            WriteLine("NORMAL SUBGROUP                    QUOTIENT GROUP");

            foreach (var H in D4.NormalSubgroups())
            {
                
                WriteLine("H = {0,-30} D4/H = {1}", H, D4.QuotientGroup(H));
            }
        }
    }
}
