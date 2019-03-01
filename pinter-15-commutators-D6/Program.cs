using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebra;
using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;
using AbstractAlgebraCosetGrouping;
using AbstractAlgebraQuotientGroup;

using static System.Console;

namespace pinter_15_commutators_D6
{
    class Program
    {
        static void Main(string[] args)
        {
            var R0 = new GapPerm(0);                 // R0  
            var R1 = new GapPerm("(123456)");        // R60 
            var R2 = new GapPerm("(135)(246)");      // R120
            var R3 = new GapPerm("(14)(25)(36)");    // R180
            var R4 = new GapPerm("(153)(264)");      // R240
            var R5 = new GapPerm("(165432)");        // R300
            var R6 = new GapPerm("(15)(24)");        // FA
            var R7 = new GapPerm("(16)(25)(34)");    // FB  
            var R8 = new GapPerm("(26)(35)");        // FC  
            var R9 = new GapPerm("(12)(36)(45)");    // FD  
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
                Op = (a, b) => a.Compose(b),
                Lookup = lookup,
                OpString = "·"
            };

            Write("D6 "); D6.ShowOperationTableColored(); WriteLine();

            foreach (var a in D6.Set)
            {
                foreach (var b in D6.Set)
                {
                    WriteLine("{0,3}·{1,3}·{2,3}·{3,3} -> {4,3}",
                        lookup(a),
                        lookup(b),
                        lookup(D6.Inverse(a)),
                        lookup(D6.Inverse(b)),
                        lookup(D6.Op_(a, b, D6.Inverse(a), D6.Inverse(b))));
                }

                WriteLine();
            } WriteLine();

            void ShowCommutators<T>(Group<T> G)
            {
                WriteLine("commutators:\n");

                var width = G.Set.Select(elt => G.Lookup(elt).Count()).Max();

                var color_thunks = new List<Action>()
                {
                    () => { ForegroundColor = ConsoleColor.White; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.Red; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.Yellow; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.Green; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.Blue; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.Cyan; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.Magenta; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.DarkRed; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.DarkBlue; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.DarkGreen; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.DarkMagenta; BackgroundColor = ConsoleColor.Black; },
                    () => { ForegroundColor = ConsoleColor.DarkYellow; BackgroundColor = ConsoleColor.Black; }
                };

                void pick_color(T elt)
                {
                    var i = G.Set.ToList().IndexOf(elt);

                    color_thunks[i % color_thunks.Count()]();
                }


                Write("{0}|", "".PadLeft(width));

                foreach (var elt in G.Set) { pick_color(elt); Write("{0} ", G.Lookup(elt).PadLeft(width)); }

                WriteLine();


                color_thunks[0](); WriteLine(new String('-', (G.Set.Count() + 1) * (width + 1)));

                foreach (var x in G.Set)
                {
                    pick_color(x); Write("{0}", G.Lookup(x).PadLeft(width)); color_thunks[0](); Write("|");

                    foreach (var y in G.Set)
                    {
                        // var elt = G.Op(x, y);

                        var elt = G.Op_(x, y, G.Inverse(x), G.Inverse(y));

                        pick_color(elt);

                        Write("{0} ", G.Lookup(elt).PadLeft(width));
                    }

                    WriteLine();
                }

                ForegroundColor = ConsoleColor.White;
                BackgroundColor = ConsoleColor.Black;
            }

            Write("D6 "); ShowCommutators(D6); WriteLine();

            var H = D6.Subgroup(new[] { R0, R2, R4 }); // D6 is size 12. H is size 3. 12/3 -> 4 cosets

            foreach (var elt in D6.CosetGrouping(H, "H"))
                WriteLine("{0}   {1}",
                    String.Join(" ", elt.Select(item => String.Format("{0} =", item.ToString()))),
                    elt.Key.ConvertAll(D6.Lookup)); WriteLine();

            Write("D6/H "); D6.QuotientGroup(H, "H").ShowOperationTableColored();
        }
    }
}
