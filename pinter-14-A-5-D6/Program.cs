using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;

using static System.Console;

namespace pinter_14_A_5_D6
{
    class Program
    {
        static void Main(string[] args)
        {

            var R0  = new GapPerm(0);                 // R0  
            var R1  = new GapPerm("(123456)");        // R60 
            var R2  = new GapPerm("(135)(246)");      // R120
            var R3  = new GapPerm("(14)(25)(36)");    // R180
            var R4  = new GapPerm("(153)(264)");      // R240
            var R5  = new GapPerm("(165432)");        // R300
            var R6  = new GapPerm("(15)(24)");        // FA
            var R7  = new GapPerm("(16)(25)(34)");    // FB  
            var R8  = new GapPerm("(26)(35)");        // FC  
            var R9  = new GapPerm("(12)(36)(45)");    // FD  
            var R10 = new GapPerm("(13)(46)");        // FE  
            var R11 = new GapPerm("(14)(23)(56)");    // FF  

            string lookup(GapPerm f)
            {
                var items = new[] {
                    (R0,  "R0"),
                    (R1,  "R1"),
                    (R2,  "R2"),
                    (R3,  "R3"),
                    (R4,  "R4"),
                    (R5,  "R5"),
                    (R6,  "R6"),
                    (R7,  "R7"),
                    (R8,  "R8"),
                    (R9,  "R9"), 
                    (R10, "R10"), 
                    (R11, "R11") 
                };

                return items.First(elt => f == elt.Item1).Item2;
            }

            var D6 = new Group<GapPerm>
            {
                Identity = R0,
                Set = new MathSet<GapPerm>(new[] { R0, R1, R2, R3, R4, R5, R6, R7, R8, R9, R10, R11 }),
                Op = (a,b) => a.Compose(b),
                Lookup = lookup
            };

            D6.ShowOperationTableColored();

        }
    }
}
