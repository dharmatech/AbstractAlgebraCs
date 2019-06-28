using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using System.Numerics;

using AbstractAlgebraZipMany;
using AbstractAlgebraMathSet;
using AbstractAlgebraCycles;
using AbstractAlgebraGapPerm;
using AbstractAlgebraGroup;
using AbstractAlgebraIsomorphism;

using static AbstractAlgebraGenerate.Utils;

using static AbstractAlgebraStandardGroupZ.Utils;
using static AbstractAlgebraStandardGroupZxZ.Utils;
using static AbstractAlgebraStandardGroupZxZxZ.Utils;
using static AbstractAlgebraStandardGroupP.Utils;

using Xunit;


namespace AbstractAlgebraTests
{
    public class Tests
    {
        [Fact]
        public static void CyclesFromString_000()
        {
            Assert.Equal(
                new Cycles(new Cycle(1,4,5), new Cycle(3,7), new Cycle(6,8,2)),
                Cycles.FromString("(145)(37)(682)"));
        }

        [Fact] public static void CyclesToPermutation_000()
        {
            Assert.Equal(
                new GapPerm(0, 4, 6, 7, 5, 1, 8, 3, 2, 9),
                Cycles.FromString("(145)(37)(682)").ToPermutation(9));
        }

        [Fact]
        public static void CyclesToPermutation_001()
        {
            Assert.Equal(
                new GapPerm(0, 7, 8, 5, 9, 4, 2, 1, 6, 3),
                Cycles.FromString("(17)(628)(9354)").ToPermutation(9));
        }

        [Fact]
        public static void CyclesToPermutation_002()
        {
            Assert.Equal(
                new GapPerm(0, 8, 5, 6, 9, 7, 3, 1, 2, 4),
                Cycles.FromString("(71825)(36)(49)").ToPermutation(9));
        }

        [Fact]
        public static void CyclesToPermutation_003()
        {
            Assert.Equal(
                new GapPerm(0, 2, 1, 4, 7, 5, 6, 3, 8, 9),
                Cycles.FromString("(12)(347)").ToPermutation(9));
        }

        [Fact]
        public static void CyclesToPermutation_004()
        {
            Assert.Equal(
                new GapPerm(0, 3, 8, 2, 6, 5, 1, 7, 4, 9),
                Cycles.FromString("(147)(1678)(74132)").ToPermutation(9));
        }

        [Fact]
        public static void CyclesToPermutation_005()
        {
            Assert.Equal(
                new GapPerm(0, 3, 5, 4, 9, 2, 1, 7, 6, 8),
                Cycles.FromString("(6148)(2345)(12493)").ToPermutation(9));
        }

        [Fact]
        public static void GapPerm_ToDisjointCycles_000()
        {
            Assert.Equal(
                new Cycles(new Cycle(1, 4, 5), new Cycle(2, 9, 3), new Cycle(6, 7)),
                new GapPerm(0, 4, 9, 2, 5, 1, 7, 6, 8, 3).ToDisjointCycles());
        }

        [Fact]
        public static void GapPerm_ToDisjointCycles_001()
        {
            Assert.Equal(
                new Cycles(new Cycle(1, 7), new Cycle(2, 4), new Cycle(3, 9, 5), new Cycle(6, 8)),
                new GapPerm(0, 7, 4, 9, 2, 3, 8, 1, 6, 5).ToDisjointCycles());
        }

        [Fact]
        public static void GapPerm_ToDisjointCycles_002()
        {
            Assert.Equal(
                new Cycles(new Cycle(1, 7, 4, 3, 5), new Cycle(2, 9, 6)),
                new GapPerm(0, 7, 9, 5, 3, 1, 2, 4, 8, 6).ToDisjointCycles());
        }

        [Fact]
        public static void GapPerm_ToDisjointCycles_003()
        {
            Assert.Equal(
                "(1928)(375)",
                new GapPerm(0, 9, 8, 7, 4, 3, 6, 5, 1, 2).ToDisjointCycles().ToString());
        }

        [Fact]
        public static void Cycles_ToTranspositions_000()
        {
            Assert.Equal(
                "(82)(84)(87)(83)(81)",
                Cycles.FromString("(137428)").to_transpositions().ToString());
        }

        [Fact]
        public static void Cycles_ToTranspositions_001()
        {
            Assert.Equal(
                "(61)(64)(53)(52)(58)",
                Cycles.FromString("(416)(8235)").to_transpositions().ToString());
        }

        [Fact]
        public static void Cycles_ToTranspositions_002()
        {
            Assert.Equal(
                "(32)(31)(65)(64)(47)(45)(41)",
                Cycles.FromString("(123)(456)(1574)").to_transpositions().ToString());
        }

        [Fact]
        public static void Cycles_ToTranspositions_003()
        {
            Assert.Equal(
                "(24)(23)(21)(85)(76)",
                new GapPerm(0, 3, 1, 4, 2, 8, 7, 6, 5).ToDisjointCycles().to_transpositions().ToString());
        }
    }

    public class pinter_08_A_4
    {
        static Cycle α = "(3714)".to_cycle();
        static Cycle β = "(123)".to_cycle();
        static Cycle γ = "(24135)".to_cycle();

        [Fact]
        public static void Cycles_ToPermutation_000() => Assert.Equal("(124)(37)", new Cycles(α.inverse(), β).ToPermutation(7).ToString());

        [Fact]
        public static void Cycles_ToPermutation_001() => Assert.Equal("(125374)", new Cycles(γ.inverse(), α).ToPermutation(7).ToString());

        [Fact]
        public static void Cycles_ToPermutation_002() => Assert.Equal("(12)(47)", new Cycles(α, α, β).ToPermutation(7).ToString());

        [Fact]
        public static void Cycles_ToPermutation_003() => 
            Assert.Equal(
                "(1735)",
                new Cycles(β, β, α, γ).ToPermutation(7).ToString());

        [Fact]
        public static void Cycles_ToPermutation_004() => 
            Assert.Equal(
                "(14253)",
                new Cycles(γ, γ, γ, γ).ToPermutation(7).ToString());

        [Fact]
        public static void Cycles_ToPermutation_005() => 
            Assert.Equal(
                "(174235)",
                new Cycles(γ, γ, γ, α.inverse()).ToPermutation(7).ToString());

        [Fact]
        public static void Cycles_ToPermutation_006() => 
            Assert.Equal(
                "(12435)",
                new Cycles(β.inverse(), γ).ToPermutation(7).ToString());

        [Fact]
        public static void Cycles_ToPermutation_007() => 
            Assert.Equal(
                "(14275)",
                new Cycles(α.inverse(), γ, γ, α).ToPermutation(7).ToString());
    }

    public class pinter_08_B_1a
    {
        static GapPerm a = new GapPerm("(123)");

        [Fact] public static void GapPerm_Inverse() => Assert.Equal("(132)", a.Inverse().ToString());

        [Fact] public static void GapPerm_Compose_000() => Assert.Equal("(132)", a.Compose(a).ToString());
        [Fact] public static void GapPerm_Compose_001() => Assert.Equal("()", a.Compose(a).Compose(a).ToString());
        [Fact] public static void GapPerm_Compose_002() => Assert.Equal("(123)", a.Compose(a).Compose(a).Compose(a).ToString());
        [Fact] public static void GapPerm_Compose_003() => Assert.Equal("(132)", a.Compose(a).Compose(a).Compose(a).Compose(a).ToString());
    }

    public class pinter_08_B_1b
    {
        static GapPerm a = new GapPerm("(1234)");

        [Fact] public static void GapPerm_Inverse() => Assert.Equal("(1432)", a.Inverse().ToString());                                         
        [Fact] public static void GapPerm_Compose_000() => Assert.Equal("(13)(24)", a.Compose(a).ToString());                                  
        [Fact] public static void GapPerm_Compose_001() => Assert.Equal("(1432)", a.Compose(a).Compose(a).ToString());                         
        [Fact] public static void GapPerm_Compose_002() => Assert.Equal("()", a.Compose(a).Compose(a).Compose(a).ToString());                  
        [Fact] public static void GapPerm_Compose_003() => Assert.Equal("(1234)", a.Compose(a).Compose(a).Compose(a).Compose(a).ToString());   
    }                                                                                                                                              

    public class pinter_08_B_1c
    {
        static GapPerm a = new GapPerm("(123456)");

        [Fact] public static void GapPerm_Inverse() => Assert.Equal("(165432)", a.Inverse().ToString());                                         
        [Fact] public static void GapPerm_Compose_000() => Assert.Equal("(135)(246)", a.Compose(a).ToString());                                  
        [Fact] public static void GapPerm_Compose_001() => Assert.Equal("(14)(25)(36)", a.Compose(a).Compose(a).ToString());                     
        [Fact] public static void GapPerm_Compose_002() => Assert.Equal("(153)(264)", a.Compose(a).Compose(a).Compose(a).ToString());            
        [Fact] public static void GapPerm_Compose_003() => Assert.Equal("(165432)", a.Compose(a).Compose(a).Compose(a).Compose(a).ToString());   
    }

    public class pinter_08_C_1
    {
        static string even_or_odd(GapPerm obj) => obj.ToDisjointCycles().to_transpositions().ls.Count % 2 == 0 ? "even" : "odd";
                
        [Fact] public static void ToTranspositions_000() => Assert.Equal("odd" , even_or_odd(new GapPerm(0, 7, 4, 1, 5, 6, 2, 3, 8))); 
        [Fact] public static void ToTranspositions_001() => Assert.Equal("even", even_or_odd(new GapPerm("(71864)"))); 
        [Fact] public static void ToTranspositions_002() => Assert.Equal("even", even_or_odd(new GapPerm("(12)(76)(345)"))); 
        [Fact] public static void ToTranspositions_003() => Assert.Equal("odd" , even_or_odd(new GapPerm("(1276)(3241)(7812)"))); 
        [Fact] public static void ToTranspositions_004() => Assert.Equal("even", even_or_odd(new GapPerm("(123)(2345)(1357)"))); 
    }

    public class pinter_08_F_3
    {
        static int order(GapPerm a)
        {
            var result = new GapPerm();

            foreach (var n in Enumerable.Range(1, 100))
            {
                result = result.Compose(a);
                                
                if (result == new GapPerm(0))
                    return n;
            }

            throw new Exception();
        }

        [Fact] public static void GapPerm_Compose_000() => Assert.Equal(6, order(new GapPerm("(12)(345)")));
        [Fact] public static void GapPerm_Compose_001() => Assert.Equal(4, order(new GapPerm("(12)(3456)")));
        [Fact] public static void GapPerm_Compose_002() => Assert.Equal(20, order(new GapPerm("(1234)(56789)")));
    }

    public class pinter_09_C_1
    {
        [Fact] public static void IsIsomorphic()
        {
            var I = "I";
            var V = "V";
            var H = "H";
            var D = "D";

            var G1 = new Group<string>
            {
                Identity = I,
                Set = new[] { I, V, H, D }.ToMathSet(),
                Op = (a, b) =>
                {
                    if (a == I && b == I) return I;
                    if (a == I && b == V) return V;
                    if (a == I && b == H) return H;
                    if (a == I && b == D) return D;

                    if (a == V && b == I) return V;
                    if (a == V && b == V) return I;
                    if (a == V && b == H) return D;
                    if (a == V && b == D) return H;

                    if (a == H && b == I) return H;
                    if (a == H && b == V) return D;
                    if (a == H && b == H) return I;
                    if (a == H && b == D) return V;

                    if (a == D && b == I) return D;
                    if (a == D && b == V) return H;
                    if (a == D && b == H) return V;
                    if (a == D && b == D) return I;

                    throw new Exception();
                },
                OpString = "*"
            };

            var G2 = new Group<Complex>
            {
                Identity = new Complex(1, 0),
                Set = new[] { new Complex(1, 0), new Complex(0, -1), new Complex(-1, 0), new Complex(0, 1) }.ToMathSet(),
                Op = (a, b) => a * b,
                OpString = "*"
            };

            Assert.False(G1.IsIsomorphic(G2));
        }
    }

    public class pinter_09_C_2
    {
        [Fact]
        public static void IsIsomorphic()
        {
            var I = "I";
            var V = "V";
            var H = "H";
            var D = "D";

            var G1 = new Group<string>
            {
                Identity = I,
                Set = new[] { I, V, H, D }.ToMathSet(),
                Op = (a, b) =>
                {
                    if (a == I && b == I) return I;
                    if (a == I && b == V) return V;
                    if (a == I && b == H) return H;
                    if (a == I && b == D) return D;

                    if (a == V && b == I) return V;
                    if (a == V && b == V) return I;
                    if (a == V && b == H) return D;
                    if (a == V && b == D) return H;

                    if (a == H && b == I) return H;
                    if (a == H && b == V) return D;
                    if (a == H && b == H) return I;
                    if (a == H && b == D) return V;

                    if (a == D && b == I) return D;
                    if (a == D && b == V) return H;
                    if (a == D && b == H) return V;
                    if (a == D && b == D) return I;

                    throw new Exception();
                },
                OpString = "*"
            };
                        
            Assert.False(G1.IsIsomorphic(Z(4)));
        }
    }

    public class pinter_09_C_3
    {
        [Fact] public static void IsIsomorphic()
        {
            var H = new Group<Complex>
            {
                Identity = new Complex(1, 0),
                Set = new[] { new Complex(1, 0), new Complex(0, -1), new Complex(-1, 0), new Complex(0, 1) }.ToMathSet(),
                Op = (a, b) => a * b,
                OpString = "*"
            };

            Assert.False(P(2).IsIsomorphic(H));
        }
    }

    public class pinter_10_B
    {
        [Fact] public static void Order_Z25() => Assert.Equal(5, Z(25).Order(10));
        [Fact] public static void Order_Z16() => Assert.Equal(8, Z(16).Order(6));
        [Fact] public static void Order_S6() => Assert.Equal(4, AbstractAlgebraSymmetricGroup.Utils.SymmetricGroup(6).Order(new GapPerm(0, 6, 1, 3, 2, 5, 4)));
    }

    public class pinter_10_B_7
    {
        [Fact] public static void Order_Z24()
        {
            var Z24 = Z(24);

            Assert.Equal(new MathSet<int>(new[] { 12 }), Z24.Set.Where(elt => Z24.Order(elt) == 2).ToMathSet());
            Assert.Equal(new MathSet<int>(new[] { 8, 16 }), Z24.Set.Where(elt => Z24.Order(elt) == 3).ToMathSet());
            Assert.Equal(new MathSet<int>(new[] { 6, 18 }), Z24.Set.Where(elt => Z24.Order(elt) == 4).ToMathSet());
            Assert.Equal(new MathSet<int>(new[] { 4, 20 }), Z24.Set.Where(elt => Z24.Order(elt) == 6).ToMathSet());
        }
    }

    public class pinter_13_H_3
    {
        static Dictionary<string, string> eqs = new Dictionary<string, string>
            {
                { "e", "" },
                { "aaaa", "e" },    // a^4 = e
                { "bb", "e" },      // b^2 = e
                { "ba", "ab" }      // ba = ab
            };

        static Dictionary<string, string> memo = new Dictionary<string, string>();

        static Group<string> G = new Group<string>
        {
            Identity = "e",
            Set = new MathSet<string>(new[] { "e", "a", "aa", "aaa", "b", "ab", "aab", "aaab" }),
            // Op = (a, b) => Generate(eqs, a + b).First(elt => G.Set.Contains(elt))
            Op = (a, b) => 
            {
                if (memo.ContainsKey(a + b)) return memo[a + b];

                memo[a + b] = Generate(eqs, a + b).First(elt => G.Set.Contains(elt));

                return memo[a + b];
            }
        };

        [Fact]
        public static void IsIsomorphic() => Assert.True(G.IsIsomorphic(ZxZ(4, 2)));

        [Fact]
        public static void IsomorphicImage() => Assert.Equal("Z4xZ2", G.IsomorphicImage());

        [Fact]
        public static void IsomorphicImage_Z2xZ2xZ2() => Assert.Equal("Z2xZ2xZ2", ZxZxZ(2, 2, 2).IsomorphicImage());
    }
}
