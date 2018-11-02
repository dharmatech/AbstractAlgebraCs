using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using AbstractAlgebraUtil;

using AbstractAlgebraFunctionIntInt;
using AbstractAlgebraMathSet;
// using AbstractAlgebra.permutation;
using AbstractAlgebraCycles;

using static System.Console;
using static AbstractAlgebraSymmetricGroup.Utils;

using static AbstractAlgebra.alternating_group.Utils;

using static AbstractAlgebra.Program.Utils;

namespace AbstractAlgebra.Program
{    
    public static class Utils
    {
        public static void show_operation_table<T>(MathSet<T> G, Func<T, T, T> Op, Func<T, string> lookup)
        {
            WriteLine("operation table:\n");

            var width = G.Select(elt => lookup(elt).Count()).Max();

            foreach (var x in G)
            {
                foreach (var y in G) Write("{0} ", lookup(Op(x, y)).PadLeft(width));

                WriteLine();
            }
        }

        public static void show_operation_table_colored<T>(MathSet<T> G, Func<T, T, T> Op, Func<T, String> lookup)
        {
            WriteLine("operation table:\n");

            var width = G.Select(elt => lookup(elt).Count()).Max();

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
                var i = G.ToList().IndexOf(elt);

                color_thunks[i % color_thunks.Count()]();
            }


            Write("{0}|", "".PadLeft(width));

            foreach (var elt in G) { pick_color(elt); Write("{0} ", lookup(elt).PadLeft(width)); }

            WriteLine();


            color_thunks[0](); WriteLine(new String('-', (G.Count() + 1) * (width + 1)));

            foreach (var x in G)
            {
                pick_color(x); Write("{0}", lookup(x).PadLeft(width)); color_thunks[0](); Write("|");

                foreach (var y in G)
                {
                    var elt = Op(x, y);

                    pick_color(elt);

                    Write("{0} ", lookup(elt).PadLeft(width));
                }

                WriteLine();
            }

            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;
        }

        public static MathSet<MathSet<T>> Cosets_generic<T>(MathSet<T> G, MathSet<T> H, Func<T, T, T> Op) => G.ConvertAll(a => H.ConvertAll(h => Op(h, a)));

        public static (IEnumerable<IEnumerable<T>>, IEnumerable<IEnumerable<T>>) Subgroups_tuple<T>(MathSet<T> G, Func<T, T, T> Op)
        {
            var subgroups = G.PowerSet().OrderBy(elt => elt.Count())
                .Where(set => new[] { set, set }.CartesianProduct().Select(elt => set.Contains(Op(elt.ElementAt(0), elt.ElementAt(1)))).All(elt => elt))
                .Where(set => set.Count() > 0);

            var proper_subgroups = subgroups
                .Where(subgroup => subgroup.Count() > 1)
                .Where(subgroup => subgroup.Count() < G.Count());

            return (subgroups, proper_subgroups);
        }
        
        public static void ShowGroupCosets<T>(MathSet<T> G, Func<T, T, T> Op, Func<T, string> lookup)
        {
            var (subgroups, proper_subgroups) = Subgroups_tuple(G, Op);

            WriteLine("subgroups:");
            foreach (var subgroup in subgroups) WriteLine(subgroup.Select(lookup).ToMathSet());
            WriteLine();

            //WriteLine("subgroups:");
            //foreach (var subgroup in subgroups)
            //{
            //    WriteLine("order is {1}   index is {2}   {0}",
            //        subgroup.Select(lookup).ToMathSet(),
            //        subgroup.Count(),
            //        G.Count()/subgroup.Count()
            //        );
            //}

            WriteLine();

            WriteLine("proper subgroups:");
            foreach (var subgroup in proper_subgroups) WriteLine(subgroup.Select(lookup).ToMathSet());
            WriteLine();

            {
                foreach (var H in proper_subgroups.Select(elt => elt.ToMathSet()))
                {
                    WriteLine("------------------------------");
                    WriteLine("H = {0}   order is {1}   index is {2}", H.ConvertAll(lookup), H.Count(), G.Count() / H.Count());
                    WriteLine();

                    WriteLine("cosets:");

                    foreach (var coset in Cosets_generic(G, H, Op)) WriteLine(coset.ConvertAll(f => lookup(f)));

                    WriteLine();

                    {
                        var ls = G.Select(a => (a, H.ConvertAll(h => Op(h, a))));

                        foreach (var coset in Cosets_generic(G, H, Op))
                        {
                            foreach (var a in ls.Where(elt => coset == elt.Item2).Select(elt => elt.a))
                                Write("{0} {1} = ", H.ConvertAll(lookup), lookup(a));

                            WriteLine(coset.ConvertAll(lookup));
                        }
                    }
                }
            }
        }

    }

    public static class Extensions
    {
        // public static T display<T>(this T obj) { WriteLine(obj); return obj; }
        
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> tail, T head)
        {
            yield return head;
            foreach (var item in tail) yield return item;
        }

        public static IEnumerable<IEnumerable<T>> PowerSet<T>(this IEnumerable<T> items)
        {
            if (items.Any() == false) yield return items;

            else
            {
                var head = items.First();
                var powerset = items.Skip(1).PowerSet().ToList();
                foreach (var set in powerset) yield return set.Prepend(head);
                foreach (var set in powerset) yield return set;
            }
        }

        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> seqs)
        {
            IEnumerable<IEnumerable<T>> empty_product = new[] { Enumerable.Empty<T>() };
            
            return
                seqs.Aggregate(
                    empty_product,
                    (acc, seq) =>
                        from accseq in acc
                        from item in seq
                        select accseq.Concat(new[] { item }));
        }

    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // ----------------------------------------------------------------------

            {
                var ε = new FunctionIntInt((1, 1), (2, 2), (3, 3));
                var α = new FunctionIntInt((1, 1), (2, 3), (3, 2));
                var β = new FunctionIntInt((1, 3), (2, 1), (3, 2));
                var γ = new FunctionIntInt((1, 2), (2, 1), (3, 3));
                var σ = new FunctionIntInt((1, 2), (2, 3), (3, 1));
                var κ = new FunctionIntInt((1, 3), (2, 2), (3, 1));

                var S_3 = new[] { ε, α, β, γ, σ, κ }.ToMathSet();

                var items = new[] { (ε, "ε"), (α, "α"), (β, "β"), (γ, "y"), (σ, "σ"), (κ, "k") };

                string lookup(FunctionIntInt f) => items.First(elt => f == elt.Item1).Item2;

                WriteLine("S_3: {0}", S_3.ConvertAll(lookup)); WriteLine();

                show_operation_table(S_3, (a, b) => a.Compose(b), lookup);

                WriteLine();

                ShowGroupCosets(S_3, (a, b) => a.Compose(b), lookup);

                WriteLine();
            }

            WriteLine("----------------------------------------------------------------------");

            {
                var G = Enumerable.Range(0, 15).ToMathSet();

                int add(int a, int b) => (a + b) % 15;

                WriteLine("G: {0}\n", G);

                show_operation_table(G, add, Convert.ToString); WriteLine();

                ShowGroupCosets(G, add, Convert.ToString); WriteLine();
            }

            WriteLine("----------------------------------------------------------------------");
            
            {     
                // 1---2
                // |   |
                // 4---3

                var R0   = new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)); 
                var R90  = new FunctionIntInt((1, 2), (2, 3), (3, 4), (4, 1)); 
                var R180 = new FunctionIntInt((1, 3), (2, 4), (3, 1), (4, 2)); 
                var R270 = new FunctionIntInt((1, 4), (2, 1), (3, 2), (4, 3)); 
                var F13  = new FunctionIntInt((1, 1), (2, 4), (3, 3), (4, 2)); // flip about 13         axis
                var F24  = new FunctionIntInt((1, 3), (2, 2), (3, 1), (4, 4)); // flip about 24         axis
                var Fva  = new FunctionIntInt((1, 2), (2, 1), (3, 4), (4, 3)); // flip about vertical   axis
                var Fha  = new FunctionIntInt((1, 4), (2, 3), (3, 2), (4, 1)); // flip about horizontal axis

                var D_4 = new[] { R0, R90, R180, R270, F13, F24, Fva, Fha }.ToMathSet();

                var items = new[] { (R0, "R0"), (R90, "R90"), (R180, "R180"), (R270, "R270"), (F13, "F13"), (F24, "F24"), (Fva, "Fva"), (Fha, "Fha") };

                string lookup(FunctionIntInt f) => items.First(elt => f == elt.Item1).Item2;

                WriteLine("D_4: {0}", D_4.ConvertAll(lookup)); WriteLine();
                                
                show_operation_table_colored(D_4, (a, b) => a.Compose(b), lookup); WriteLine();


                //{
                //    var a = R90;
                //    var b = F13;

                //    WriteLine("ba   {0}", lookup(b.Compose(a)));
                //    WriteLine("a³b  {0}", lookup(a.Compose(a.Compose(a.Compose(b)))));
                //}
                                
                {
                    // 13.H.4

                    void check(FunctionIntInt a, FunctionIntInt b)
                    {
                        WriteLine("a    {0}", lookup(a));
                        WriteLine("b    {0}", lookup(b));
                        WriteLine("ba   {0}", lookup(b.Compose(a)));
                        WriteLine("a³b  {0}", lookup(a.Compose(a.Compose(a.Compose(b)))));
                    }

                    check(R90, F13); WriteLine();
                    check(R90, F24); WriteLine();
                    check(R90, Fva); WriteLine();
                    check(R90, Fha); WriteLine();
                }
                
                ShowGroupCosets(D_4, (a, b) => a.Compose(b), lookup);
            }

            WriteLine("----------------------------------------------------------------------");

            // R0 R1 R2 R3 F13 F24 Fv Fh

            {
                var R0 = new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4)); // R0
                var R1 = new FunctionIntInt((1, 2), (2, 3), (3, 4), (4, 1)); // R90
                var R2 = new FunctionIntInt((1, 3), (2, 4), (3, 1), (4, 2)); // R180
                var R3 = new FunctionIntInt((1, 4), (2, 1), (3, 2), (4, 3)); // R270
                var R4 = new FunctionIntInt((1, 1), (2, 4), (3, 3), (4, 2)); // flip about 13         axis
                var R5 = new FunctionIntInt((1, 3), (2, 2), (3, 1), (4, 4)); // flip about 24         axis
                var R6 = new FunctionIntInt((1, 2), (2, 1), (3, 4), (4, 3)); // flip about vertical   axis
                var R7 = new FunctionIntInt((1, 4), (2, 3), (3, 2), (4, 1)); // flip about horizontal axis

                var D_4 = new[] { R0, R1, R2, R3, R4, R5, R6, R7 }.ToMathSet();

                var items = new[] { (R0, "R0"), (R1, "R1"), (R2, "R2"), (R3, "R3"), (R4, "R4"), (R5, "R5"), (R6, "R6"), (R7, "R7") };

                string lookup(FunctionIntInt f) => items.First(elt => f == elt.Item1).Item2;

                WriteLine("D_4: {0}", D_4.ConvertAll(lookup)); WriteLine();

                show_operation_table(D_4, (a, b) => a.Compose(b), lookup); WriteLine();

                ShowGroupCosets(D_4, (a, b) => a.Compose(b), lookup);
            }

            // ----------------------------------------------------------------------

            {
                var ε = new FunctionIntInt((1, 1), (2, 2), (3, 3));
                var α = new FunctionIntInt((1, 1), (2, 3), (3, 2));
                var β = new FunctionIntInt((1, 3), (2, 1), (3, 2));
                var γ = new FunctionIntInt((1, 2), (2, 1), (3, 3));
                var σ = new FunctionIntInt((1, 2), (2, 3), (3, 1));
                var κ = new FunctionIntInt((1, 3), (2, 2), (3, 1));

                var S_3 = new[] { ε, α, β, γ, σ, κ }.ToMathSet();

                var items = new[] { (ε, "ε"), (α, "α"), (β, "β"), (γ, "y"), (σ, "σ"), (κ, "k") };

                string lookup(FunctionIntInt f) => items.First(elt => f == elt.Item1).Item2;

                WriteLine("S_3: {0}", S_3.ConvertAll(lookup)); WriteLine();

                // operation table

                // show_operation_table(G, lookup, op)

                foreach (var x in S_3)
                {
                    foreach (var y in S_3) Write("{0} ", lookup(x.Compose(y)));

                    WriteLine();
                }

                WriteLine();

                {
                    WriteLine("13.A.1"); WriteLine();

                    var G = S_3;
                    var H = new[] { ε, β, σ }.ToMathSet();

                    WriteLine("H = {0}", H.ConvertAll(lookup));

                    WriteLine("order H = {0}", H.Count);
                    WriteLine("index H = {0}", G.Count / H.Count);

                    WriteLine();

                    foreach (var a in G)
                        WriteLine("H{0} = {1}",
                            lookup(a),
                            H.ConvertAll(h => lookup(h.Compose(a))));

                    WriteLine(); WriteLine("cosets:");

                    foreach (var coset in G.ConvertAll(a => H.ConvertAll(h => h.Compose(a))))
                        WriteLine(coset.ConvertAll(f => lookup(f)));

                    WriteLine();
                }
                
                ShowGroupCosets(S_3, (a, b) => a.Compose(b), lookup);
                
                {
                    WriteLine("13.A.2"); WriteLine();

                    var G = S_3;
                    var H = new[] { ε, α }.ToMathSet();
                    
                    WriteLine("H = {0}", H.ConvertAll(lookup)); 

                    WriteLine("order H = {0}", H.Count);
                    WriteLine("index H = {0}", G.Count / H.Count);

                    WriteLine();

                    foreach (var a in G)
                        WriteLine("H{0} = {1}",
                            lookup(a),
                            H.ConvertAll(h => lookup(h.Compose(a))));
                    
                    WriteLine(); WriteLine("cosets:");

                    foreach (var coset in G.ConvertAll(a => H.ConvertAll(h => h.Compose(a))))
                        WriteLine(coset.ConvertAll(f => lookup(f)));

                    WriteLine();
                }

            }

            {
                var Z4xZ2 = new[] { (0, 0), (0, 1), (1, 0), (1, 1), (2, 0), (2, 1), (3, 0), (3, 1) }.ToMathSet();

                // var Z4xZ2 = new[] { (0, 0), (1, 0), (2, 0), (3, 0), (0, 1), (1, 1), (2, 1), (3, 1) }.ToMathSet();

                (int, int) Op((int, int) a, (int, int) b) => ((a.Item1 + b.Item1) % 4, (a.Item2 + b.Item2) % 2);

                show_operation_table_colored(Z4xZ2, Op, a => $"{a}"); WriteLine();
            }

            {
                var Z2xZ2xZ2 = new[] { (0, 0, 0), (0, 0, 1), (0, 1, 0), (0, 1, 1), (1, 0, 0), (1, 0, 1), (1, 1, 0), (1, 1, 1) }.ToMathSet();

                (int,int,int) Op((int,int,int) a, (int,int,int) b) => ((a.Item1 + b.Item1) % 2, (a.Item2 + b.Item2) % 2, (a.Item3 + b.Item3) % 2);

                show_operation_table_colored(Z2xZ2xZ2, Op, a => $"{a}"); WriteLine();

                ShowGroupCosets(Z2xZ2xZ2, Op, a => $"{a}");
            }


            WriteLine("----------------------------------------------------------------------");

            {
                var R0 = new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4), (5, 5));
                var R1 = new FunctionIntInt((1, 2), (2, 3), (3, 4), (4, 5), (5, 1));
                var R2 = new FunctionIntInt((1, 3), (2, 4), (3, 5), (4, 1), (5, 2));
                var R3 = new FunctionIntInt((1, 4), (2, 5), (3, 1), (4, 2), (5, 3));
                var R4 = new FunctionIntInt((1, 5), (2, 1), (3, 2), (4, 3), (5, 4));
                var Ra = new FunctionIntInt((1, 1), (2, 5), (3, 4), (4, 3), (5, 2));
                var Rb = new FunctionIntInt((1, 2), (2, 1), (3, 5), (4, 4), (5, 3));
                var Rc = new FunctionIntInt((1, 3), (2, 2), (3, 1), (4, 5), (5, 4));
                var Rd = new FunctionIntInt((1, 4), (2, 3), (3, 2), (4, 1), (5, 5));
                var Re = new FunctionIntInt((1, 5), (2, 4), (3, 3), (4, 2), (5, 1));
                
                var D_5 = new[] { R0, R1, R2, R3, R4, Ra, Rb, Rc, Rd, Re }.ToMathSet();
                
                var items = new[] { (R0, "R0"), (R1, "R1"), (R2, "R2"), (R3, "R3"), (R4, "R4"), (Ra, "Ra"), (Rb, "Rb"), (Rc, "Rc"), (Rd, "Rd"), (Re, "Re") };

                string lookup(FunctionIntInt f) => items.First(elt => f == elt.Item1).Item2;

                WriteLine("D_5: {0}", D_5.ConvertAll(lookup)); WriteLine();

                show_operation_table(D_5, (a, b) => a.Compose(b), lookup); WriteLine();

                show_operation_table_colored(D_5, (a, b) => a.Compose(b), lookup); WriteLine();

                ShowGroupCosets(D_5, (a, b) => a.Compose(b), lookup);
            }





            {
                WriteLine("---------- 13.A.3 ----------"); WriteLine();

                var G = Enumerable.Range(0, 15).ToMathSet();

                var H = new[] { 0, 5, 10 }.ToMathSet();

                int add(int a, int b) => (a + b) % 15;

                WriteLine("G: {0}", G);
                WriteLine("H: {0}", H);

                WriteLine("order H = {0}", H.Count);
                WriteLine("index H = {0}", G.Count / H.Count);

                WriteLine();
                
                foreach (var a in G) WriteLine("H{0,2} = {1}", a, H.ConvertAll(h => add(h, a)));

                WriteLine(); WriteLine("cosets:\n");
                
                foreach (var coset in G.ConvertAll(a => H.ConvertAll(h => add(h, a))))
                    WriteLine(coset);

                WriteLine();
            }
            
            ShowGroupCosets(Enumerable.Range(0, 15).ToMathSet(), (a, b) => (a + b) % 15, Convert.ToString);
            
            {
                WriteLine("---------- 13.A.4 ----------"); WriteLine();

                var R0 = new FunctionIntInt((1, 1), (2, 2), (3, 3), (4, 4));
                var R1 = new FunctionIntInt((1, 2), (2, 3), (3, 4), (4, 1));
                var R2 = new FunctionIntInt((1, 3), (2, 4), (3, 1), (4, 2));
                var R3 = new FunctionIntInt((1, 4), (2, 1), (3, 2), (4, 3));
                var R4 = new FunctionIntInt((1, 1), (2, 4), (3, 3), (4, 2));
                var R5 = new FunctionIntInt((1, 3), (2, 2), (3, 1), (4, 4));
                var R6 = new FunctionIntInt((1, 2), (2, 1), (3, 4), (4, 3));
                var R7 = new FunctionIntInt((1, 4), (2, 3), (3, 2), (4, 1));

                var D_4 = new[] { R0, R1, R2, R3, R4, R5, R6, R7 }.ToMathSet();

                var items = new[] { (R0, "R0"), (R1, "R1"), (R2, "R2"), (R3, "R3"), (R4, "R4"), (R5, "R5"), (R6, "R6"), (R7, "R7") };

                string lookup(FunctionIntInt f) => items.First(elt => f == elt.Item1).Item2;

                WriteLine("D_4: {0}", D_4.ConvertAll(lookup)); WriteLine();

                foreach (var x in D_4)
                {
                    foreach (var y in D_4) Write("{0} ", lookup(x.Compose(y)));

                    WriteLine();
                }

                WriteLine();

                {
                    var G = D_4;
                    var H = new[] { R0, R4 }.ToMathSet();

                    WriteLine("H = {0}", H.ConvertAll(lookup));

                    WriteLine("order H = {0}", H.Count);
                    WriteLine("index H = {0}", G.Count / H.Count);
                    WriteLine();

                    foreach (var a in G)
                        WriteLine("H {0} = {1}",
                            lookup(a),
                            H.ConvertAll(h => lookup(h.Compose(a))));

                    WriteLine();

                    {
                        var ls = G.Select(a => (a, H.ConvertAll(h => h.Compose(a))));

                        foreach (var coset in G.ConvertAll(a => H.ConvertAll(h => h.Compose(a))))
                        {
                            foreach(var a in ls.Where(elt => coset == elt.Item2).Select(elt => elt.a))
                                Write("H {0} = ", lookup(a));
                            
                            WriteLine(coset.ConvertAll(lookup));
                        }
                    }

                    WriteLine();

                    {
                        var ls = G.Select(a => (a, H.ConvertAll(h => h.Compose(a))));

                        foreach (var coset in G.ConvertAll(a => H.ConvertAll(h => h.Compose(a))))
                        {
                            foreach (var a in ls.Where(elt => coset == elt.Item2).Select(elt => elt.a))
                                Write("{0} {1} = ", H.ConvertAll(lookup), lookup(a));

                            WriteLine(coset.ConvertAll(lookup));
                        }
                    }

                    WriteLine(); WriteLine("cosets:");

                    foreach (var coset in G.ConvertAll(a => H.ConvertAll(h => h.Compose(a))))
                        WriteLine(coset.ConvertAll(f => lookup(f)));

                    WriteLine();
                }
                
                ShowGroupCosets(D_4, (a, b) => a.Compose(b), lookup);

            }

            {
                WriteLine("---------- 13.A.5 ----------"); WriteLine();

                var G = SymmetricGroup(4);
                var H = AlternatingGroup(4);

                string lookup(FunctionIntInt f) => G.ToList().FindIndex(elt => elt.Equals(f)).ToString();

                foreach (var x in G)
                {
                    foreach (var y in G) Write("{0,3}", lookup(x.Compose(y)));

                    WriteLine();
                }

                WriteLine();

                WriteLine("H = {0}", H.ConvertAll(lookup));

                WriteLine("order H = {0}", H.Count);
                WriteLine("index H = {0}", G.Count / H.Count);
                WriteLine();

                foreach (var a in G)
                    WriteLine("H{0,2} = {1}",
                        lookup(a),
                        H.ConvertAll(h => lookup(h.Compose(a))));

                WriteLine(); WriteLine("cosets:");

                foreach (var coset in G.ConvertAll(a => H.ConvertAll(h => h.Compose(a))))
                    WriteLine(coset.ConvertAll(f => lookup(f)));

                WriteLine();
            }

            {
                // consider: https://math.stackexchange.com/questions/76176/enumerating-all-subgroups-of-the-symmetric-group

                // consider: https://math.stackexchange.com/questions/1569349/how-to-find-all-subgroups-of-a-group-in-gap

                // subgroups

                var G = SymmetricGroup(4);

                string lookup(FunctionIntInt f) => G.ToList().FindIndex(elt => elt.Equals(f)).ToString();


                foreach (var elt in G) WriteLine("{0,-4} {1}", lookup(elt), elt);

                show_operation_table_colored(G, (a, b) => a.Compose(b), lookup);


                WriteLine("subgroups:");

                //{
                //    var result = G.PowerSet();
                //}


                //var subgroups = G.PowerSet().OrderBy(elt => elt.Count())
                //    .Where(set => new[] { set, set }.CartesianProduct().Select(elt => set.Contains(elt.ElementAt(0).Compose(elt.ElementAt(1)))).All(elt => elt))
                //    .Where(set => set.Count() > 0);

                //var subgroups = G.PowerSet()//.OrderBy(elt => elt.Count())
                //    .Where(set => new[] { set, set }.CartesianProduct().Select(elt => set.Contains(elt.ElementAt(0).Compose(elt.ElementAt(1)))).All(elt => elt))
                //    .Where(set => set.Count() > 0);

                var identity = G.ElementAt(0);

                //var result_a = G.PowerSet().Count();

                // var result_b = G.PowerSet().Take(1).Where(set => set.Contains(identity));

                // var result_c = G.PowerSet().Where(set => set.Contains(identity)).Take(10).Select(elt => elt.ToList()).ToList();

                // var result_d = G.PowerSet().Where(set => set.Contains(identity)).Select(elt => elt.ToList()).ToList();

                // var result_e = G.PowerSet().Where(set => set.ToMathSet().Contains(identity)).Select(elt => elt.ToList()).ToList();

                //var result_f = G

                //    .PowerSet()

                //    .Where(set => set.ToMathSet().Contains(identity))

                //    .OrderBy(elt => elt.Count())

                //    ;

                //.Select(elt => elt.ToList()).ToList();


                //var subgroups = G

                //    .PowerSet()

                //    .Where(set => set.ToMathSet().Contains(G.ElementAt(0)))

                //    .OrderBy(elt => elt.Count())
                //    .Where(set => new[] { set, set }.CartesianProduct().Select(elt => set.Contains(elt.ElementAt(0).Compose(elt.ElementAt(1)))).All(elt => elt))
                //    .Where(set => set.Count() > 0);


                //{
                //    var subgroups_b = G.AsParallel()

                //    .PowerSet()

                //    .Where(set => set.ToMathSet().Contains(G.ElementAt(0)))

                //    .OrderBy(elt => elt.Count())
                //    .Where(set => cartesian_product(set, set).Select(elt => set.Contains(elt.Item1.Compose(elt.Item2))).All(elt => elt))
                //    .Where(set => set.Count() > 0);

                //    var result = subgroups_b.Count();
                    
                //}


                //{
                //    var id = G.ElementAt(0);

                //    var result = G.PowerSet().Count();

                //    var result_b = G.PowerSet().Where(set => set.Contains(id)).Count();
                //}

                
                //IEnumerable<(FunctionIntInt,FunctionIntInt)> cartesian_product(IEnumerable<FunctionIntInt> a, IEnumerable<FunctionIntInt> b)
                //{
                //    foreach (var x in a) foreach (var y in b) yield return (x, y);
                //}

                
                var subgroups = G.AsParallel()

                    .PowerSet()

                    .Where(set => set.ToMathSet().Contains(G.ElementAt(0)))

                    .OrderBy(elt => elt.Count())
                    .Where(set => new[] { set, set }.CartesianProduct().Select(elt => set.Contains(elt.ElementAt(0).Compose(elt.ElementAt(1)))).All(elt => elt))
                    .Where(set => set.Count() > 0);

                // specialized version of CartesianProduct for two sequences
                //     result is sequence of two-elements tuples
                //         (a,b) (b,c) (c,d) ...


                foreach (var subgroup in subgroups)
                    WriteLine(subgroup.Select(elt => lookup(elt)).ToMathSet());

                WriteLine(); WriteLine("proper subgroups:");

                var proper_subgroups = subgroups.Where(subgroup => subgroup.Count() > 1).Where(subgroup => subgroup.Count() < G.Count());

                foreach (var subgroup in proper_subgroups)
                    WriteLine(subgroup.Select(elt => lookup(elt)).ToMathSet());

                WriteLine();

                {
                    foreach (var H in proper_subgroups.Select(elt => elt.ToMathSet()))
                    {
                        WriteLine("------------------------------");

                        WriteLine("H = {0}", H.ConvertAll(lookup));

                        WriteLine(); WriteLine("cosets:");

                        foreach (var coset in G.ConvertAll(a => H.ConvertAll(h => h.Compose(a))))
                            WriteLine(coset.ConvertAll(f => lookup(f)));

                        WriteLine();

                        {
                            var ls = G.Select(a => (a, H.ConvertAll(h => h.Compose(a))));

                            foreach (var coset in G.ConvertAll(a => H.ConvertAll(h => h.Compose(a))))
                            {
                                foreach (var a in ls.Where(elt => coset == elt.Item2).Select(elt => elt.a))
                                    Write("{0} {1} = ", H.ConvertAll(lookup), lookup(a));

                                WriteLine(coset.ConvertAll(lookup));
                            }
                        }
                    }
                }
            }

        }
    }
}
