using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraGapPerm;
using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;

namespace AbstractAlgebraStandardGroupD4
{
    public static class Utils
    {
        //     A   C   B
        //      \  |  /
        //       1---2
        //       |   | - D
        //       4---3

        public static GapPerm R0 = new GapPerm(0);           // R0  
        public static GapPerm R1 = new GapPerm("(1234)");    // R90 
        public static GapPerm R2 = new GapPerm("(13)(24)");  // R180
        public static GapPerm R3 = new GapPerm("(4321)");    // R270
        public static GapPerm R4 = new GapPerm("(24)");      // FA  
        public static GapPerm R5 = new GapPerm("(13)");      // FB  
        public static GapPerm R6 = new GapPerm("(12)(34)");  // FC  
        public static GapPerm R7 = new GapPerm("(14)(23)");  // FD  

        static string lookup(GapPerm f)
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

        public static Group<GapPerm> D4 = new Group<GapPerm>
        {
            Identity = R0,
            Set = new MathSet<GapPerm>(new[] { R0, R1, R2, R3, R4, R5, R6, R7 }),
            Op = (a, b) => a.Compose(b),
            Lookup = lookup,
            OpString = "·"
        };
    }
}
