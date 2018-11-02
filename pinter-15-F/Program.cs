using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AbstractAlgebraPowerSet;

using AbstractAlgebraMathSet;
using AbstractAlgebraGroup;
using AbstractAlgebraGapPerm;
using AbstractAlgebraQuotientGroup;

using static System.Console;

namespace pinter_15_F
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            //MathSet<int> center(Group<int> G)
            //{
            //    return G.Set.Where(a => G.Set.All(b => G.Op(a, b) == G.Op(b, a))).ToMathSet();
            //}

            // G.Set.Where(elt => G.Set.All(b => Commute(a,b)))

            // G.Set.Where(elt => G.Set.All(CommutesWith(a)))

            //MathSet<GapPerm> center_GapPerm(Group<GapPerm> G)
            //{
            //    return G.Set.Where(a => G.Set.All(b => G.Op(a, b) == G.Op(b, a))).ToMathSet();
            //}

                        
            var group_table_gap_perm = new List<(Group<GapPerm>, string)>();

            var group_table_int = new List<(Group<int>, string)>();

            var group_table_mathset_int = new List<(Group<MathSet<int>>, string)>();

            {

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
                    Lookup = lookup
                };

                // (S3, "S3")

                // WriteLine(center_GapPerm(S3));

                WriteLine("S3 center : {0}", S3.Center());
                                
                group_table_gap_perm.Add((S3, "S3"));
            }

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

                Write("D4 "); D4.ShowOperationTableColored();
                
                WriteLine("D4 center : {0}\n", D4.Center());

                // D4/C

                // D4.QuotientGroup(D4.Center())

                {
                    var C = D4.Subgroup(D4.Center());

                    Write("D4/C ");

                    D4.QuotientGroup(C).ShowOperationTableColored();
                }

                WriteLine();

                group_table_gap_perm.Add((D4, "D4"));


            }

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

                WriteLine("D6 center : {0}", D6.Center());

                // D6/C
                                
                {
                    var C = D6.Subgroup(D6.Center());

                    Write("D6/C ");

                    D6.QuotientGroup(C).ShowOperationTableColored();
                }

                WriteLine();
                
                group_table_gap_perm.Add((D6, "D6"));
            }

            {
                var Z8 = new Group<int>
                {
                    Identity = 0,
                    Set = Enumerable.Range(0, 8).ToMathSet(),
                    Op = (a, b) => (a + b) % 8,
                    OpString = "+"
                };
                                
                WriteLine("Z8 center : {0}", Z8.Center());

                group_table_int.Add((Z8, "Z8"));
            }

            {
                var Z10 = new Group<int>
                {
                    Identity = 0,
                    Set = Enumerable.Range(0, 10).ToMathSet(),
                    Op = (a, b) => (a + b) % 10,
                    OpString = "+"
                };

                WriteLine("Z10 center : {0}", Z10.Center());

                group_table_int.Add((Z10, "Z10"));
            }

            {
                IEnumerable<T> SymmetricDifference<T>(IEnumerable<T> a, IEnumerable<T> b) =>
                                a.Except(b).Union(b.Except(a));

                var P3 = new Group<MathSet<int>>
                {
                    Identity = new MathSet<int> { },
                    Set = new[] { 1, 2, 3 }.PowerSet().Select(elt => elt.ToMathSet()).ToMathSet(),
                    Op = (a, b) => SymmetricDifference(a, b).ToMathSet()
                };

                WriteLine("P3 center : {0}", P3.Center());

                group_table_mathset_int.Add((P3, "P3"));
            }

            {
                var a = new List<int> { 10, 20, 30 };

                var b = new List<string> { "abc", "bcd", "cde" };

                var c = new List<double> { 1.2, 2.3, 3.4 };


                var ls = new List<System.Collections.IList> { a, b, c };

                //foreach (var elt in ls)
                //    Console.WriteLine(elt.Count);

                // var ls = new List<List<object>> { a, b, c };


                // Group interface - IGroup

                // new List<IGroup>
                

            }

            // S3 D4 D6 Z8 Z10 P3

            foreach (var tup in group_table_gap_perm)
            {
                WriteLine("{0,-4}   size : {1,3}   center size : {2,3}", tup.Item2, tup.Item1.Set.Count, tup.Item1.Center().Count);
            }

            foreach (var tup in group_table_int)
            {
                WriteLine("{0,-4}   size : {1,3}   center size : {2,3}", tup.Item2, tup.Item1.Set.Count, tup.Item1.Center().Count);
            }

            foreach (var tup in group_table_mathset_int)
            {
                WriteLine("{0,-4}   size : {1,3}   center size : {2,3}", tup.Item2, tup.Item1.Set.Count, tup.Item1.Center().Count);
            }


        }
    }
}
