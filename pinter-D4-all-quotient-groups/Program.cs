using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebra;
using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;
using AbstractAlgebraQuotientGroup;
using AbstractAlgebraCosetGrouping;

using static System.Console;

namespace pinter_D4_all_quotient_groups
{
    class Program
    {
        static void Main(string[] args)
        {
            //     A   C   B
            //      \  |  /
            //       1---2
            //       |   | - D
            //       4---3

            var R0 = new GapPerm(0);           // R0  
            var R1 = new GapPerm("(1234)");    // R90 
            var R2 = new GapPerm("(13)(24)");  // R180
            var R3 = new GapPerm("(4321)");    // R270
            var R4 = new GapPerm("(24)");      // FA  
            var R5 = new GapPerm("(13)");      // FB  
            var R6 = new GapPerm("(12)(34)");  // FC  
            var R7 = new GapPerm("(14)(23)");  // FD  

            string lookup(GapPerm f)
            {
                var items = new[] {
                    (R0, "R0"),
                    (R1, "R1"),
                    (R2, "R2"),
                    (R3, "R3"),
                    (R4, "R4"),
                    (R5, "R5"),
                    (R6, "R6"),
                    (R7, "R7")
                };

                return items.First(elt => f == elt.Item1).Item2;
            }

            var D4 = new Group<GapPerm>
            {
                Identity = R0,
                Set = new MathSet<GapPerm>(new[] { R0, R1, R2, R3, R4, R5, R6, R7 }),
                Op = (a, b) => a.Compose(b),
                Lookup = lookup,
                OpString = "·"
            };

            WriteLine("normal subgroups: {0}\n", D4.NormalSubgroups());

            foreach (var H in D4.NormalSubgroups())
            {
                WriteLine("normal subgroup: {0}", H);
                                
                WriteLine("  coset grouping:");

                foreach (var elt in D4.CosetGrouping(H, "H"))
                    WriteLine("    {0}   {1}", elt.ToMathSet(), elt.Key.ConvertAll(lookup));
                                
                WriteLine("  quotient group: {0}\n", D4.QuotientGroup(H));

                D4.QuotientGroup(H).ShowOperationTableColored();

                WriteLine("----------------------------------------------------------------------");

            }

        }
    }
}
