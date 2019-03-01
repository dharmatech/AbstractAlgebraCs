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

namespace pinter_15_commutators_S3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var ε = new GapPerm(0);         // new FunctionIntInt((1, 1), (2, 2), (3, 3));
            var α = new GapPerm("(23)");    // new FunctionIntInt((1, 1), (2, 3), (3, 2));
            var β = new GapPerm("(132)");   // new FunctionIntInt((1, 3), (2, 1), (3, 2));
            var γ = new GapPerm("(12)");    // new FunctionIntInt((1, 2), (2, 1), (3, 3));
            var σ = new GapPerm("(123)");   // new FunctionIntInt((1, 2), (2, 3), (3, 1));
            var κ = new GapPerm("(13)");    // new FunctionIntInt((1, 3), (2, 2), (3, 1));

            string lookup(GapPerm f)
            {
                var items = new[] { (ε, "ε"), (α, "α"), (β, "β"), (γ, "γ"), (σ, "σ"), (κ, "κ") };

                return items.First(elt => f == elt.Item1).Item2;
            }

            var S3 = new Group<GapPerm>
            {
                Identity = ε,
                Set = new[] { ε, α, β, γ, σ, κ }.ToMathSet(),
                Op = (a, b) => a.Compose(b),
                Lookup = lookup,
                OpString = "·"
            };

            Write("S3 "); S3.ShowOperationTableColored(); WriteLine();
                                                         
            foreach (var a in S3.Set)
            {
                foreach (var b in S3.Set)
                {
                    WriteLine("{0}{1}{2}{3} -> {4}",
                        lookup(a),
                        lookup(b),
                        lookup(S3.Inverse(a)),
                        lookup(S3.Inverse(b)),
                        lookup(S3.Op_(a, b, S3.Inverse(a), S3.Inverse(b))));
                }

                //foreach (var b in S3.Set)
                //{
                //    WriteLine("{5}{6}{5}⁻¹{6}⁻¹ -> {0}{1}{2}{3} -> {4}",
                //        lookup(a),
                //        lookup(b),
                //        lookup(S3.Inverse(a)),
                //        lookup(S3.Inverse(b)),
                //        lookup(S3.Op_(a, b, S3.Inverse(a), S3.Inverse(b))),

                //        lookup(a),
                //        lookup(b));
                //}

                WriteLine();
            }

            WriteLine();

            foreach (var a in S3.Set)
            {
                foreach (var b in S3.Set)
                {
                    Write("{0} ", lookup(S3.Op_(a, b, S3.Inverse(a), S3.Inverse(b))));
                }

                WriteLine();
            }

            WriteLine();

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

            ShowCommutators(S3);

            WriteLine();

            // S3.Commutators -> ε β σ
            
            var H = S3.Subgroup(new[] { ε, β, σ });
            
            foreach (var elt in S3.CosetGrouping(H, "H"))
                WriteLine("{0}   {1}",
                    String.Join(" ", elt.Select(item => String.Format("{0} =", item.ToString()))),
                    elt.Key.ConvertAll(S3.Lookup));

            WriteLine();
            
            Write("S3/H ");

            S3.QuotientGroup(H, "H").ShowOperationTableColored();
        }
    }
}
