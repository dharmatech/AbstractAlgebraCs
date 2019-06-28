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

namespace pinter_15_commutators_D4
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

            Write("D4 "); D4.ShowOperationTableColored(); WriteLine();


            foreach (var a in D4.Set)
            {
                foreach (var b in D4.Set)
                {
                    WriteLine("{0}·{1}·{2}·{3} -> {4}",
                        lookup(a),
                        lookup(b),
                        lookup(D4.Inverse(a)),
                        lookup(D4.Inverse(b)),
                        lookup(D4.Op_(a, b, D4.Inverse(a), D4.Inverse(b))));
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

            ShowCommutators(D4); WriteLine();

            var H = D4.Subgroup(new[] { R0, R2 });


            foreach (var elt in D4.CosetGrouping(H, "H"))
                WriteLine("{0}   {1}",
                    String.Join(" ", elt.Select(item => String.Format("{0} =", item.ToString()))),
                    elt.Key.ConvertAll(D4.Lookup)); WriteLine();

            Write("D4/H "); D4.QuotientGroup(H, "H").ShowOperationTableColored();
        }
    }
}
